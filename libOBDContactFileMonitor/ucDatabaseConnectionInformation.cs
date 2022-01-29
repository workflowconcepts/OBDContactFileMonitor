using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace com.workflowconcepts.applications.filemonitor
{
    public partial class ucDatabaseConnectionInformation : UserControl
    {
        public event EventHandler Changed;
        public ucDatabaseConnectionInformation()
        {
            InitializeComponent();
        }

        private void ucDatabaseConnectionInformation_Load(object sender, EventArgs e)
        {
           Log.Instance.Info("Enter.");

            if (Changed != null)
            {
                Changed(this, new EventArgs());
            }
        }

        private void txtServer_TextChanged(object sender, EventArgs e)
        {
           Log.Instance.Info("Enter.");

            if (Changed != null)
            {
                Changed(this, new EventArgs());
            }
        }

        private void txtDatabase_TextChanged(object sender, EventArgs e)
        {
           Log.Instance.Info("Enter.");

            if (Changed != null)
            {
                Changed(this, new EventArgs());
            }
        }

        private void txtUsername_TextChanged(object sender, EventArgs e)
        {
           Log.Instance.Info("Enter.");

            if (Changed != null)
            {
                Changed(this, new EventArgs());
            }
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
           Log.Instance.Info("Enter.");

            if (Changed != null)
            {
                Changed(this, new EventArgs());
            }
        }

        private void ckbUseIntegratedSecurity_CheckedChanged(object sender, EventArgs e)
        {
           Log.Instance.Info("Enter.");

            if (Changed != null)
            {
                Changed(this, new EventArgs());
            }

            txtUsername.Enabled = !ckbUseIntegratedSecurity.Checked;
            txtPassword.Enabled = !ckbUseIntegratedSecurity.Checked;
        }

        private void btnTestConnection_Click(object sender, EventArgs e)
        {
           Log.Instance.Info("Enter.");

            this.Cursor = Cursors.WaitCursor;

            if (this.txtServer.Text == String.Empty)
            {
                MessageBox.Show("Please provide a valid value for Server name.");

                this.txtServer.Focus();

                return;
            }

            if (this.txtDatabase.Text == String.Empty)
            {
                Log.Instance.Warn("No database name was provided; default database will be assumed.");
            }


            if (!ckbUseIntegratedSecurity.Checked)
            {
                if (this.txtUsername.Text == String.Empty)
                {
                    MessageBox.Show("Please provide a valid value for username.");

                    this.txtUsername.Focus();

                    return;
                }

                if (this.txtPassword.Text == String.Empty)
                {
                    MessageBox.Show("Please provide a valid value for password.");

                    this.txtPassword.Focus();

                    return;
                }
            }

            try
            {
                ApplicationSettings settings = new ApplicationSettings(string.Empty);

                settings.SqlServerHostIp = txtServer.Text;
                settings.SqlDatabaseName = txtDatabase.Text;
                settings.SqlUserName = txtUsername.Text;
                settings.SqlPassword = txtPassword.Text;

                SqlAccess db = new SqlAccess(settings);

                if(db.TestConnection())
                {
                    MessageBox.Show("Successfully connected to database.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Could not connect to database." + Environment.NewLine + "Please review the information provided.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.txtServer.Focus();
                }

                db = null;
                settings = null;
            }
            catch (Exception ex)
            {
                Log.Instance.Error("Exception:" + ex.Message + Environment.NewLine + "StackTrace:" + ex.StackTrace);
            }

            this.Cursor = Cursors.Default;
        }
    }
}
