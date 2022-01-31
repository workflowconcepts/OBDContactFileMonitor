namespace com.workflowconcepts.applications.filemonitor
{
    partial class frmManager
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.MenuStrip mnuMain;
        private System.Windows.Forms.ToolStripMenuItem mnuMainFile;
        private System.Windows.Forms.ToolStripMenuItem mnuMainFileExit;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.TabPage tabSettings;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.GroupBox groupBox1;
        private ucEmailSettings ucEmailSettings;
        private System.Windows.Forms.TabPage tabServices;
        private ucWindowsServiceController ucWindowsServiceController;
        private ucWindowsServiceController ucDataCollectionServiceController;
        private System.Windows.Forms.PictureBox pbCompanyLogo;
        private System.Windows.Forms.ToolStripMenuItem mnuMainAbout;
        private System.Windows.Forms.ToolStripMenuItem mnuMainAboutCompany;
        private System.Windows.Forms.GroupBox groupBox3;
        private ucDatabaseConnectionInformation ucDatabaseConnectionInformation;
        private System.Windows.Forms.GroupBox groupBox4;
        private ucRemotingInformation ucRemotingInformation;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmManager));
            this.mnuMain = new System.Windows.Forms.MenuStrip();
            this.mnuMainFile = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuMainFileExit = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuMainAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuMainAboutCompany = new System.Windows.Forms.ToolStripMenuItem();
            this.btnClose = new System.Windows.Forms.Button();
            this.tabSettings = new System.Windows.Forms.TabPage();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.ucFolderMonitor = new com.workflowconcepts.applications.filemonitor.ucFolderMonitor();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.ucRemotingInformation = new com.workflowconcepts.applications.filemonitor.ucRemotingInformation();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.ucDatabaseConnectionInformation = new com.workflowconcepts.applications.filemonitor.ucDatabaseConnectionInformation();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ucEmailSettings = new com.workflowconcepts.applications.filemonitor.ucEmailSettings();
            this.btnSave = new System.Windows.Forms.Button();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabServices = new System.Windows.Forms.TabPage();
            this.btnFakeServiceStopped = new System.Windows.Forms.Button();
            this.btnFakeServiceRunning = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.ucWindowsServiceController = new com.workflowconcepts.applications.filemonitor.ucWindowsServiceController();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.ucDebugSettings = new com.workflowconcepts.applications.filemonitor.ucDebugSettings();
            this.pbCompanyLogo = new System.Windows.Forms.PictureBox();
            this.mnuMain.SuspendLayout();
            this.tabSettings.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabServices.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbCompanyLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // mnuMain
            // 
            this.mnuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuMainFile,
            this.mnuMainAbout});
            this.mnuMain.Location = new System.Drawing.Point(0, 0);
            this.mnuMain.Name = "mnuMain";
            this.mnuMain.Size = new System.Drawing.Size(880, 24);
            this.mnuMain.TabIndex = 0;
            this.mnuMain.Text = "menuStrip1";
            // 
            // mnuMainFile
            // 
            this.mnuMainFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuMainFileExit});
            this.mnuMainFile.Name = "mnuMainFile";
            this.mnuMainFile.Size = new System.Drawing.Size(37, 20);
            this.mnuMainFile.Text = "&File";
            // 
            // mnuMainFileExit
            // 
            this.mnuMainFileExit.Name = "mnuMainFileExit";
            this.mnuMainFileExit.Size = new System.Drawing.Size(93, 22);
            this.mnuMainFileExit.Text = "&Exit";
            this.mnuMainFileExit.Click += new System.EventHandler(this.mnuMainFileExit_Click);
            // 
            // mnuMainAbout
            // 
            this.mnuMainAbout.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuMainAboutCompany});
            this.mnuMainAbout.Name = "mnuMainAbout";
            this.mnuMainAbout.Size = new System.Drawing.Size(52, 20);
            this.mnuMainAbout.Text = "&About";
            // 
            // mnuMainAboutCompany
            // 
            this.mnuMainAboutCompany.Name = "mnuMainAboutCompany";
            this.mnuMainAboutCompany.Size = new System.Drawing.Size(178, 22);
            this.mnuMainAboutCompany.Text = "&Workflow Concepts";
            this.mnuMainAboutCompany.Click += new System.EventHandler(this.mnuMainAboutCompany_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(791, 357);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 27);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "&Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // tabSettings
            // 
            this.tabSettings.Controls.Add(this.groupBox6);
            this.tabSettings.Controls.Add(this.groupBox4);
            this.tabSettings.Controls.Add(this.groupBox3);
            this.tabSettings.Controls.Add(this.groupBox1);
            this.tabSettings.Location = new System.Drawing.Point(4, 22);
            this.tabSettings.Name = "tabSettings";
            this.tabSettings.Padding = new System.Windows.Forms.Padding(3);
            this.tabSettings.Size = new System.Drawing.Size(850, 298);
            this.tabSettings.TabIndex = 0;
            this.tabSettings.Text = "Settings";
            this.tabSettings.UseVisualStyleBackColor = true;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.ucFolderMonitor);
            this.groupBox6.Location = new System.Drawing.Point(426, 177);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(410, 113);
            this.groupBox6.TabIndex = 14;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Folder Monitor";
            // 
            // ucFolderMonitor
            // 
            this.ucFolderMonitor.Location = new System.Drawing.Point(6, 19);
            this.ucFolderMonitor.Name = "ucFolderMonitor";
            this.ucFolderMonitor.Size = new System.Drawing.Size(390, 92);
            this.ucFolderMonitor.TabIndex = 0;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.ucRemotingInformation);
            this.groupBox4.Location = new System.Drawing.Point(10, 201);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(410, 70);
            this.groupBox4.TabIndex = 13;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Remoting";
            // 
            // ucRemotingInformation
            // 
            this.ucRemotingInformation.Location = new System.Drawing.Point(8, 19);
            this.ucRemotingInformation.Name = "ucRemotingInformation";
            this.ucRemotingInformation.Size = new System.Drawing.Size(390, 36);
            this.ucRemotingInformation.TabIndex = 0;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.ucDatabaseConnectionInformation);
            this.groupBox3.Location = new System.Drawing.Point(426, 6);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(410, 165);
            this.groupBox3.TabIndex = 12;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Database Connection Information";
            // 
            // ucDatabaseConnectionInformation
            // 
            this.ucDatabaseConnectionInformation.Location = new System.Drawing.Point(8, 17);
            this.ucDatabaseConnectionInformation.Name = "ucDatabaseConnectionInformation";
            this.ucDatabaseConnectionInformation.Size = new System.Drawing.Size(390, 146);
            this.ucDatabaseConnectionInformation.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ucEmailSettings);
            this.groupBox1.Location = new System.Drawing.Point(10, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(410, 189);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Email Notifications";
            // 
            // ucEmailSettings
            // 
            this.ucEmailSettings.Location = new System.Drawing.Point(8, 19);
            this.ucEmailSettings.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ucEmailSettings.Name = "ucEmailSettings";
            this.ucEmailSettings.Size = new System.Drawing.Size(390, 171);
            this.ucEmailSettings.TabIndex = 9;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(710, 357);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 27);
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabSettings);
            this.tabControl.Controls.Add(this.tabServices);
            this.tabControl.Location = new System.Drawing.Point(12, 27);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(858, 324);
            this.tabControl.TabIndex = 2;
            // 
            // tabServices
            // 
            this.tabServices.Controls.Add(this.btnFakeServiceStopped);
            this.tabServices.Controls.Add(this.btnFakeServiceRunning);
            this.tabServices.Controls.Add(this.groupBox5);
            this.tabServices.Controls.Add(this.groupBox2);
            this.tabServices.Location = new System.Drawing.Point(4, 22);
            this.tabServices.Name = "tabServices";
            this.tabServices.Size = new System.Drawing.Size(850, 298);
            this.tabServices.TabIndex = 1;
            this.tabServices.Text = "Services";
            this.tabServices.UseVisualStyleBackColor = true;
            // 
            // btnFakeServiceStopped
            // 
            this.btnFakeServiceStopped.Location = new System.Drawing.Point(215, 170);
            this.btnFakeServiceStopped.Name = "btnFakeServiceStopped";
            this.btnFakeServiceStopped.Size = new System.Drawing.Size(205, 27);
            this.btnFakeServiceStopped.TabIndex = 15;
            this.btnFakeServiceStopped.Text = "Detach from Console Application";
            this.btnFakeServiceStopped.UseVisualStyleBackColor = true;
            this.btnFakeServiceStopped.Visible = false;
            this.btnFakeServiceStopped.Click += new System.EventHandler(this.btnFakeServiceStopped_Click);
            // 
            // btnFakeServiceRunning
            // 
            this.btnFakeServiceRunning.Location = new System.Drawing.Point(10, 170);
            this.btnFakeServiceRunning.Name = "btnFakeServiceRunning";
            this.btnFakeServiceRunning.Size = new System.Drawing.Size(205, 27);
            this.btnFakeServiceRunning.TabIndex = 14;
            this.btnFakeServiceRunning.Text = "Attach to Console Application";
            this.btnFakeServiceRunning.UseVisualStyleBackColor = true;
            this.btnFakeServiceRunning.Visible = false;
            this.btnFakeServiceRunning.Click += new System.EventHandler(this.btnFakeServiceRunning_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.ucWindowsServiceController);
            this.groupBox5.Location = new System.Drawing.Point(10, 6);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(410, 158);
            this.groupBox5.TabIndex = 13;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Windows Service";
            // 
            // ucWindowsServiceController
            // 
            this.ucWindowsServiceController.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ucWindowsServiceController.Location = new System.Drawing.Point(7, 21);
            this.ucWindowsServiceController.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ucWindowsServiceController.Name = "ucWindowsServiceController";
            this.ucWindowsServiceController.ServiceName = "";
            this.ucWindowsServiceController.Size = new System.Drawing.Size(390, 126);
            this.ucWindowsServiceController.TabIndex = 8;
            this.ucWindowsServiceController.UseProcessInfo = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.ucDebugSettings);
            this.groupBox2.Location = new System.Drawing.Point(426, 6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(410, 246);
            this.groupBox2.TabIndex = 12;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Debug";
            // 
            // ucDebugSettings
            // 
            this.ucDebugSettings.Location = new System.Drawing.Point(12, 15);
            this.ucDebugSettings.Name = "ucDebugSettings";
            this.ucDebugSettings.Size = new System.Drawing.Size(390, 224);
            this.ucDebugSettings.TabIndex = 0;
            // 
            // pbCompanyLogo
            // 
            this.pbCompanyLogo.Location = new System.Drawing.Point(16, 366);
            this.pbCompanyLogo.Name = "pbCompanyLogo";
            this.pbCompanyLogo.Size = new System.Drawing.Size(125, 37);
            this.pbCompanyLogo.TabIndex = 3;
            this.pbCompanyLogo.TabStop = false;
            // 
            // frmManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(880, 421);
            this.Controls.Add(this.pbCompanyLogo);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.mnuMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.mnuMain;
            this.MaximizeBox = false;
            this.Name = "frmManager";
            this.Text = "Outbound Dialer Data Connector -  Manager";
            this.Load += new System.EventHandler(this.frmConnectorManager_Load);
            this.mnuMain.ResumeLayout(false);
            this.mnuMain.PerformLayout();
            this.tabSettings.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.tabControl.ResumeLayout(false);
            this.tabServices.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbCompanyLogo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button btnFakeServiceRunning;
        private System.Windows.Forms.Button btnFakeServiceStopped;
        private ucDebugSettings ucDebugSettings;
        private System.Windows.Forms.GroupBox groupBox6;
        private ucFolderMonitor ucFolderMonitor;
    }
}