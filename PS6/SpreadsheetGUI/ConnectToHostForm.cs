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
    public partial class ConnectToHostForm : Form
    {

        SpreadsheetClientModel model;

        public ConnectToHostForm()
        {
            InitializeComponent();

            model = SpreadsheetClient.model;

            this.KeyPreview = true;

            AcceptButton = btn_ConnectToServer;
        }

        private void btn_ConnectToServer_Click(object sender, EventArgs e)
        {
            string server = serverInputTextBox.Text;
            if (server == null || server.Length < 1)
                MessageBox.Show("The server address you entered is invalid. Please try again");
            else
            {
                response.Text = "Connecting...";
                model.ConnectToServer(server);

                //SpreadsheetClient.OpenSpreadSheetEntry(server);
                //Close();

                //SpreadsheetApplicationContext.getAppContext().RunForm(new SpreadsheetEntry(server));
            }

            if (model.IsConnected)
            {
                SpreadsheetClient.OpenSpreadSheetEntry(server);
                //SpreadsheetApplicationContext.getAppContext().RunForm(new SpreadsheetEntry(server));
                Close();
            }

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
            Application.Exit();
            //SpreadsheetClient.CloseAll();
        }

        private void openDebugConsoleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SpreadsheetClient.showDebug();   
        }

    }
}
