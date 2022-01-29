using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.workflowconcepts.applications.filemonitor
{
    namespace ApplicationTypes
    {
        public enum LogLevel
        {
            TRACE
            , DEBUG
            , INFORMATION
            , WARNING
            , ERROR
            , FATAL
        }

        public enum NotificationType { FAILURE, SUCCESS };

        public enum ApplicationSettingsReturn { FILE_NOT_FOUND, ERROR, SUCCESS, NONE, INVALID_VALUE };

        public interface IEmailSender
        {
            bool Send(string EmailFrom, string EmailTo, string Subject, string Body, string Attachment, ApplicationTypes.NotificationType NotificationType);

            bool Send(string Username, string Subject, string Body, System.Net.Mail.MailPriority Priority);
        }

        public delegate void SettingsCallback(iApplicationSettings Settings);

        public interface iEmailSettings
        {
            bool EmailOnSuccess
            {
                get;
                set;
            }

            bool EmailOnFailure
            {
                get;
                set;
            }

            string EmailFrom
            {
                get;
                set;
            }

            string EmailTo
            {
                get;
                set;
            }

            string SMTPServer
            {
                get;
                set;
            }

            int SMTPPort
            {
                get;
                set;
            }

            string SMTPUserName
            {
                get;
                set;
            }

            string SMTPPassword
            {
                get;
                set;
            }
        }

        public interface iApplicationSettings
        {
            bool EmailOnSuccess
            {
                get;
                set;
            }

            bool EmailOnFailure
            {
                get;
                set;
            }

            bool EmailEnableSSL
            {
                get;
                set;
            }

            string EmailFrom
            {
                get;
                set;
            }

            string EmailTo
            {
                get;
                set;
            }

            string SMTPServer
            {
                get;
                set;
            }

            int SMTPPort
            {
                get;
                set;
            }

            string SMTPUserName
            {
                get;
                set;
            }

            string SMTPPassword
            {
                get;
                set;
            }

            bool Debug
            {
                get;
                set;
            }

            string DebugLevel
            {
                get;
                set;
            }

            int DebugRetainValue
            {
                get;
                set;
            }

            string DebugFileSize
            {
                get;
                set;
            }

            bool Load();

            bool Save();

            bool ParseArgs(string[] args);
        }

        public interface iCounter
        {
            int Count();
        }

        public delegate void EmailNotifierDelegate(string Subject, string Body, ApplicationTypes.NotificationType NotificationType);

    }
}
