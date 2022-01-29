using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.workflowconcepts.applications.filemonitor
{
    public class EmailNotifier : ApplicationTypes.IEmailSender
    {
        private string _SMTPServer = string.Empty;
        private int _SMTPPort = 0;
        private string _SMTPUserName = string.Empty;
        private string _SMTPPassword = string.Empty;
        private bool _SMTPEnableSSL = false;

        private string _DefaultEmailDomain = string.Empty;

        private bool _EmailOnFailure = false;
        private bool _EmailOnSuccess = false;

        public string SMTPServer
        {
            get { return _SMTPServer; }
            set { _SMTPServer = value; }
        }

        public int SMTPPort
        {
            get { return _SMTPPort; }
            set { _SMTPPort = value; }
        }

        public string SMTPUserName
        {
            get { return _SMTPUserName; }
            set { _SMTPUserName = value; }
        }

        public string SMTPPassword
        {
            get { return _SMTPPassword; }
            set { _SMTPPassword = value; }
        }

        public bool EmailOnFailure
        {
            get { return _EmailOnFailure; }
            set { _EmailOnFailure = value; }
        }

        public bool EmailOnSuccess
        {
            get { return _EmailOnSuccess; }
            set { _EmailOnSuccess = value; }
        }

        public bool _EnableSSL
        {
            get { return _SMTPEnableSSL; }
            set { _SMTPEnableSSL = value; }
        }

        public string DefaultEmailDomain { get => _DefaultEmailDomain; set => _DefaultEmailDomain = value; }

        public EmailNotifier()
        {
            _SMTPServer = string.Empty;
            _SMTPPort = 0;
            _SMTPUserName = string.Empty;
            _SMTPPassword = string.Empty;

            _SMTPEnableSSL = false;

            _EmailOnFailure = false;
            _EmailOnSuccess = false;

            _DefaultEmailDomain = string.Empty;
        }

        public EmailNotifier(string SMTPServer, int SMTPPort, bool EnableSSL, string User, string Password, bool EmailOnSuccess, bool EmailOnFailure)
        {
            _SMTPServer = SMTPServer;
            _SMTPPort = SMTPPort;
            _SMTPUserName = User;
            _SMTPPassword = Password;

            _SMTPEnableSSL = EnableSSL;

            _EmailOnFailure = EmailOnFailure;
            _EmailOnSuccess = EmailOnSuccess;

            _DefaultEmailDomain = string.Empty;
        }

        public EmailNotifier(string SMTPServer, int SMTPPort, bool EnableSSL, string User, string Password, bool EmailOnSuccess, bool EmailOnFailure, string DefaultEmailDomain)
        {
            _SMTPServer = SMTPServer;
            _SMTPPort = SMTPPort;
            _SMTPUserName = User;
            _SMTPPassword = Password;

            _SMTPEnableSSL = EnableSSL;

            _EmailOnFailure = EmailOnFailure;
            _EmailOnSuccess = EmailOnSuccess;

            _DefaultEmailDomain = DefaultEmailDomain;
        }

        public bool Send(string EmailFrom, string EmailTo, string Subject, string Body, string Attachment, ApplicationTypes.NotificationType NotificationType)
        {
            Log.Instance.Debug("Enter.");

            try
            {
                System.Net.Mail.MailPriority Priority = System.Net.Mail.MailPriority.Normal;

                switch (NotificationType)
                {
                    case ApplicationTypes.NotificationType.FAILURE:

                        if (!_EmailOnFailure)
                        {
                            Log.Instance.Warn("Email on failure is disabled.");
                            return false;
                        }

                        Priority = System.Net.Mail.MailPriority.High;

                        break;

                    case ApplicationTypes.NotificationType.SUCCESS:

                        if (!_EmailOnSuccess)
                        {
                            Log.Instance.Warn("Email on success is disabled.");
                            return false;
                        }

                        break;
                }

                if (string.IsNullOrEmpty(EmailFrom))
                {
                    Log.Instance.Warn($"{nameof(EmailFrom)} is null/empty -> assume _SMTPUserName");
                    EmailFrom = _SMTPUserName;
                }

                if (string.IsNullOrEmpty(EmailTo))
                {
                    Log.Instance.Warn($"{nameof(EmailTo)} is null/empty -> assume _SMTPUserName");
                    EmailTo = _SMTPUserName;
                }

                if (string.IsNullOrEmpty(_SMTPServer))
                {
                    Log.Instance.Warn($"{nameof(_SMTPServer)} is null/empty");
                    return false;
                }

                if (!Utilities.ValidatePortNumber(_SMTPPort))
                {
                    Log.Instance.Warn($"SMTPPort value is invalid {_SMTPPort}");
                    return false;
                }

                System.Net.Mail.MailMessage _email = new System.Net.Mail.MailMessage();

                _email.Subject = Subject;
                _email.Body = Body;
                _email.Priority = Priority;

                _email.From = new System.Net.Mail.MailAddress(EmailFrom);

                foreach (string _ToAddress in EmailTo.Split(';'))
                {
                    _email.To.Add(new System.Net.Mail.MailAddress(_ToAddress));
                }

                System.Net.Mail.SmtpClient _SMTPClient = new System.Net.Mail.SmtpClient(_SMTPServer, _SMTPPort);

                if (_SMTPUserName.Length > 0)
                {
                    _SMTPClient.Credentials = new System.Net.NetworkCredential(_SMTPUserName, _SMTPPassword);
                }
                else
                {
                    //Log.Instance.Debug("No UserName was specified for the SMTP server.");
                }

                _SMTPClient.EnableSsl = _SMTPEnableSSL;

                if (!String.IsNullOrEmpty(Attachment))
                {
                    if (System.IO.File.Exists(Attachment))
                    {
                        System.Net.Mail.Attachment attachment;
                        attachment = new System.Net.Mail.Attachment(Attachment);
                        _email.Attachments.Add(attachment);
                        attachment = null;
                    }
                }

                _SMTPClient.Timeout = 20000;

                _SMTPClient.Send(_email);

                _email.Dispose();
                _email = null;

                //_SMTPClient.Dispose();
                _SMTPClient = null;

                return true;
            }
            catch (Exception ex)
            {
                Log.Instance.Error("Exception:" + ex.Message + Environment.NewLine + "StackTrace:" + ex.StackTrace);
                return false;
            }
        }

        public bool Send(string Username, string Subject, string Body, System.Net.Mail.MailPriority Priority)
        {
            try
            {
                if (string.IsNullOrEmpty(Username))
                {
                    Log.Instance.Warn($"{nameof(Username)} is null/empty");
                    return false;
                }

                if (string.IsNullOrEmpty(Subject))
                {
                    Log.Instance.Warn($"{nameof(Subject)} is null/empty");
                    return false;
                }

                if (string.IsNullOrEmpty(Body))
                {
                    Log.Instance.Warn($"{nameof(Body)} is null/empty -> default to empty");
                    Body = string.Empty;
                }

                if (string.IsNullOrEmpty(_SMTPServer))
                {
                    Log.Instance.Warn($"{nameof(_SMTPServer)} is null/empty");
                    return false;
                }

                if (!Utilities.ValidatePortNumber(_SMTPPort))
                {
                    Log.Instance.Warn($"SMTPPort value is invalid {_SMTPPort}");
                    return false;
                }

                System.Net.Mail.MailMessage _email = new System.Net.Mail.MailMessage();

                _email.Subject = Subject;
                _email.Body = Body;
                _email.Priority = Priority;

                _email.From = new System.Net.Mail.MailAddress(_SMTPUserName);

                System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
                System.Text.RegularExpressions.Match match = regex.Match(Username);

                regex = null;

                if (match.Success)
                {
                    Log.Instance.Debug($"Username:{Username} is a valid email; no need to add default domain");

                    match = null;
                }
                else
                {
                    Log.Instance.Warn($"Username:{Username} is NOT a valid email -> add default domain");

                    match = null;

                    if (string.IsNullOrEmpty(_DefaultEmailDomain))
                    {
                        Log.Instance.Warn($"{nameof(_DefaultEmailDomain)} is null/empty");
                        return false;
                    }

                    Username = Username + _DefaultEmailDomain;

                    Log.Instance.Debug($"Username after adding default domain {Username}");
                }

                foreach (string _ToAddress in Username.Split(';'))
                {
                    _email.To.Add(new System.Net.Mail.MailAddress(_ToAddress));
                }

                System.Net.Mail.SmtpClient _SMTPClient = new System.Net.Mail.SmtpClient(_SMTPServer, _SMTPPort);

                if (_SMTPUserName.Length > 0)
                {
                    _SMTPClient.Credentials = new System.Net.NetworkCredential(_SMTPUserName, _SMTPPassword);
                }
                else
                {
                    //Log.Instance.Debug("No UserName was specified for the SMTP server.");
                }

                _SMTPClient.EnableSsl = _SMTPEnableSSL;

                _SMTPClient.Timeout = 20000;

                _SMTPClient.Send(_email);

                _email.Dispose();
                _email = null;

                //_SMTPClient.Dispose();
                _SMTPClient = null;

                return true;
            }
            catch(Exception ex)
            {
                Log.Instance.Error("Exception:" + ex.Message + Environment.NewLine + "StackTrace:" + ex.StackTrace);
                return false;
            }
        }

    }
}
