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
    public partial class ucRemotingInformation : UserControl
    {
        public event EventHandler Changed;

        public ucRemotingInformation()
        {
            InitializeComponent();
        }

        private void ucRemotingInformation_Load(object sender, EventArgs e)
        {

        }

        private void txtPort_TextChanged(object sender, EventArgs e)
        {
           Log.Instance.Info("Enter.");

            if (Changed != null)
            {
                Changed(this, new EventArgs());
            }
        }

        private void txtPort_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtPrefix_TextChanged(object sender, EventArgs e)
        {
           Log.Instance.Info("Enter.");

            if (Changed != null)
            {
                Changed(this, new EventArgs());
            }
        }
    }
}
