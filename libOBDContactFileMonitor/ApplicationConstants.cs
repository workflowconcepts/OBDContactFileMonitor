using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.workflowconcepts.applications.filemonitor
{
    public class ApplicationConstants
    {
        public const string SERVICEDISPLAYNAME = "OBD Contact File Monitor";
        public const string PROCESSNAME = "OBDContactFileMonitor";

        public const string APPLICATIONSETTINGSFILENAME = "ApplicationSettings.json";

        public const string ENCRYPTION_PASSWORD = "OmniXpressServer@WorkflowConcepts";
        public const string ENCRYPTION_SALT = "m$9~YJ%th}vaKBFz|7-ViUMQJn;Xi]p8%Ac?K0m6@i#wS4!!of&#B/|F*qi)%yrE";
        public const string ENCRYPTION_INITIALIZATION_VECTOR = "mTIFzipkBM3pWO4d";

        public const string COMPANY_WEB_SITE = "http://www.workflowconcepts.com";

        public const int SMTP_PORT = 587;

        public const int IPC_PORT = 8500;
        public const string IPC_URI = "ServicesManagment";

        public const int LOG_ARCHIVING_NUMBER_OF_LOG_FILES_PER_ARCHIVE = 100;
        public const int LOG_ARCHIVING_MAX_DISK_PERCENTAGE = 20;
        public const int LOG_ARCHIVING_MAXIMUM_AGE_IN_DAYS_IF_DISK_SPACE_NEEDED = 15;

        public static string ApplicationSettingsFilePath
        {
            get
            {
                //THis value is based on CompanyName and ProdcutName
                return System.Windows.Forms.Application.CommonAppDataPath.Substring(0, System.Windows.Forms.Application.CommonAppDataPath.LastIndexOf("\\"));
            }
        }

        public static string GetIPv4AddressForCurrentHost
        {
            get
            {
                System.Net.IPHostEntry host = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName());

                foreach (System.Net.IPAddress ip in host.AddressList)
                {
                    if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                    {
                        return ip.ToString();
                    }
                }

                return string.Empty;
            }
        }
    }
}
