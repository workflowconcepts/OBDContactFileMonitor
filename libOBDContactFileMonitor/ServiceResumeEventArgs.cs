using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.workflowconcepts.applications.filemonitor
{
    public class ServiceResumeEventArgs : EventArgs
    {
        private string _Description = string.Empty;
        private string _EntityName = string.Empty;

        public string Description
        {
            get { return _Description; }
            set { _Description = string.Empty; }
        }

        public string EntityName
        {
            get { return _EntityName; }
            set { _EntityName = string.Empty; }
        }

        public ServiceResumeEventArgs()
        {
            _EntityName = string.Empty;
            _Description = string.Empty;
        }

        public ServiceResumeEventArgs(string EntityName, string Description)
        {
            _EntityName = EntityName;
            _Description = Description;
        }
    }
}
