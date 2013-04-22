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
    public partial class SpreadsheetEntry : Form
    {

        SpreadsheetClientModel model;

        //host name
        string host;

        /// <summary>
        /// 
        /// </summary>
        public SpreadsheetEntry(string host)
        {
            InitializeComponent();

            model = SpreadsheetClient.model;

            this.KeyPreview = true;

            this.host = host;
        }

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void newConnectionMenuItem_Click(object sender, EventArgs e)
        //{
        //    new SpreadsheetClient();
        //}


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void disconnectMenuItem_Click(object sender, EventArgs e)
        {
            new SpreadsheetClient();
            Close();
        }


        /// <summary>
        /// Send a request to join an existing spreadsheet to the server.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void joinExistingButton_Click(object sender, EventArgs e)
        {
            string ssName = spreadsheetNameTextBox.Text;
            string password = passwordTextBox.Text;

            if (password == null || password.Length < 1)
                MessageBox.Show("The password entered was invalid");
            else if (ssName == null || ssName.Length < 1)
                MessageBox.Show("The Spreadsheet Name entered was invalid");
            else
            {
                string message = "JOIN\nName:" + ssName + "\nPassword:" + password;
                model.SendMessage(message);

                //model.SendMessage("JOIN");
                //model.SendMessage("Name:" + spreadsheetNameTextBox.Text);
                //model.SendMessage("Password:" + passwordTextBox.Text);
                //For Debug
                //SpreadsheetApplicationContext appContext = SpreadsheetApplicationContext.getAppContext();
                //appContext.RunForm(new SpreadsheetGUI(ssName, password));
            }
        }


        /// <summary>
        /// Send a message to the server requesting to create a new spreadsheet
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void createNewButton_Click(object sender, EventArgs e)
        {
            string ssName = spreadsheetNameTextBox.Text;
            string password = passwordTextBox.Text;

            if (password == null || password.Length < 1)
                MessageBox.Show("The password entered was invalid");
            else if (ssName == null || ssName.Length < 1)
                MessageBox.Show("The Spreadsheet Name entered was invalid");
            else
            {
                string message = "CREATE\nName:" + ssName + "\nPassword:" + password;
                model.SendMessage(message);
                //model.SendMessage("CREATE");
                //model.SendMessage("Name:" + ssName);
                //model.SendMessage("Password:" + password);
                //For Debug
                //SpreadsheetApplicationContext appContext = SpreadsheetApplicationContext.getAppContext();
                //appContext.RunForm(new SpreadsheetGUI(ssName, password));
            }
        }


        /// <summary>
        /// Always disconnect from the server on close.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SpreadsheetEntry_FormClosing(object sender, FormClosingEventArgs e)
        {
            model.CloseConnection();
        }

        private void SpreadsheetEntry_Load(object sender, EventArgs e)
        {
            hostNameLabel.Text = this.host;
        }

        public void SetResponseText(string text)
        {
            if (response.Disposing || response.IsDisposed)
                return;
            else if(response.InvokeRequired)
                response.Invoke(new Action(() => {response.Text = text;}));
            else
                response.Text = text;
        }

        private void showDebugConsoleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SpreadsheetClient.showDebug();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
            Application.Exit();
            //SpreadsheetClient.CloseAll();
        }

        //private void KeyDown(object sender, EventArgs e)
        //{
        //    if(e.Alt && e.KeyCode == Keys.N)
        //}

    }
}
