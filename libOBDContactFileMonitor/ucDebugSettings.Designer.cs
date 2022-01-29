namespace com.workflowconcepts.applications.filemonitor
{
    partial class ucDebugSettings
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ckbDebugEnabled = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cbDebugLevel = new System.Windows.Forms.ComboBox();
            this.cbRetainUnit = new System.Windows.Forms.ComboBox();
            this.txtRetainValue = new System.Windows.Forms.TextBox();
            this.cbFileSize = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.ckbEnableLogArchiving = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtLogFilesPerArchive = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtMaxPercentDiskForLogArchiving = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtMaxArchiveAgeIfDiskSpaceNeeded = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ckbDebugEnabled
            // 
            this.ckbDebugEnabled.AutoSize = true;
            this.ckbDebugEnabled.Location = new System.Drawing.Point(3, 3);
            this.ckbDebugEnabled.Name = "ckbDebugEnabled";
            this.ckbDebugEnabled.Size = new System.Drawing.Size(92, 17);
            this.ckbDebugEnabled.TabIndex = 0;
            this.ckbDebugEnabled.Text = "Enable debug";
            this.ckbDebugEnabled.UseVisualStyleBackColor = true;
            this.ckbDebugEnabled.CheckedChanged += new System.EventHandler(this.ckbDebugEnabled_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Debug Level";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(23, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Retain Unit";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(243, 60);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Retain Value";
            // 
            // cbDebugLevel
            // 
            this.cbDebugLevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDebugLevel.FormattingEnabled = true;
            this.cbDebugLevel.Items.AddRange(new object[] {
            "Trace",
            "Debug",
            "Information",
            "Warning",
            "Error",
            "Fatal"});
            this.cbDebugLevel.Location = new System.Drawing.Point(97, 30);
            this.cbDebugLevel.Name = "cbDebugLevel";
            this.cbDebugLevel.Size = new System.Drawing.Size(121, 21);
            this.cbDebugLevel.TabIndex = 4;
            this.cbDebugLevel.SelectedIndexChanged += new System.EventHandler(this.cbDebugLevel_SelectedIndexChanged);
            this.cbDebugLevel.Leave += new System.EventHandler(this.cbDebugLevel_Leave);
            // 
            // cbRetainUnit
            // 
            this.cbRetainUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbRetainUnit.FormattingEnabled = true;
            this.cbRetainUnit.Items.AddRange(new object[] {
            "Files"});
            this.cbRetainUnit.Location = new System.Drawing.Point(97, 57);
            this.cbRetainUnit.Name = "cbRetainUnit";
            this.cbRetainUnit.Size = new System.Drawing.Size(121, 21);
            this.cbRetainUnit.TabIndex = 5;
            this.cbRetainUnit.SelectedIndexChanged += new System.EventHandler(this.cbRetainUnit_SelectedIndexChanged);
            // 
            // txtRetainValue
            // 
            this.txtRetainValue.Location = new System.Drawing.Point(317, 57);
            this.txtRetainValue.MaxLength = 3;
            this.txtRetainValue.Name = "txtRetainValue";
            this.txtRetainValue.Size = new System.Drawing.Size(56, 20);
            this.txtRetainValue.TabIndex = 6;
            this.txtRetainValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtRetainValue.TextChanged += new System.EventHandler(this.txtRetainValue_TextChanged);
            this.txtRetainValue.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtRetainValue_KeyPress);
            this.txtRetainValue.Leave += new System.EventHandler(this.txtRetainValue_Leave);
            // 
            // cbFileSize
            // 
            this.cbFileSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFileSize.FormattingEnabled = true;
            this.cbFileSize.Items.AddRange(new object[] {
            "OneMegaByte",
            "ThreeMegaBytes",
            "FiveMegaBytes"});
            this.cbFileSize.Location = new System.Drawing.Point(97, 84);
            this.cbFileSize.Name = "cbFileSize";
            this.cbFileSize.Size = new System.Drawing.Size(121, 21);
            this.cbFileSize.TabIndex = 7;
            this.cbFileSize.SelectedIndexChanged += new System.EventHandler(this.cbFileSize_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(23, 87);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(46, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "File Size";
            // 
            // ckbEnableLogArchiving
            // 
            this.ckbEnableLogArchiving.AutoSize = true;
            this.ckbEnableLogArchiving.Location = new System.Drawing.Point(26, 121);
            this.ckbEnableLogArchiving.Name = "ckbEnableLogArchiving";
            this.ckbEnableLogArchiving.Size = new System.Drawing.Size(127, 17);
            this.ckbEnableLogArchiving.TabIndex = 9;
            this.ckbEnableLogArchiving.Text = "Enable Log Archiving";
            this.ckbEnableLogArchiving.UseVisualStyleBackColor = true;
            this.ckbEnableLogArchiving.CheckedChanged += new System.EventHandler(this.ckbEnableLogArchiving_CheckedChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(39, 151);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(106, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Log Files per Archive";
            // 
            // txtLogFilesPerArchive
            // 
            this.txtLogFilesPerArchive.Location = new System.Drawing.Point(260, 148);
            this.txtLogFilesPerArchive.MaxLength = 3;
            this.txtLogFilesPerArchive.Name = "txtLogFilesPerArchive";
            this.txtLogFilesPerArchive.Size = new System.Drawing.Size(56, 20);
            this.txtLogFilesPerArchive.TabIndex = 11;
            this.txtLogFilesPerArchive.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtLogFilesPerArchive.TextChanged += new System.EventHandler(this.txtLogFilesPerArchive_TextChanged);
            this.txtLogFilesPerArchive.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtLogFilesPerArchive_KeyPress);
            this.txtLogFilesPerArchive.Leave += new System.EventHandler(this.txtLogFilesPerArchive_Leave);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(39, 177);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(150, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "Max % of disk for log archiving";
            // 
            // txtMaxPercentDiskForLogArchiving
            // 
            this.txtMaxPercentDiskForLogArchiving.Location = new System.Drawing.Point(260, 174);
            this.txtMaxPercentDiskForLogArchiving.MaxLength = 3;
            this.txtMaxPercentDiskForLogArchiving.Name = "txtMaxPercentDiskForLogArchiving";
            this.txtMaxPercentDiskForLogArchiving.Size = new System.Drawing.Size(56, 20);
            this.txtMaxPercentDiskForLogArchiving.TabIndex = 13;
            this.txtMaxPercentDiskForLogArchiving.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtMaxPercentDiskForLogArchiving.TextChanged += new System.EventHandler(this.txtMaxPercentDiskForLogArchiving_TextChanged);
            this.txtMaxPercentDiskForLogArchiving.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMaxPercentDiskForLogArchiving_KeyPress);
            this.txtMaxPercentDiskForLogArchiving.Leave += new System.EventHandler(this.txtMaxPercentDiskForLogArchiving_Leave);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(39, 203);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(161, 13);
            this.label7.TabIndex = 14;
            this.label7.Text = "Max archive age (if disk needed)";
            // 
            // txtMaxArchiveAgeIfDiskSpaceNeeded
            // 
            this.txtMaxArchiveAgeIfDiskSpaceNeeded.Location = new System.Drawing.Point(260, 200);
            this.txtMaxArchiveAgeIfDiskSpaceNeeded.MaxLength = 3;
            this.txtMaxArchiveAgeIfDiskSpaceNeeded.Name = "txtMaxArchiveAgeIfDiskSpaceNeeded";
            this.txtMaxArchiveAgeIfDiskSpaceNeeded.Size = new System.Drawing.Size(56, 20);
            this.txtMaxArchiveAgeIfDiskSpaceNeeded.TabIndex = 15;
            this.txtMaxArchiveAgeIfDiskSpaceNeeded.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtMaxArchiveAgeIfDiskSpaceNeeded.TextChanged += new System.EventHandler(this.txtMaxArchiveAgeIfDiskSpaceNeeded_TextChanged);
            this.txtMaxArchiveAgeIfDiskSpaceNeeded.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMaxArchiveAgeIfDiskSpaceNeeded_KeyPress);
            this.txtMaxArchiveAgeIfDiskSpaceNeeded.Leave += new System.EventHandler(this.txtMaxArchiveAgeIfDiskSpaceNeeded_Leave);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(322, 203);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(29, 13);
            this.label8.TabIndex = 16;
            this.label8.Text = "days";
            // 
            // ucDebugSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtMaxArchiveAgeIfDiskSpaceNeeded);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtMaxPercentDiskForLogArchiving);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtLogFilesPerArchive);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.ckbEnableLogArchiving);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cbFileSize);
            this.Controls.Add(this.txtRetainValue);
            this.Controls.Add(this.cbRetainUnit);
            this.Controls.Add(this.cbDebugLevel);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ckbDebugEnabled);
            this.Name = "ucDebugSettings";
            this.Size = new System.Drawing.Size(390, 224);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.CheckBox ckbDebugEnabled;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.ComboBox cbDebugLevel;
        public System.Windows.Forms.ComboBox cbRetainUnit;
        public System.Windows.Forms.TextBox txtRetainValue;
        public System.Windows.Forms.ComboBox cbFileSize;
        private System.Windows.Forms.Label label4;
        public System.Windows.Forms.CheckBox ckbEnableLogArchiving;
        private System.Windows.Forms.Label label5;
        public System.Windows.Forms.TextBox txtLogFilesPerArchive;
        private System.Windows.Forms.Label label6;
        public System.Windows.Forms.TextBox txtMaxPercentDiskForLogArchiving;
        private System.Windows.Forms.Label label7;
        public System.Windows.Forms.TextBox txtMaxArchiveAgeIfDiskSpaceNeeded;
        private System.Windows.Forms.Label label8;
    }
}
