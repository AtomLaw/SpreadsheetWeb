using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace SpreadsheetClient
{

    class SpreadsheetApplicationContext : ApplicationContext
    {

        private static int formCount = 0;

        private static SpreadsheetApplicationContext appContext;

        /// <summary>
        /// 
        /// </summary>
        private SpreadsheetApplicationContext()
        {

        }

        public static int FormCount
        {
            get { return formCount; }
        }
        /// <summary>
        /// Returns the one SpreadsheetApplicationContext.
        /// </summary>
        public static SpreadsheetApplicationContext getAppContext()
        {
            if (appContext == null)
            {
                appContext = new SpreadsheetApplicationContext();
            }
            return appContext;
        }


        /// <summary>
        /// Runs the form
        /// </summary>
        public void RunForm(Form form)
        {
            // One more form is running
            formCount++;

            // When this form closes, we want to find out
            form.FormClosed += (o, e) => { if (--formCount <= 0) ExitThread(); };

            // Run the form
            form.Show();
        }
    }



    /// <summary>
    /// The program instance when the app is launched
    /// </summary>
    static class Program
    {
        //static ClientProtocol cp;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            SpreadsheetApplicationContext appContext = SpreadsheetApplicationContext.getAppContext();
            SpreadsheetClient ssc = new SpreadsheetClient();
            
            
            //appContext.RunForm(new ConnectToHostForm());
            Application.Run(appContext);
        }
        
    }


    /// <summary>
    /// The global ClientProtocol.  All windows will use this protocol to access the model for
    /// server communication.
    /// </summary>
    class SpreadsheetClient
    {
        public static SpreadsheetClientModel model;

        private static SpreadsheetEntry entryForm;

        private static DebugConsole debug;

        private static bool ss_open_successful;

        private static Dictionary<string, SpreadsheetGUI> sheets;


        /// <summary>
        /// Create a SpreadsheetClientModel and register its events
        /// </summary>
        public SpreadsheetClient()
        {
            //Register client model events
            RegisterEvents();
            ss_open_successful = false;
            sheets = new Dictionary<string, SpreadsheetGUI>();

            SpreadsheetApplicationContext appContext = SpreadsheetApplicationContext.getAppContext();
            if (debug == null)
            {
                debug = new DebugConsole();
                appContext.RunForm(debug);
            }
            //entryForm = new SpreadsheetEntry("");

            appContext.RunForm(new ConnectToHostForm());
        }

        ///// <summary>
        ///// Returns the client protocol if one exists.  If none exists,
        ///// creates one and returns it.
        ///// </summary>
        ///// <returns></returns>
        //public static SpreadsheetClient getClientProtocol()
        //{
        //    if (ssc == null)
        //    {
        //        ssc = new SpreadsheetClient();
        //    }
        //    return ssc;
        //}

        ///// <summary>
        ///// Returns the Spreadsheet Client Model if there is one.  If none exists,
        ///// create one and return it.
        ///// </summary>
        ///// <returns></returns>
        //public static SpreadsheetClientModel getClientModel()
        //{
        //    if (model == null)
        //    {
        //        model = new SpreadsheetClientModel();
        //    }
        //    return model;
        //}

        /// <summary>
        /// Getter for ss_open_successful
        /// </summary>
        /// <returns></returns>
        public static bool IsSSOpen
        {
            get
            {
                return ss_open_successful;
            }
            set {}
        }


        /// <summary>
        /// 
        /// </summary>
        public static void OpenSpreadSheetEntry(string host)
        {
            entryForm = new SpreadsheetEntry(host);
            entryForm.Visible = true;
            SpreadsheetApplicationContext appContext = SpreadsheetApplicationContext.getAppContext();
            appContext.RunForm(entryForm);
        }

        /// <summary>
        /// 
        /// </summary>
        public static void FocusSpreadSheetEntry()
        {
            entryForm.Activate();
        }

        private static void RegisterEvents()
        {
            model = new SpreadsheetClientModel();

            model.CreateOKEvent += model_CreateOKEvent;
            model.CreateFailEvent += model_CreateFailEvent;
            model.ChangeOKEvent += model_ChangeOKEvent;
            model.ChangeFailEvent += model_ChangeFailEvent;
            model.ChangeWaitEvent += model_ChangeWaitEvent;
            model.JoinOKEvent += model_JoinOKEvent;
            model.JoinFailEvent += model_JoinFailEvent;
            model.SaveOKEvent += model_SaveOKEvent;
            model.SaveFailEvent += model_SaveFailEvent;
            model.UndoOKEvent += model_UndoOKEvent;
            model.UndoFailEvent += model_UndoFailEvent;
            model.UndoEndEvent += model_UndoEndEvent;
            model.UndoWaitEvent += model_UndoWaitEvent;
            model.UpdateEvent += model_UpdateEvent;
            model.ConnectionErrorEvent += model_CouldNotConnect;
            model.NullMessageReceivedEvent += model_NullMessage;

            model.Debug += model_Debug;
        }

        private static void model_Debug(string message)
        {
            debug.Line(message);
        }

        private static void model_NullMessage()
        {
            MessageBox.Show("Your connection was lost");
        }

        private static void model_CouldNotConnect(Exception e)
        {
            MessageBox.Show("Could not connect to the Spreadsheet Server: " + e.Message);
            debug.Line(e.Message);
        }

        public static void model_UpdateEvent(string message)
        {
            SpreadsheetApplicationContext appContext = SpreadsheetApplicationContext.getAppContext();
            string[] Message = message.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None);
            string name = "", cell = "", content = "";
            string version = "";
            int length = 0;
            foreach (string s in Message)
            {
                if (s.Contains("Name:"))
                    name = s.Substring(5);
                if (s.Contains("Cell:"))
                    cell = s.Substring(5);
                if (s.Contains("Version:"))
                    version = s.Substring(8);
                if (s.Contains("Length:"))
                    length = Int32.Parse(s.Substring(7));
                content = Message[5];
            }

            sheets[name].updateContents(cell, content, version);
            
        }

        public static void model_UndoEndEvent(string message)
        {
            //let user know they are up to date
        }

        public static void model_UndoWaitEvent(string message)
        {
            MessageBox.Show("Your version of the spreadsheet is out of date. Please wait!");
            //Let user know they are being updated.
        }

        public static void model_UndoFailEvent(string message)
        {
            MessageBox.Show("Failure to Undo changes.");
        }

        public static void model_UndoOKEvent(string message)
        {
            SpreadsheetApplicationContext appContext = SpreadsheetApplicationContext.getAppContext();
            string[] Message = message.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None);
            string name = "", cell = "", content = "";
            string version = "";
            int length = 0;
            foreach (string s in Message)
            {
                if (s.Contains("Name:"))
                    name = s.Substring(5);
                if (s.Contains("Cell:"))
                    cell = s.Substring(5); 
                if (s.Contains("Version:"))
                    version = s.Substring(8);
                if (s.Contains("Length:"))
                    length = Int32.Parse(s.Substring(7));
                content = Message[4];
            }

            //if (message.Contains("Name:"))
            //{
            //    name = message.Substring(message.IndexOf("Name:"));
            //}

            sheets[name].updateContents(cell, content, version);
        }

        public static void model_SaveFailEvent(string message)
        {
            SpreadsheetApplicationContext appContext = SpreadsheetApplicationContext.getAppContext();
            string[] Message = message.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None);
            string reason = Message[2];
            MessageBox.Show("The server was unable to save the session: " + reason);
        }

        public static void model_SaveOKEvent(string message)
        {
            //Update saved box on GUI
            MessageBox.Show("Saved successfully.");
        }

        public static void model_JoinFailEvent(string message)
        {
            SpreadsheetApplicationContext appContext = SpreadsheetApplicationContext.getAppContext();
            ss_open_successful = true;
            string[] Message = message.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None);
            string reason = Message[2];
            entryForm.SetResponseText("The server was unable to join you to the session: " + reason);
        }

        public static void model_JoinOKEvent(string message)
        {
            SpreadsheetApplicationContext appContext = SpreadsheetApplicationContext.getAppContext();
            ss_open_successful = true;
            string[] Message = message.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None);
            string name = "", password = "", xml = "";
            string version = ""; 
            int length = 0;
            foreach (string s in Message)
            {
                if (s.Contains("Name:"))
                    name = s.Substring(5);
                if (s.Contains("Password:"))
                    password = s.Substring(9);
                if (s.Contains("Version:"))
                    version = s.Substring(8);
                if (s.Contains("Length:"))
                    length = Int32.Parse(s.Substring(7));
                xml = Message[4];
            }
            //if (message.Contains("Name:"))
            //{
            //    name = message.Substring(message.IndexOf("Name:"));
            //}
            entryForm.SetResponseText("Join Successful. Setting up Spreadsheet...");
            SpreadsheetGUI sheet = new SpreadsheetGUI(name, password, version);
            sheets.Add(name, sheet);
            //the window is run from the GUI class when the ss is loaded
            //appContext.RunForm(sheet);
        }

        public static void model_ChangeFailEvent(string message)
        {
            MessageBox.Show("Your change failed.");
        }

        public static void model_ChangeOKEvent(string message)
        {
            SpreadsheetApplicationContext appContext = SpreadsheetApplicationContext.getAppContext();
            ss_open_successful = true;
            string[] Message = message.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None);
            string name = "", version = "";
            foreach (string s in Message)
            {
                if (s.Contains("Name:"))
                    name = s.Substring(5);
                if (s.Contains("Version:"))
                    version = s.Substring(8);
            }

            sheets[name].SetContentsOkay(version);

        }

        public static void model_ChangeWaitEvent(string message)
        {
            SpreadsheetApplicationContext appContext = SpreadsheetApplicationContext.getAppContext();
            ss_open_successful = true;
            string[] Message = message.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None);
            string name = "", version = "";
            foreach (string s in Message)
            {
                if (s.Contains("Name:"))
                    name = s.Substring(5);
                if (s.Contains("Version:"))
                    version = s.Substring(8);
            }

            //do nothing?
        }

        public static void model_CreateFailEvent(string name, string message)
        {
            entryForm.SetResponseText("The server was unable to join the session: " + message);
        }

        public static void model_CreateOKEvent(string message)
        {
            debug.Line("Message: " + message);
            entryForm.SetResponseText("Created successfully, please join now!");

            SpreadsheetApplicationContext appContext = SpreadsheetApplicationContext.getAppContext();
            ss_open_successful = true;
            string[] Message = message.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None);
            string name = "", password = "";
            foreach (string s in Message)
            {
                if (s.Contains("Name:"))
                    name = s.Substring(s.IndexOf("Name:")+5, 5);
                if (s.Contains("Password:"))
                    password = s.Substring(9);
            }
            debug.Line("Parsed Name: " + name);
            debug.Line("Parsed Password: " + password);
            //if (message.Contains("Name:"))
            //{
            //    name = message.Substring(message.IndexOf("Name:"));
            //}
            //SpreadsheetGUI sheet = new SpreadsheetGUI(name, password, 0);
            //sheets.Add(name, sheet);
            //appContext.RunForm(sheet);
        }

    }

}
