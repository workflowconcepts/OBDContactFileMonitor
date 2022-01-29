using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace com.workflowconcepts.applications.filemonitor
{
    public partial class ucFolderMonitor : UserControl
    {
        public event EventHandler Changed;

        public ucFolderMonitor()
        {
            InitializeComponent();
        }

        private void ucFileMonitor_Load(object sender, EventArgs e)
        {

        }

        private void txtFolderPath_TextChanged(object sender, EventArgs e)
        {
            Log.Instance.Info("Enter.");

            if (Changed != null)
            {
                Changed(this, new EventArgs());
            }
        }

        private void ckbArchiveProcessedFiles_CheckedChanged(object sender, EventArgs e)
        {
            Log.Instance.Info("Enter.");

            txtArchivePath.Enabled = !ckbArchiveProcessedFiles.Checked;
            txtArchivePath.Enabled = ckbArchiveProcessedFiles.Checked;

            if (Changed != null)
            {
                Changed(this, new EventArgs());
            }
        }

        private void txtArchivePath_TextChanged(object sender, EventArgs e)
        {
            Log.Instance.Info("Enter.");

            if (Changed != null)
            {
                Changed(this, new EventArgs());
            }
        }
    }
}
