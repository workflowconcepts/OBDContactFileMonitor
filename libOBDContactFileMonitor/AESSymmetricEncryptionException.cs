using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.workflowconcepts.applications.filemonitor
{
    [Serializable]
    public class AESSymmetricEncryptionException : Exception
    {
        private String _Message = string.Empty;
        private String _StackTrace = string.Empty;

        public String Message
        {
            get { return _Message; }
        }

        public String StackTrace
        {
            get { return _StackTrace; }
        }

        public AESSymmetricEncryptionException(String Message, String StackTrace)
        {
            _Message = Message;
            _StackTrace = StackTrace;
        }
    }
}
