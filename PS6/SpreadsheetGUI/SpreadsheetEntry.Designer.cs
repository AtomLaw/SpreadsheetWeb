﻿namespace SpreadsheetClient
{
    partial class SpreadsheetEntry
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showDebugConsoleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.disconnectMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.spreadsheetNameTextBox = new System.Windows.Forms.TextBox();
            this.passwordTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.joinExistingButton = new System.Windows.Forms.Button();
            this.createNewButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.hostNameLabel = new System.Windows.Forms.Label();
            this.respLabel = new System.Windows.Forms.Label();
            this.response = new System.Windows.Forms.Label();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.menuStrip1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(506, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showDebugConsoleToolStripMenuItem,
            this.disconnectMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // showDebugConsoleToolStripMenuItem
            // 
            this.showDebugConsoleToolStripMenuItem.Name = "showDebugConsoleToolStripMenuItem";
            this.showDebugConsoleToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.showDebugConsoleToolStripMenuItem.Text = "Show De&bug Console";
            this.showDebugConsoleToolStripMenuItem.Click += new System.EventHandler(this.showDebugConsoleToolStripMenuItem_Click);
            // 
            // disconnectMenuItem
            // 
            this.disconnectMenuItem.Name = "disconnectMenuItem";
            this.disconnectMenuItem.Size = new System.Drawing.Size(187, 22);
            this.disconnectMenuItem.Text = "&Disconnect";
            this.disconnectMenuItem.Click += new System.EventHandler(this.disconnectMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // spreadsheetNameTextBox
            // 
            this.spreadsheetNameTextBox.Location = new System.Drawing.Point(126, 126);
            this.spreadsheetNameTextBox.Name = "spreadsheetNameTextBox";
            this.spreadsheetNameTextBox.Size = new System.Drawing.Size(337, 20);
            this.spreadsheetNameTextBox.TabIndex = 1;
            // 
            // passwordTextBox
            // 
            this.passwordTextBox.Location = new System.Drawing.Point(126, 152);
            this.passwordTextBox.Name = "passwordTextBox";
            this.passwordTextBox.Size = new System.Drawing.Size(337, 20);
            this.passwordTextBox.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 129);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Spreadsheet Name:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(66, 155);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Password: ";
            // 
            // joinExistingButton
            // 
            this.joinExistingButton.Location = new System.Drawing.Point(302, 178);
            this.joinExistingButton.Name = "joinExistingButton";
            this.joinExistingButton.Size = new System.Drawing.Size(75, 25);
            this.joinExistingButton.TabIndex = 5;
            this.joinExistingButton.Text = "&Join";
            this.joinExistingButton.UseVisualStyleBackColor = true;
            this.joinExistingButton.Click += new System.EventHandler(this.joinExistingButton_Click);
            // 
            // createNewButton
            // 
            this.createNewButton.Location = new System.Drawing.Point(388, 178);
            this.createNewButton.Name = "createNewButton";
            this.createNewButton.Size = new System.Drawing.Size(75, 25);
            this.createNewButton.TabIndex = 6;
            this.createNewButton.Text = "Create &New";
            this.createNewButton.UseVisualStyleBackColor = true;
            this.createNewButton.Click += new System.EventHandler(this.createNewButton_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(17, 40);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(106, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Connected To Host: ";
            // 
            // hostNameLabel
            // 
            this.hostNameLabel.AutoSize = true;
            this.hostNameLabel.Location = new System.Drawing.Point(123, 40);
            this.hostNameLabel.Name = "hostNameLabel";
            this.hostNameLabel.Size = new System.Drawing.Size(66, 13);
            this.hostNameLabel.TabIndex = 8;
            this.hostNameLabel.Text = "(Host Name)";
            // 
            // respLabel
            // 
            this.respLabel.AutoSize = true;
            this.respLabel.Location = new System.Drawing.Point(60, 72);
            this.respLabel.Name = "respLabel";
            this.respLabel.Size = new System.Drawing.Size(61, 13);
            this.respLabel.TabIndex = 9;
            this.respLabel.Text = "Response: ";
            // 
            // response
            // 
            this.response.AutoSize = true;
            this.response.Location = new System.Drawing.Point(3, 0);
            this.response.Name = "response";
            this.response.Size = new System.Drawing.Size(55, 13);
            this.response.TabIndex = 10;
            this.response.Text = "Welcome!";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.response);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(120, 72);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(335, 48);
            this.flowLayoutPanel1.TabIndex = 11;
            // 
            // SpreadsheetEntry
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(506, 220);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.respLabel);
            this.Controls.Add(this.hostNameLabel);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.createNewButton);
            this.Controls.Add(this.joinExistingButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.passwordTextBox);
            this.Controls.Add(this.spreadsheetNameTextBox);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "SpreadsheetEntry";
            this.Text = "SpreadheetEntry";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SpreadsheetEntry_FormClosing);
            this.Load += new System.EventHandler(this.SpreadsheetEntry_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem disconnectMenuItem;
        private System.Windows.Forms.TextBox spreadsheetNameTextBox;
        private System.Windows.Forms.TextBox passwordTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button joinExistingButton;
        private System.Windows.Forms.Button createNewButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label hostNameLabel;
        private System.Windows.Forms.Label respLabel;
        private System.Windows.Forms.Label response;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.ToolStripMenuItem showDebugConsoleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
    }
}