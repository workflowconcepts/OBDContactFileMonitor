
namespace com.workflowconcepts.applications.filemonitor
{
    partial class ucFolderMonitor
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
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtFolderPath = new System.Windows.Forms.TextBox();
            this.txtArchivePath = new System.Windows.Forms.TextBox();
            this.ckbArchiveProcessedFiles = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Folder Path";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(21, 69);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Archive ";
            // 
            // txtFolderPath
            // 
            this.txtFolderPath.Location = new System.Drawing.Point(86, 3);
            this.txtFolderPath.Name = "txtFolderPath";
            this.txtFolderPath.Size = new System.Drawing.Size(301, 20);
            this.txtFolderPath.TabIndex = 3;
            this.txtFolderPath.TextChanged += new System.EventHandler(this.txtFolderPath_TextChanged);
            // 
            // txtArchivePath
            // 
            this.txtArchivePath.Location = new System.Drawing.Point(86, 66);
            this.txtArchivePath.Name = "txtArchivePath";
            this.txtArchivePath.Size = new System.Drawing.Size(301, 20);
            this.txtArchivePath.TabIndex = 4;
            this.txtArchivePath.TextChanged += new System.EventHandler(this.txtArchivePath_TextChanged);
            // 
            // ckbArchiveProcessedFiles
            // 
            this.ckbArchiveProcessedFiles.AutoSize = true;
            this.ckbArchiveProcessedFiles.Location = new System.Drawing.Point(9, 43);
            this.ckbArchiveProcessedFiles.Name = "ckbArchiveProcessedFiles";
            this.ckbArchiveProcessedFiles.Size = new System.Drawing.Size(139, 17);
            this.ckbArchiveProcessedFiles.TabIndex = 5;
            this.ckbArchiveProcessedFiles.Text = "Archive Processed Files";
            this.ckbArchiveProcessedFiles.UseVisualStyleBackColor = true;
            this.ckbArchiveProcessedFiles.CheckedChanged += new System.EventHandler(this.ckbArchiveProcessedFiles_CheckedChanged);
            // 
            // ucFolderMonitor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ckbArchiveProcessedFiles);
            this.Controls.Add(this.txtArchivePath);
            this.Controls.Add(this.txtFolderPath);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Name = "ucFolderMonitor";
            this.Size = new System.Drawing.Size(390, 92);
            this.Load += new System.EventHandler(this.ucFileMonitor_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.TextBox txtFolderPath;
        public System.Windows.Forms.TextBox txtArchivePath;
        public System.Windows.Forms.CheckBox ckbArchiveProcessedFiles;
    }
}
