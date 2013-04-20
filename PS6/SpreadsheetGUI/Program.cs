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

        private static bool ss_open_successful;

        /// <summary>
        /// Create a SpreadsheetClientModel and register its events
        /// </summary>
        public SpreadsheetClient()
        {
            //Register client model events
            RegisterEvents();
            ss_open_successful = false;

            SpreadsheetApplicationContext appContext = SpreadsheetApplicationContext.getAppContext();
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
        public bool IsSSOpen
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
            SpreadsheetEntry entryForm = new SpreadsheetEntry(host);
            SpreadsheetApplicationContext appContext = SpreadsheetApplicationContext.getAppContext();
            appContext.RunForm(entryForm);
        }

        /// <summary>
        /// 
        /// </summary>
        public static void FocusSpreadSheetEntry()
        {
            entryForm.Focus();
        }

        private static void RegisterEvents()
        {
            model = new SpreadsheetClientModel();

            model.CreateOKEvent += model_CreateOKEvent;
            model.CreateFailEvent += model_CreateFailEvent;
            model.ChangeOKEvent += model_ChangeOKEvent;
            model.ChangeFailEvent += model_ChangeFailEvent;
            model.JoinOKEvent += model_JoinOKEvent;
            model.JoinFailEvent += model_JoinFailEvent;
            model.SaveOKEvent += model_SaveOKEvent;
            model.SaveFailEvent += model_SaveFailEvent;
            model.UndoOKEvent += model_UndoOKEvent;
            model.UndoFailEvent += model_UndoFailEvent;
            model.UndoEndEvent += model_UndoEndEvent;
            model.UpdateEvent += model_UpdateEvent;
            model.ConnectionErrorEvent += model_CouldNotConnect;
        }

        private static void model_CouldNotConnect(Exception e)
        {
            MessageBox.Show("Could not connect to the Spreadsheet Server: " + e.Message);
        }

        public static void model_UpdateEvent(string message)
        {
            throw new NotImplementedException();
            
        }

        public static void model_UndoEndEvent(string message)
        {
            throw new NotImplementedException();
        }

        public static void model_UndoFailEvent(string message)
        {
            throw new NotImplementedException();
        }

        public static void model_UndoOKEvent(string message)
        {
            throw new NotImplementedException();
        }

        public static void model_SaveFailEvent(string message)
        {
            throw new NotImplementedException();
        }

        public static void model_SaveOKEvent(string message)
        {
            throw new NotImplementedException();
        }

        public static void model_JoinFailEvent(string message)
        {
            throw new NotImplementedException();
        }

        public static void model_JoinOKEvent(string message)
        {
            SpreadsheetApplicationContext appContext = SpreadsheetApplicationContext.getAppContext();
            ss_open_successful = true;
            string[] Message = message.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None);
            string name = "", password = "";
            foreach (string s in Message)
            {
                if (s.Contains("Name:"))
                    name = s.Substring(5);
                if (s.Contains("Password:"))
                    password = s.Substring(9);
            }
            //if (message.Contains("Name:"))
            //{
            //    name = message.Substring(message.IndexOf("Name:"));
            //}
            appContext.RunForm(new SpreadsheetGUI(name, password));
        }

        public static void model_ChangeFailEvent(string message)
        {
            throw new NotImplementedException();
        }

        public static void model_ChangeOKEvent(string message)
        {
            throw new NotImplementedException();
        }

        public static void model_CreateFailEvent(string message)
        {
            throw new NotImplementedException();
        }

        public static void model_CreateOKEvent(string message)
        {
            SpreadsheetApplicationContext appContext = SpreadsheetApplicationContext.getAppContext();
            ss_open_successful = true;
            string[] Message = message.Split(new string[] {"\r\n", "\n"}, StringSplitOptions.None);
            string name = "", password = "";
            foreach (string s in Message)
            {
                if (s.Contains("Name:"))
                    name = s.Substring(5);
                if (s.Contains("Password:"))
                    password = s.Substring(9);
            }
            //if (message.Contains("Name:"))
            //{
            //    name = message.Substring(message.IndexOf("Name:"));
            //}
            appContext.RunForm(new SpreadsheetGUI(name, password));
        }

    }

}
