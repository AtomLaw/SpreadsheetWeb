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
            //debug = new DebugConsole();
            //if (debug == null)
            //{
            //    debug = new DebugConsole();
            //    appContext.RunForm(debug);
            //}
            //entryForm = new SpreadsheetEntry("");

            appContext.RunForm(new ConnectToHostForm());
            //appContext.RunForm(new SpreadsheetGUI("name", "0", "<xml></xml>"));
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
        /// Opens a Spreadsheet Entry form.  There should ever only be one
        /// Spreadsheet entry form for any instance of the program.
        /// </summary>
        public static void OpenSpreadSheetEntry(string host)
        {
            entryForm = new SpreadsheetEntry(host);
            entryForm.Visible = true;
            SpreadsheetApplicationContext appContext = SpreadsheetApplicationContext.getAppContext();
            appContext.RunForm(entryForm);
        }


        /// <summary>
        /// Brings the entry form to the top level.
        /// </summary>
        public static void FocusSpreadSheetEntry()
        {
            entryForm.Activate();
        }


        /// <summary>
        /// Closes all open windows running in this instance of the client.
        /// </summary>
        public static void CloseAll()
        {
            Application.Exit();
        }


        /// <summary>
        /// Shows the DebugConsole window
        /// </summary>
        public static void showDebug()
        {
            if (debug == null)
                debug = new DebugConsole();
            SpreadsheetApplicationContext c = SpreadsheetApplicationContext.getAppContext();
            //Invoke(new Action(() => { c.RunForm(debug); }));
            if (debug.IsDisposed)
                debug = new DebugConsole();
            c.RunForm(debug);
            debug.Activate();
        }

        public static void RemoveSpreadsheetGUI(string name)
        {
            sheets.Remove(name);
        }


        /// <summary>
        /// Creates the member client model and registers all the
        /// clients methods to the model.
        /// </summary>
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
            model.ErrorEvent += model_ErrorEvent;
            model.Debug += model_Debug;
        }

        /// <summary>
        /// 
        /// </summary>
        static void model_ErrorEvent()
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// For printing out any debugging information to the DebugConsole.
        /// </summary>
        /// <param name="message"></param>
        private static void model_Debug(string message)
        {
            if(debug != null)
                debug.Line(message);
        }


        /// <summary>
        /// 
        /// </summary>
        private static void model_NullMessage()
        {
            MessageBox.Show("Your connection was lost");
        }

        private static void model_CouldNotConnect(Exception e)
        {
            MessageBox.Show("Could not connect to the Spreadsheet Server: " + e.Message);
            if (debug != null)
                debug.Line(e.Message);
        }

        public static void model_CreateOKEvent(string name, string password)
        {
            if (debug != null)
            {
                debug.Line("Name: " + name);
                debug.Line("Password: " + password);
            }
            entryForm.SetResponseText("'" + name + "' created successfully, please join now!");
        }

        public static void model_CreateFailEvent(string name, string message)
        {
            entryForm.SetResponseText("Unable to create'" + name + "'. " + message);
        }

        public static void model_JoinOKEvent(string name, string version, int length, string xml)
        {
            entryForm.SetResponseText("Joining '" + name + "'...");
            SpreadsheetGUI sheet = new SpreadsheetGUI(name, version, xml);
            sheets.Add(name, sheet);

            SpreadsheetApplicationContext appContext = SpreadsheetApplicationContext.getAppContext();
            //appContext.RunForm(new SpreadsheetGUI(name, version, xml));
            entryForm.Invoke(new Action(() => { appContext.RunForm(sheet); }));
        }
        
        public static void model_JoinFailEvent(string name, string message)
        {
            entryForm.SetResponseText("Unable to join '" + name + "'. " + message);
        }

        public static void model_ChangeOKEvent(string name, string version)
        {
            SpreadsheetApplicationContext appContext = SpreadsheetApplicationContext.getAppContext();
            sheets[name].updateVersionLabel(version);
            sheets[name].SetContentsOkay(version);
        }

        public static void model_ChangeFailEvent(string name, string message)
        {
            MessageBox.Show("Your change failed. " + message);
        }

        public static void model_ChangeWaitEvent(string name, string version)
        {
            MessageBox.Show("Waiting to be up to date to make your change...");
            //do nothing?
        }

        public static void model_UndoOKEvent(string name, string version, string cell, int length, string content)
        {
            sheets[name].updateContents(cell, content, version);
            sheets[name].updateVersionLabel(version);
        }

        public static void model_UndoEndEvent(string name, string version)
        {
            //let user know they are up to date
        }

        public static void model_UndoWaitEvent(string name, string version)
        {
            MessageBox.Show("Your version of the spreadsheet is out of date. Please wait!");
            //Let user know they are being updated.
        }

        public static void model_UndoFailEvent(string name, string message)
        {
            //Update response field on Spreadsheet window
            MessageBox.Show("Failure to Undo changes.");
        }

        public static void model_UpdateEvent(string name, string version, string cell, int length, string content)
        {
            sheets[name].updateContents(cell, content, version);
            sheets[name].updateVersionLabel(version);

        }

        public static void model_SaveOKEvent(string name)
        {
            //Update response field on Spreadsheet window
            MessageBox.Show("Saved successfully.");
        }

        public static void model_SaveFailEvent(string name, string message)
        {
            //Update response field on Spreadsheet window
            MessageBox.Show("Unable to Save. " + message);
        }
    }

}
