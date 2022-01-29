using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace com.workflowconcepts.applications.filemonitor
{
    public partial class Service : ServiceBase
    {
        ApplicationCore application = null;

        public Service()
        {
            InitializeComponent();

            application = new ApplicationCore();
        }

        protected override void OnStart(string[] args)
        {
            Log.Instance.Debug($"Enter");

            application.OnStart(args);
        }

        protected override void OnStop()
        {
            Log.Instance.Debug($"Enter");

            application.OnStop();
        }

        protected override void OnPause()
        {
            Log.Instance.Debug($"Enter");

            base.OnPause();
        }

        protected override void OnShutdown()
        {
            Log.Instance.Debug($"Enter");

            base.OnShutdown();
        }
    }
}
