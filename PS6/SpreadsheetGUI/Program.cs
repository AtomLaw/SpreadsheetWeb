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




    static class Program
    {
        static ClientProtocol cp;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {


            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            cp = new ClientProtocol();

            
        }
        
    }



    public class ClientProtocol
    {
        static SpreadsheetClientModel model;

        static ClientProtocol()
        {
            //Register client model events
            RegisterEvents();

            SpreadsheetApplicationContext appContext = SpreadsheetApplicationContext.getAppContext();
            appContext.RunForm(new ConnectToHostForm());
            Application.Run(appContext);
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
        }

        public static void model_UpdateEvent(string obj)
        {
            throw new NotImplementedException();
        }

        public static void model_UndoEndEvent(string obj)
        {
            throw new NotImplementedException();
        }

        public static void model_UndoFailEvent(string obj)
        {
            throw new NotImplementedException();
        }

        public static void model_UndoOKEvent(string obj)
        {
            throw new NotImplementedException();
        }

        public static void model_SaveFailEvent(string obj)
        {
            throw new NotImplementedException();
        }

        public static void model_SaveOKEvent(string obj)
        {
            throw new NotImplementedException();
        }

        public static void model_JoinFailEvent(string obj)
        {
            throw new NotImplementedException();
        }

        public static void model_JoinOKEvent(string obj)
        {
            throw new NotImplementedException();
        }

        public static void model_ChangeFailEvent(string obj)
        {
            throw new NotImplementedException();
        }

        public static void model_ChangeOKEvent(string obj)
        {
            throw new NotImplementedException();
        }

        public static void model_CreateFailEvent(string obj)
        {
            throw new NotImplementedException();
        }

        public static void model_CreateOKEvent(string obj)
        {
            throw new NotImplementedException();
        }

    }

}
