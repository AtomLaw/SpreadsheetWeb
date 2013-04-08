using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace SpreadsheetGUI
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
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            SpreadsheetApplicationContext appContext = SpreadsheetApplicationContext.getAppContext();
            appContext.RunForm(new Form1());
            Application.Run(appContext);
        }
    }
}
