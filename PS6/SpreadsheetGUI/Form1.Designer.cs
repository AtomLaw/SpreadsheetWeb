namespace SpreadsheetGUI
{
    partial class Form1
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
            this.container_Master = new System.Windows.Forms.SplitContainer();
            this.btn_SetContents = new System.Windows.Forms.Button();
            this.label_Contents = new System.Windows.Forms.Label();
            this.txtBox_Contents = new System.Windows.Forms.TextBox();
            this.label_Value = new System.Windows.Forms.Label();
            this.label_Cell = new System.Windows.Forms.Label();
            this.txtBox_Value = new System.Windows.Forms.TextBox();
            this.txtBox_Cell = new System.Windows.Forms.TextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menu_File = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_File_New = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_File_Open = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_File_Save = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_File_Exit = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showHelpDocumentationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ssp = new SS.SpreadsheetPanel();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.container_Master)).BeginInit();
            this.container_Master.Panel1.SuspendLayout();
            this.container_Master.Panel2.SuspendLayout();
            this.container_Master.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // container_Master
            // 
            this.container_Master.Dock = System.Windows.Forms.DockStyle.Fill;
            this.container_Master.Location = new System.Drawing.Point(0, 0);
            this.container_Master.Name = "container_Master";
            this.container_Master.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // container_Master.Panel1
            // 
            this.container_Master.Panel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.container_Master.Panel1.Controls.Add(this.btn_SetContents);
            this.container_Master.Panel1.Controls.Add(this.label_Contents);
            this.container_Master.Panel1.Controls.Add(this.txtBox_Contents);
            this.container_Master.Panel1.Controls.Add(this.label_Value);
            this.container_Master.Panel1.Controls.Add(this.label_Cell);
            this.container_Master.Panel1.Controls.Add(this.txtBox_Value);
            this.container_Master.Panel1.Controls.Add(this.txtBox_Cell);
            this.container_Master.Panel1.Controls.Add(this.menuStrip1);
            // 
            // container_Master.Panel2
            // 
            this.container_Master.Panel2.Controls.Add(this.ssp);
            this.container_Master.Size = new System.Drawing.Size(1164, 683);
            this.container_Master.SplitterDistance = 71;
            this.container_Master.TabIndex = 0;
            // 
            // btn_SetContents
            // 
            this.btn_SetContents.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_SetContents.Location = new System.Drawing.Point(899, 28);
            this.btn_SetContents.Name = "btn_SetContents";
            this.btn_SetContents.Size = new System.Drawing.Size(109, 23);
            this.btn_SetContents.TabIndex = 7;
            this.btn_SetContents.Text = "Set Contents";
            this.btn_SetContents.UseVisualStyleBackColor = true;
            this.btn_SetContents.Click += new System.EventHandler(this.btn_SetContents_Click);
            // 
            // label_Contents
            // 
            this.label_Contents.AutoSize = true;
            this.label_Contents.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_Contents.Location = new System.Drawing.Point(551, 51);
            this.label_Contents.Name = "label_Contents";
            this.label_Contents.Size = new System.Drawing.Size(55, 15);
            this.label_Contents.TabIndex = 5;
            this.label_Contents.Text = "Contents";
            // 
            // txtBox_Contents
            // 
            this.txtBox_Contents.AcceptsReturn = true;
            this.txtBox_Contents.AcceptsTab = true;
            this.txtBox_Contents.BackColor = System.Drawing.SystemColors.HighlightText;
            this.txtBox_Contents.Location = new System.Drawing.Point(300, 30);
            this.txtBox_Contents.Name = "txtBox_Contents";
            this.txtBox_Contents.Size = new System.Drawing.Size(573, 20);
            this.txtBox_Contents.TabIndex = 4;
            this.txtBox_Contents.UseWaitCursor = true;
            // 
            // label_Value
            // 
            this.label_Value.AutoSize = true;
            this.label_Value.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_Value.Location = new System.Drawing.Point(189, 51);
            this.label_Value.Name = "label_Value";
            this.label_Value.Size = new System.Drawing.Size(37, 15);
            this.label_Value.TabIndex = 3;
            this.label_Value.Text = "Value";
            // 
            // label_Cell
            // 
            this.label_Cell.AutoSize = true;
            this.label_Cell.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_Cell.Location = new System.Drawing.Point(50, 51);
            this.label_Cell.Name = "label_Cell";
            this.label_Cell.Size = new System.Drawing.Size(28, 15);
            this.label_Cell.TabIndex = 2;
            this.label_Cell.Text = "Cell";
            // 
            // txtBox_Value
            // 
            this.txtBox_Value.AcceptsTab = true;
            this.txtBox_Value.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.txtBox_Value.Location = new System.Drawing.Point(135, 30);
            this.txtBox_Value.Name = "txtBox_Value";
            this.txtBox_Value.ReadOnly = true;
            this.txtBox_Value.Size = new System.Drawing.Size(142, 20);
            this.txtBox_Value.TabIndex = 1;
            this.txtBox_Value.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtBox_Cell
            // 
            this.txtBox_Cell.AcceptsTab = true;
            this.txtBox_Cell.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.txtBox_Cell.Location = new System.Drawing.Point(15, 30);
            this.txtBox_Cell.Name = "txtBox_Cell";
            this.txtBox_Cell.ReadOnly = true;
            this.txtBox_Cell.Size = new System.Drawing.Size(100, 20);
            this.txtBox_Cell.TabIndex = 0;
            this.txtBox_Cell.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menu_File,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1164, 24);
            this.menuStrip1.TabIndex = 6;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // menu_File
            // 
            this.menu_File.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.menu_File.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menu_File_New,
            this.menu_File_Open,
            this.menu_File_Save,
            this.saveAsToolStripMenuItem,
            this.menu_File_Exit});
            this.menu_File.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.menu_File.Name = "menu_File";
            this.menu_File.Size = new System.Drawing.Size(37, 20);
            this.menu_File.Text = "File";
            // 
            // menu_File_New
            // 
            this.menu_File_New.Name = "menu_File_New";
            this.menu_File_New.Size = new System.Drawing.Size(152, 22);
            this.menu_File_New.Text = "New";
            this.menu_File_New.Click += new System.EventHandler(this.menu_File_New_Click);
            // 
            // menu_File_Open
            // 
            this.menu_File_Open.Name = "menu_File_Open";
            this.menu_File_Open.Size = new System.Drawing.Size(152, 22);
            this.menu_File_Open.Text = "Open";
            this.menu_File_Open.Click += new System.EventHandler(this.menu_File_Open_Click);
            // 
            // menu_File_Save
            // 
            this.menu_File_Save.Name = "menu_File_Save";
            this.menu_File_Save.Size = new System.Drawing.Size(152, 22);
            this.menu_File_Save.Text = "Save";
            this.menu_File_Save.Click += new System.EventHandler(this.menu_File_Save_Click);
            // 
            // menu_File_Exit
            // 
            this.menu_File_Exit.Name = "menu_File_Exit";
            this.menu_File_Exit.Size = new System.Drawing.Size(152, 22);
            this.menu_File_Exit.Text = "Exit";
            this.menu_File_Exit.Click += new System.EventHandler(this.menu_File_Exit_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showHelpDocumentationToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // showHelpDocumentationToolStripMenuItem
            // 
            this.showHelpDocumentationToolStripMenuItem.Name = "showHelpDocumentationToolStripMenuItem";
            this.showHelpDocumentationToolStripMenuItem.Size = new System.Drawing.Size(217, 22);
            this.showHelpDocumentationToolStripMenuItem.Text = "Show Help Documentation";
            this.showHelpDocumentationToolStripMenuItem.Click += new System.EventHandler(this.showHelpDocumentationToolStripMenuItem_Click);
            // 
            // ssp
            // 
            this.ssp.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ssp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ssp.Location = new System.Drawing.Point(0, 0);
            this.ssp.Name = "ssp";
            this.ssp.Size = new System.Drawing.Size(1164, 608);
            this.ssp.TabIndex = 0;
            this.ssp.SelectionChanged += new SS.SelectionChangedHandler(this.spreadsheetPanel1_SelectionChanged);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.saveAsToolStripMenuItem.Text = "Save As..";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1164, 683);
            this.Controls.Add(this.container_Master);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "SimpleSheets";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.container_Master.Panel1.ResumeLayout(false);
            this.container_Master.Panel1.PerformLayout();
            this.container_Master.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.container_Master)).EndInit();
            this.container_Master.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer container_Master;
        private SS.SpreadsheetPanel ssp;
        private System.Windows.Forms.Label label_Cell;
        private System.Windows.Forms.TextBox txtBox_Value;
        private System.Windows.Forms.TextBox txtBox_Cell;
        private System.Windows.Forms.Label label_Contents;
        private System.Windows.Forms.TextBox txtBox_Contents;
        private System.Windows.Forms.Label label_Value;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menu_File;
        private System.Windows.Forms.ToolStripMenuItem menu_File_Open;
        private System.Windows.Forms.ToolStripMenuItem menu_File_Save;
        private System.Windows.Forms.ToolStripMenuItem menu_File_Exit;
        private System.Windows.Forms.ToolStripMenuItem menu_File_New;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Button btn_SetContents;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showHelpDocumentationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
    }
}

