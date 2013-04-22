using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SpreadsheetClient
{
    public partial class DebugConsole : Form
    {
        public DebugConsole()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Prints a message to the debug console new lines
        /// are NOT implied at the END of the line, but is implied at the beginning.
        /// </summary>
        /// <param name="text"></param>
        public void Out(string text)
        {
            if (console.InvokeRequired)
                console.Invoke(new Action(() => { console.AppendText("\n" + text); }));
            else
                console.Text += "\n" + text;
        }

        /// <summary>
        /// Prints a message to the debug console new lines
        /// ARE implied at both beginning and ends of the parameter.
        /// </summary>
        /// <param name="text"></param>
        public void Line(string text)
        {
            if (console.InvokeRequired)
                console.Invoke(new Action(() => { console.AppendText("\n" + text + "\n"); }));
            else if (console.Disposing)
                return;
            else
                console.AppendText("\n" + text + "\n");
        }

        private void DebugConsole_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Application.OpenForms.Count > 1)
            {
                Hide();
                e.Cancel = true;
            }
        }

    }
}
