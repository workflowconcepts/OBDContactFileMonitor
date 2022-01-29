using System;
using System.Windows.Forms;


namespace com.workflowconcepts.applications.filemonitor
{
    public partial class ucDebugSettings : UserControl
    {
        public event EventHandler Changed;

        public ucDebugSettings()
        {
            InitializeComponent();
        }

        private void ckbDebugEnabled_CheckedChanged(object sender, EventArgs e)
        {
           Log.Instance.Info("Enter.");

            cbDebugLevel.Enabled = ckbDebugEnabled.Checked;
            cbRetainUnit.Enabled = ckbDebugEnabled.Checked;
            cbFileSize.Enabled = ckbDebugEnabled.Checked;

            txtRetainValue.Enabled = ckbDebugEnabled.Checked;

            if (Changed != null)
            {
                Changed(this, new EventArgs());
            }
        }

        private void txtRetainValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void cbDebugLevel_SelectedIndexChanged(object sender, EventArgs e)
        {
           Log.Instance.Info("Enter.");

            if (Changed != null)
            {
                Changed(this, new EventArgs());
            }
        }

        private void cbRetainUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
           Log.Instance.Info("Enter.");

            if (Changed != null)
            {
                Changed(this, new EventArgs());
            }
        }

        private void txtRetainValue_TextChanged(object sender, EventArgs e)
        {
           Log.Instance.Info("Enter.");

            if (Changed != null)
            {
                Changed(this, new EventArgs());
            }
        }

        private void txtRetainValue_Leave(object sender, EventArgs e)
        {
           Log.Instance.Info("Enter.");

            if (txtRetainValue.Text.Length == 0)
            {
                txtRetainValue.Text = "100";
            }
        }

        private void cbDebugLevel_Leave(object sender, EventArgs e)
        {
           Log.Instance.Info("Enter.");

            if (this.cbDebugLevel.Text.Length == 0)
            {
                this.cbDebugLevel.Text = "Verbose";
            }
        }

        private void cbFileSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            Log.Instance.Info("Enter.");

            if (Changed != null)
            {
                Changed(this, new EventArgs());
            }
        }

        private void txtLogFilesPerArchive_TextChanged(object sender, EventArgs e)
        {
            Log.Instance.Debug("Enter.");

            if (Changed != null)
            {
                Changed(this, new EventArgs());
            }
        }

        private void txtMaxPercentDiskForLogArchiving_TextChanged(object sender, EventArgs e)
        {
            Log.Instance.Debug("Enter.");

            if (Changed != null)
            {
                Changed(this, new EventArgs());
            }
        }

        private void txtLogFilesPerArchive_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtMaxPercentDiskForLogArchiving_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void ckbEnableLogArchiving_CheckedChanged(object sender, EventArgs e)
        {
            Log.Instance.Debug("Enter.");

            txtLogFilesPerArchive.Enabled = ckbEnableLogArchiving.Checked;
            txtMaxPercentDiskForLogArchiving.Enabled = ckbEnableLogArchiving.Checked;

            if (Changed != null)
            {
                Changed(this, new EventArgs());
            }
        }

        private void txtLogFilesPerArchive_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtLogFilesPerArchive.Text))
            {
                txtLogFilesPerArchive.Text = ApplicationConstants.LOG_ARCHIVING_NUMBER_OF_LOG_FILES_PER_ARCHIVE.ToString();
            }
            else
            {
                int iLogFilesPerArchive = 0;

                if (int.TryParse(txtLogFilesPerArchive.Text, out iLogFilesPerArchive))
                {
                    if (iLogFilesPerArchive <= 0)
                    {
                        txtLogFilesPerArchive.Text = ApplicationConstants.LOG_ARCHIVING_NUMBER_OF_LOG_FILES_PER_ARCHIVE.ToString();
                    }
                }
                else
                {
                    txtLogFilesPerArchive.Text = ApplicationConstants.LOG_ARCHIVING_NUMBER_OF_LOG_FILES_PER_ARCHIVE.ToString();
                }
            }
        }

        private void txtMaxPercentDiskForLogArchiving_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaxPercentDiskForLogArchiving.Text))
            {
                txtMaxPercentDiskForLogArchiving.Text = ApplicationConstants.LOG_ARCHIVING_MAX_DISK_PERCENTAGE.ToString();
            }
            else
            {
                int iMaxPercentDiskForLogArchiving = 0;

                if (int.TryParse(txtMaxPercentDiskForLogArchiving.Text, out iMaxPercentDiskForLogArchiving))
                {
                    if (iMaxPercentDiskForLogArchiving <= 0 || iMaxPercentDiskForLogArchiving > 40)
                    {
                        txtMaxPercentDiskForLogArchiving.Text = ApplicationConstants.LOG_ARCHIVING_MAX_DISK_PERCENTAGE.ToString();
                    }
                }
                else
                {
                    txtMaxPercentDiskForLogArchiving.Text = ApplicationConstants.LOG_ARCHIVING_MAX_DISK_PERCENTAGE.ToString();
                }
            }
        }

        private void txtMaxArchiveAgeIfDiskSpaceNeeded_TextChanged(object sender, EventArgs e)
        {
            if (Changed != null)
            {
                Changed(this, new EventArgs());
            }
        }

        private void txtMaxArchiveAgeIfDiskSpaceNeeded_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaxArchiveAgeIfDiskSpaceNeeded.Text))
            {
                txtMaxArchiveAgeIfDiskSpaceNeeded.Text = ApplicationConstants.LOG_ARCHIVING_MAXIMUM_AGE_IN_DAYS_IF_DISK_SPACE_NEEDED.ToString();
            }
            else
            {
                int iMaxArchiveAgeInDaysIfDiskSpaceNeeded = 0;

                if (int.TryParse(txtMaxArchiveAgeIfDiskSpaceNeeded.Text, out iMaxArchiveAgeInDaysIfDiskSpaceNeeded))
                {
                    if (iMaxArchiveAgeInDaysIfDiskSpaceNeeded <= 0 || iMaxArchiveAgeInDaysIfDiskSpaceNeeded > 90)
                    {
                        txtMaxArchiveAgeIfDiskSpaceNeeded.Text = ApplicationConstants.LOG_ARCHIVING_MAXIMUM_AGE_IN_DAYS_IF_DISK_SPACE_NEEDED.ToString();
                    }
                }
                else
                {
                    txtMaxArchiveAgeIfDiskSpaceNeeded.Text = ApplicationConstants.LOG_ARCHIVING_MAXIMUM_AGE_IN_DAYS_IF_DISK_SPACE_NEEDED.ToString();
                }
            }
        }

        private void txtMaxArchiveAgeIfDiskSpaceNeeded_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
