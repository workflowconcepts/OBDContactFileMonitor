using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.workflowconcepts.applications.filemonitor
{
    [Serializable]
    public class ApplicationSettings : ApplicationTypes.iApplicationSettings
    {
        AESSymmetricEncryption encDec = null;

        string _FILEPATH = string.Empty;

        //Email
        string _EmailFrom = string.Empty;
        string _EmailTo = string.Empty;
        string _EmailSMTPServer = string.Empty;
        int _EmailSMTPPort = 0;
        string _EmailSMTPUserName = string.Empty;
        string _EmailSMTPPassword = string.Empty;
        bool _EmailEnableSSL = false;
        bool _EmailOnFailure = false;
        bool _EmailOnSuccess = false;

        //Remoting
        int _Remoting_Port = 0;
        string _Remoting_Prefix = string.Empty;

        //Debug
        bool _DebugEnabled = false;
        string _DebugLevel = string.Empty;
        int _DebugRetainValue = 0;
        string _DebugFileSize = string.Empty;
        bool _LogArchivingEnabled = false;
        int _LogFilesPerArchive = 0;
        int _MaxDiskPercentageForLogArchiving = 0;
        int _MaximumArchiveAgeInDaysIfDiskSpaceNeeded = 0;

        public bool EmailEnableSSL
        {
            get { return _EmailEnableSSL; }
            set { _EmailEnableSSL = value; }
        }

        public bool EmailOnSuccess
        {
            get { return _EmailOnSuccess; }
            set { _EmailOnSuccess = value; }
        }

        public bool EmailOnFailure
        {
            get { return _EmailOnFailure; }
            set { _EmailOnFailure = value; }
        }

        public string EmailFrom
        {
            get { return _EmailFrom; }
            set { _EmailFrom = value; }
        }

        public string EmailTo
        {
            get { return _EmailTo; }
            set { _EmailTo = value; }
        }

        public string SMTPServer
        {
            get { return _EmailSMTPServer; }
            set { _EmailSMTPServer = value; }
        }

    //rjm 1/25/2022 START
        public string InputDirectory
        {
            get; set;
        }
        public bool ArchiveFiles
        {
            get;set;
        }
        public string ArchiveDirectory
        {
            get; set;
        }
        public string SqlCommand
        {
            get;

            set;
        }
        public string SqlServerHostIp
        {
            get;set;
        }
        public string SqlInstanceName
        {
            get;set;
        }
        public string SqlDatabaseName
        {
            get;set;
        }
        public string SqlUserName
        {
            get;set;
        }
        public string SqlPassword
        {
            get;set;
        }
    //rjm 1/25/2022 END

        public int SMTPPort
        {
            get
            {
                if (Utilities.ValidatePortNumber(_EmailSMTPPort))
                {
                    return _EmailSMTPPort;
                }
                else
                {
                    return ApplicationConstants.SMTP_PORT;
                }
            }

            set
            {
                if (Utilities.ValidatePortNumber(value))
                {
                    _EmailSMTPPort = value;
                }
                else
                {
                    _EmailSMTPPort = ApplicationConstants.SMTP_PORT;
                }
            }
        }

        public string SMTPUserName
        {
            get { return _EmailSMTPUserName; }
            set { _EmailSMTPUserName = value; }
        }

        public string SMTPPassword
        {
            get { return _EmailSMTPPassword; }
            set { _EmailSMTPPassword = value; }
        }

        public int Remoting_Port
        {
            get
            {
                if (Utilities.ValidatePortNumber(_Remoting_Port))
                {
                    return _Remoting_Port;
                }
                else
                {
                    return ApplicationConstants.IPC_PORT;
                }
            }

            set
            {
                if (Utilities.ValidatePortNumber(value))
                {
                    _Remoting_Port = value;
                }
                else
                {
                    _Remoting_Port = ApplicationConstants.IPC_PORT;
                }
            }
        }

        public string Remoting_Prefix
        {
            get
            {
                if (string.IsNullOrEmpty(_Remoting_Prefix))
                {
                    return ApplicationConstants.IPC_URI;
                }
                else
                {
                    return _Remoting_Prefix;
                }
            }

            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    _Remoting_Prefix = ApplicationConstants.IPC_URI;
                }
                else
                {
                    _Remoting_Prefix = value;
                }
            }
        }

        public bool Debug
        {
            get { return _DebugEnabled; }
            set { _DebugEnabled = value; }
        }

        public string DebugLevel
        {
            get { return _DebugLevel; }
            set { _DebugLevel = value; }
        }

        public int DebugRetainValue
        {
            get { return _DebugRetainValue; }
            set { _DebugRetainValue = value; }
        }

        public string DebugFileSize
        {
            get { return _DebugFileSize; }
            set { _DebugFileSize = value; }
        }

        public bool ArchivingEnabled
        {
            get { return _LogArchivingEnabled; }
            set { _LogArchivingEnabled = value; }
        }

        public int LogFilesPerArchive
        {
            get
            {
                if (_LogFilesPerArchive <= 0)
                {
                    return ApplicationConstants.LOG_ARCHIVING_NUMBER_OF_LOG_FILES_PER_ARCHIVE;
                }
                else
                {
                    return _LogFilesPerArchive;
                }
            }

            set
            {
                if (value <= 0)
                {
                    _LogFilesPerArchive = ApplicationConstants.LOG_ARCHIVING_NUMBER_OF_LOG_FILES_PER_ARCHIVE;
                }
                else
                {
                    _LogFilesPerArchive = value;
                }
            }
        }

        public int MaxDiskPercentageForLogArchiving
        {
            get
            {
                if (_MaxDiskPercentageForLogArchiving <= 0 || _MaxDiskPercentageForLogArchiving > 40)
                {
                    return ApplicationConstants.LOG_ARCHIVING_MAX_DISK_PERCENTAGE;
                }
                else
                {
                    return _MaxDiskPercentageForLogArchiving;
                }
            }

            set
            {
                if (value <= 0 || value > 40)
                {
                    _MaxDiskPercentageForLogArchiving = ApplicationConstants.LOG_ARCHIVING_MAX_DISK_PERCENTAGE;
                }
                else
                {
                    _MaxDiskPercentageForLogArchiving = value;
                }
            }
        }

        public int MaximumArchiveAgeInDaysIfDiskSpaceNeeded
        {
            get
            {
                if (_MaximumArchiveAgeInDaysIfDiskSpaceNeeded <= 0 || _MaximumArchiveAgeInDaysIfDiskSpaceNeeded > 90)
                {
                    return ApplicationConstants.LOG_ARCHIVING_MAXIMUM_AGE_IN_DAYS_IF_DISK_SPACE_NEEDED;
                }
                else
                {
                    return _MaximumArchiveAgeInDaysIfDiskSpaceNeeded;
                }
            }

            set
            {
                if (value <= 0 || value > 90)
                {
                    _MaximumArchiveAgeInDaysIfDiskSpaceNeeded = ApplicationConstants.LOG_ARCHIVING_MAXIMUM_AGE_IN_DAYS_IF_DISK_SPACE_NEEDED;
                }
                else
                {
                    _MaximumArchiveAgeInDaysIfDiskSpaceNeeded = value;
                }
            }
        }

        public ApplicationSettings(string FilePath)
        {
            _FILEPATH = FilePath;

            _DebugEnabled = true;
            _DebugLevel = "Information";
            _DebugRetainValue = 100;
            _DebugFileSize = "ThreeMegaBytes";
            _LogArchivingEnabled = true;
            _LogFilesPerArchive = ApplicationConstants.LOG_ARCHIVING_NUMBER_OF_LOG_FILES_PER_ARCHIVE;
            _MaxDiskPercentageForLogArchiving = ApplicationConstants.LOG_ARCHIVING_MAX_DISK_PERCENTAGE;
            _MaximumArchiveAgeInDaysIfDiskSpaceNeeded = ApplicationConstants.LOG_ARCHIVING_MAXIMUM_AGE_IN_DAYS_IF_DISK_SPACE_NEEDED;
        }

        public bool Load()
        {
            Log.Instance.Debug("Enter.");

            try
            {
                if (!System.IO.File.Exists(_FILEPATH + "\\" + ApplicationConstants.APPLICATIONSETTINGSFILENAME))
                {
                    Log.Instance.Warn(_FILEPATH + "\\" + ApplicationConstants.APPLICATIONSETTINGSFILENAME + " was not found.");
                    return false;
                }

                string sSettings = System.IO.File.ReadAllText(_FILEPATH + "\\" + ApplicationConstants.APPLICATIONSETTINGSFILENAME);

                if (string.IsNullOrEmpty(sSettings))
                {
                    Log.Instance.Warn("Settings file contents is either null or empty");
                    return false;
                }

                dynamic settings = Newtonsoft.Json.JsonConvert.DeserializeObject<Newtonsoft.Json.Linq.JObject>(sSettings);

                if (settings == null)
                {
                    Log.Instance.Warn("Deserialized settings object is null");
                    return false;
                }

                encDec = new AESSymmetricEncryption(ApplicationConstants.ENCRYPTION_PASSWORD, ApplicationConstants.ENCRYPTION_SALT, ApplicationConstants.ENCRYPTION_INITIALIZATION_VECTOR);
        
        //rjm 1/25/2022 START
                // File monitoring
                if (settings.file != null)
                {
                    if (settings.file.inputfiledirectory != null)
                    {
                        InputDirectory = ((Newtonsoft.Json.Linq.JToken)settings.file).Value<string>("inputfiledirectory");
                    }
                    if (settings.file.savetoarchivefolder != null)
                    {
                        ArchiveFiles = ((Newtonsoft.Json.Linq.JToken)settings.file).Value<bool>("savetoarchivefolder");
                    }
                    if (settings.file.archivedfiledirectory != null)
                    {
                        ArchiveDirectory = ((Newtonsoft.Json.Linq.JToken)settings.file).Value<string>("archivedfiledirectory");
                    }
                }
                else
                {
                    InputDirectory = "c:\\";
                    ArchiveFiles = false;
                    ArchiveDirectory = "c:\\";
                }

                //SQL Database
                if (settings.database != null)
                {
                    if (settings.database.sqlserverhostip != null)
                    { 
                        SqlServerHostIp = encDec.Decrypt(((Newtonsoft.Json.Linq.JToken)settings.database).Value<string>("sqlserverhostip"));
                    }
                    if (settings.database.sqlinstancename != null)
                    { 
                        SqlInstanceName = encDec.Decrypt(((Newtonsoft.Json.Linq.JToken)settings.database).Value<string>("sqlinstancename"));
                    }
                    if (settings.database.sqldatabasename != null)
                    { 
                        SqlDatabaseName = encDec.Decrypt(((Newtonsoft.Json.Linq.JToken)settings.database).Value<string>("sqldatabasename"));
                    }
                    if (settings.database.sqlusername != null)
                    {
                        SqlUserName = encDec.Decrypt(((Newtonsoft.Json.Linq.JToken)settings.database).Value<string>("sqlusername"));
                    }
                    if (settings.database.sqlpassword != null)
                    {
                        SqlPassword = encDec.Decrypt(((Newtonsoft.Json.Linq.JToken)settings.database).Value<string>("sqlpassword"));
                    }
                    if (settings.database.sqlcommand != null)
                    {
                        SqlCommand = encDec.Decrypt(((Newtonsoft.Json.Linq.JToken)settings.database).Value<string>("sqlcommand"));

                        if(string.IsNullOrEmpty(SqlCommand))
                        {
                            SqlCommand = ApplicationConstants.DATABASE_DEFAULT_COMMAND;
                        }
                    }
                }
        //rjm 1/25/2022 END
                
                //Remoting
                if (settings.remoting != null)
                {
                    if (settings.remoting.port != null)
                    {
                        _Remoting_Port = ((Newtonsoft.Json.Linq.JToken)settings.remoting).Value<int>("port");
                    }
                    else
                    {
                        Log.Instance.Warn("Property settings.remoting.port was not found; assume default value.");

                        _Remoting_Port = ApplicationConstants.IPC_PORT;
                    }

                    if (settings.remoting.prefix != null)
                    {
                        _Remoting_Prefix = ((Newtonsoft.Json.Linq.JToken)settings.remoting).Value<string>("prefix");
                    }
                    else
                    {
                        Log.Instance.Warn("Property settings.remoting.prefix was not found; assume default value.");

                        _Remoting_Prefix = ApplicationConstants.IPC_URI;
                    }
                }
                else
                {
                    Log.Instance.Warn("Deserialized settings.remoting object is null");

                    _Remoting_Port = ApplicationConstants.IPC_PORT;
                    _Remoting_Prefix = ApplicationConstants.IPC_URI;
                }

                //Email
                if (settings.email != null)
                {
                    if (settings.email.from != null)
                    {
                        _EmailFrom = encDec.Decrypt(((Newtonsoft.Json.Linq.JToken)settings.email).Value<string>("from"));
                    }
                    else
                    {
                        Log.Instance.Warn("Property settings.email.from was not found; assume default value.");

                        _EmailFrom = string.Empty;
                    }

                    if (settings.email.to != null)
                    {
                        _EmailTo = encDec.Decrypt(((Newtonsoft.Json.Linq.JToken)settings.email).Value<string>("to"));
                    }
                    else
                    {
                        Log.Instance.Warn("Property settings.email.to was not found; assume default value.");

                        _EmailTo = string.Empty;
                    }

                    if (settings.email.server != null)
                    {
                        try
                        {
                            _EmailSMTPServer = encDec.Decrypt(((Newtonsoft.Json.Linq.JToken)settings.email).Value<string>("server"));
                        }
                        catch(Exception ex)
                        {
                            Log.Instance.Warn("Exception:" + ex.Message + Environment.NewLine + "StackTrace:" + ex.StackTrace);

                            _EmailSMTPServer = string.Empty;
                        }
                    }
                    else
                    {
                        Log.Instance.Warn("Property settings.email.server was not found; assume default value.");

                        _EmailSMTPServer = string.Empty;
                    }

                    if (settings.email.port != null)
                    {
                        try
                        {
                            string sPort = encDec.Decrypt(((Newtonsoft.Json.Linq.JToken)settings.email).Value<string>("port"));

                            int iPort = 0;

                            if (Utilities.ValidatePortNumber(sPort, out iPort))
                            {
                                _EmailSMTPPort = iPort;
                            }
                            else
                            {
                                _EmailSMTPPort = ApplicationConstants.SMTP_PORT;
                            }
                        }
                        catch (Exception ex)
                        {
                            Log.Instance.Warn("Exception:" + ex.Message + Environment.NewLine + "StackTrace:" + ex.StackTrace);

                            _EmailSMTPPort = ApplicationConstants.SMTP_PORT;
                        }
                    }
                    else
                    {
                        Log.Instance.Warn("Property settings.email.port was not found; assume default value.");

                        _EmailSMTPPort = ApplicationConstants.SMTP_PORT;
                    }

                    if (settings.email.user != null)
                    {
                        try
                        {
                            _EmailSMTPUserName = encDec.Decrypt(((Newtonsoft.Json.Linq.JToken)settings.email).Value<string>("user"));
                        }
                        catch (Exception ex)
                        {
                            Log.Instance.Warn("Exception:" + ex.Message + Environment.NewLine + "StackTrace:" + ex.StackTrace);

                            _EmailSMTPUserName = string.Empty;
                        }
                    }
                    else
                    {
                        Log.Instance.Warn("Property settings.email.user was not found; assume default value.");

                        _EmailSMTPUserName = string.Empty;
                    }

                    if (settings.email.password != null)
                    {
                        try
                        {
                            _EmailSMTPPassword = encDec.Decrypt(((Newtonsoft.Json.Linq.JToken)settings.email).Value<string>("password"));
                        }
                        catch (Exception ex)
                        {
                            Log.Instance.Warn("Exception:" + ex.Message + Environment.NewLine + "StackTrace:" + ex.StackTrace);

                            _EmailSMTPPassword = string.Empty;
                        }
                    }
                    else
                    {
                        Log.Instance.Warn("Property settings.email.password was not found; assume default value.");

                        _EmailSMTPPassword = string.Empty;
                    }

                    if (settings.email.emailonfailure != null)
                    {
                        _EmailOnFailure = ((Newtonsoft.Json.Linq.JToken)settings.email).Value<bool>("emailonfailure");
                    }
                    else
                    {
                        Log.Instance.Warn("Property settings.email.emailonfailure was not found; assume default value.");

                        _EmailOnFailure = false;
                    }

                    if (settings.email.emailonsuccess != null)
                    {
                        _EmailOnSuccess = ((Newtonsoft.Json.Linq.JToken)settings.email).Value<bool>("emailonsuccess");
                    }
                    else
                    {
                        Log.Instance.Warn("Property settings.email.emailonsuccess was not found; assume default value.");

                        _EmailOnSuccess = false;
                    }

                    if (settings.email.enablessl != null)
                    {
                        _EmailEnableSSL = ((Newtonsoft.Json.Linq.JToken)settings.email).Value<bool>("enablessl");
                    }
                    else
                    {
                        Log.Instance.Warn("Property settings.email.enablessl was not found; assume default value.");

                        _EmailEnableSSL = false;
                    }
                }
                else
                {
                    Log.Instance.Warn("Deserialized settings.email object is null");

                    _EmailFrom = string.Empty;
                    _EmailTo = string.Empty;
                    _EmailSMTPServer = string.Empty;
                    _EmailSMTPPort = ApplicationConstants.SMTP_PORT;
                    _EmailSMTPUserName = string.Empty;
                    _EmailSMTPPassword = string.Empty;
                    _EmailEnableSSL = false;
                    _EmailOnFailure = false;
                    _EmailOnSuccess = false;
                }

                //Debug
                if (settings.debug != null)
                {
                    if (settings.debug.enabled != null)
                    {
                        _DebugEnabled = ((Newtonsoft.Json.Linq.JToken)settings.debug).Value<bool>("enabled");
                    }
                    else
                    {
                        Log.Instance.Warn("Property settings.debug.enabled was not found; assume default value.");

                        _DebugEnabled = true;
                    }

                    if (settings.debug.level != null)
                    {
                        _DebugLevel = ((Newtonsoft.Json.Linq.JToken)settings.debug).Value<string>("level");
                    }
                    else
                    {
                        Log.Instance.Warn("Property settings.debug.level was not found; assume default value.");

                        _DebugLevel = "Information";
                    }

                    if (settings.debug.retainvalue != null)
                    {
                        _DebugRetainValue = ((Newtonsoft.Json.Linq.JToken)settings.debug).Value<int>("retainvalue");
                    }
                    else
                    {
                        Log.Instance.Warn("Property settings.debug.retainvalue was not found; assume default value.");

                        _DebugRetainValue = 100;
                    }

                    if (settings.debug.filezise != null)
                    {
                        _DebugFileSize = ((Newtonsoft.Json.Linq.JToken)settings.debug).Value<string>("filezise");
                    }
                    else
                    {
                        Log.Instance.Warn("Property settings.debug.filezise was not found; assume default value.");

                        _DebugFileSize = "ThreeMegaBytes";
                    }

                    if (settings.debug.logarchivingenabled != null)
                    {
                        _LogArchivingEnabled = ((Newtonsoft.Json.Linq.JToken)settings.debug).Value<bool>("logarchivingenabled");
                    }
                    else
                    {
                        Log.Instance.Warn("Property settings.debug.logarchivingenabled was not found; assume default value.");

                        _LogArchivingEnabled = true;
                    }

                    if (settings.debug.logfilesperarchive != null)
                    {
                        _LogFilesPerArchive = ((Newtonsoft.Json.Linq.JToken)settings.debug).Value<int>("logfilesperarchive");
                    }
                    else
                    {
                        Log.Instance.Warn("Property settings.debug.logfilesperarchive was not found; assume default value.");

                        _LogFilesPerArchive = ApplicationConstants.LOG_ARCHIVING_NUMBER_OF_LOG_FILES_PER_ARCHIVE;
                    }

                    if (settings.debug.maxpercentageofdisk != null)
                    {
                        _MaxDiskPercentageForLogArchiving = ((Newtonsoft.Json.Linq.JToken)settings.debug).Value<int>("maxpercentageofdisk");
                    }
                    else
                    {
                        Log.Instance.Warn("Property settings.debug.maxpercentageofdisk was not found; assume default value.");

                        _MaxDiskPercentageForLogArchiving = ApplicationConstants.LOG_ARCHIVING_MAX_DISK_PERCENTAGE;
                    }

                    if (settings.debug.maxarchiveageindaysifdiskspaceneeded != null)
                    {
                        _MaximumArchiveAgeInDaysIfDiskSpaceNeeded = ((Newtonsoft.Json.Linq.JToken)settings.debug).Value<int>("maxarchiveageindaysifdiskspaceneeded");
                    }
                    else
                    {
                        Log.Instance.Warn("Property settings.debug.maxarchiveageindaysifdiskspaceneeded was not found; assume default value.");

                        _MaximumArchiveAgeInDaysIfDiskSpaceNeeded = ApplicationConstants.LOG_ARCHIVING_MAXIMUM_AGE_IN_DAYS_IF_DISK_SPACE_NEEDED;
                    }
                }
                else
                {
                    Log.Instance.Warn("Deserialized settings.debug object is null");

                    _DebugEnabled = true;
                    _DebugLevel = "Information";
                    _DebugRetainValue = 100;
                    _DebugFileSize = "ThreeMegaBytes";
                    _LogArchivingEnabled = true;
                    _LogFilesPerArchive = ApplicationConstants.LOG_ARCHIVING_NUMBER_OF_LOG_FILES_PER_ARCHIVE;
                    _MaxDiskPercentageForLogArchiving = ApplicationConstants.LOG_ARCHIVING_MAX_DISK_PERCENTAGE;
                    _MaximumArchiveAgeInDaysIfDiskSpaceNeeded = ApplicationConstants.LOG_ARCHIVING_MAXIMUM_AGE_IN_DAYS_IF_DISK_SPACE_NEEDED;
                }

                encDec = null;

                return true;
            }
            catch (Exception ex)
            {
                Log.Instance.Error("Exception:" + ex.Message + Environment.NewLine + "StackTrace:" + ex.StackTrace);
                return false;
            }
        }

        public bool Save()
        {
            Log.Instance.Debug("Enter.");

            try
            {
                encDec = new AESSymmetricEncryption(ApplicationConstants.ENCRYPTION_PASSWORD, ApplicationConstants.ENCRYPTION_SALT, ApplicationConstants.ENCRYPTION_INITIALIZATION_VECTOR);

                dynamic settings = new Newtonsoft.Json.Linq.JObject();

                //Monitor and Archiving settings
                settings.file = new Newtonsoft.Json.Linq.JObject();

                settings.file.inputfiledirectory = InputDirectory;
                settings.file.savetoarchivefolder = ArchiveFiles;
                settings.file.archivedfiledirectory = ArchiveDirectory;

                //Database
                settings.database = new Newtonsoft.Json.Linq.JObject();

                settings.database.sqlserverhostip = encDec.Encrypt(SqlServerHostIp);
                settings.database.sqlinstancename = encDec.Encrypt(SqlInstanceName);
                settings.database.sqldatabasename = encDec.Encrypt(SqlDatabaseName);
                settings.database.sqlusername = encDec.Encrypt(SqlUserName);
                settings.database.sqlpassword = encDec.Encrypt(SqlPassword);
                settings.database.sqlcommand = encDec.Encrypt(SqlCommand);

                //Remoting
                settings.remoting = new Newtonsoft.Json.Linq.JObject();

                settings.remoting.port = Remoting_Port;
                settings.remoting.prefix = Remoting_Prefix;

                //Email
                settings.email = new Newtonsoft.Json.Linq.JObject();

                settings.email.from = encDec.Encrypt(EmailFrom);
                settings.email.to = encDec.Encrypt(EmailTo);
                settings.email.server = encDec.Encrypt(SMTPServer);
                settings.email.port = encDec.Encrypt(SMTPPort.ToString());
                settings.email.user = encDec.Encrypt(SMTPUserName);
                settings.email.password = encDec.Encrypt(SMTPPassword);
                settings.email.emailonsuccess = EmailOnSuccess;
                settings.email.emailonfailure = EmailOnFailure;
                settings.email.enablessl = EmailEnableSSL;

                //Debug
                settings.debug = new Newtonsoft.Json.Linq.JObject();

                settings.debug.enabled = Debug;
                settings.debug.level = DebugLevel;
                settings.debug.retainvalue = DebugRetainValue;
                settings.debug.filezise = DebugFileSize;
                settings.debug.logarchivingenabled = ArchivingEnabled;
                settings.debug.logfilesperarchive = LogFilesPerArchive;
                settings.debug.maxpercentageofdisk = MaxDiskPercentageForLogArchiving;
                settings.debug.maxarchiveageindaysifdiskspaceneeded = MaximumArchiveAgeInDaysIfDiskSpaceNeeded;

                encDec = null;

                string sSettings = Newtonsoft.Json.JsonConvert.SerializeObject(settings, Newtonsoft.Json.Formatting.Indented);

                settings = null;

                System.IO.File.WriteAllText(_FILEPATH + "\\" + ApplicationConstants.APPLICATIONSETTINGSFILENAME, sSettings, Encoding.UTF8);

                return true;
            }
            catch (Exception ex)
            {
                Log.Instance.Error("Exception:" + ex.Message + Environment.NewLine + "StackTrace:" + ex.StackTrace);
                return false;
            }
        }

        public bool CreateDefaultSettings()
        {
            Log.Instance.Debug("Enter.");

            try
            {
                encDec = new AESSymmetricEncryption(ApplicationConstants.ENCRYPTION_PASSWORD, ApplicationConstants.ENCRYPTION_SALT, ApplicationConstants.ENCRYPTION_INITIALIZATION_VECTOR);

                dynamic settings = new Newtonsoft.Json.Linq.JObject();

                //Monitor and Archiving settings
                settings.file = new Newtonsoft.Json.Linq.JObject();

                settings.file.inputfiledirectory = "c:\\";
                settings.file.savetoarchivefolder = false;
                settings.file.archivedfiledirectory = "c:\\";

                //Database
                settings.database = new Newtonsoft.Json.Linq.JObject();

                settings.database.sqlserverhostip = encDec.Encrypt(string.Empty);
                settings.database.sqlinstancename = encDec.Encrypt(string.Empty);
                settings.database.sqldatabasename = encDec.Encrypt(string.Empty);
                settings.database.sqlusername = encDec.Encrypt(string.Empty);
                settings.database.sqlpassword = encDec.Encrypt(string.Empty);
                settings.database.sqlcommand = encDec.Encrypt(string.Empty);

                //Remoting
                settings.remoting = new Newtonsoft.Json.Linq.JObject();

                settings.remoting.port = ApplicationConstants.IPC_PORT;
                settings.remoting.prefix = ApplicationConstants.IPC_URI;

                //Email
                settings.email = new Newtonsoft.Json.Linq.JObject();

                settings.email.from = encDec.Encrypt(string.Empty);
                settings.email.to = encDec.Encrypt(string.Empty);
                settings.email.server = encDec.Encrypt(string.Empty);
                settings.email.port = encDec.Encrypt(string.Empty);
                settings.email.user = encDec.Encrypt(string.Empty);
                settings.email.password = encDec.Encrypt(string.Empty);
                settings.email.enablessl = false;
                settings.email.emailonsuccess = false;
                settings.email.emailonfailure = false;

                //Debug
                settings.debug = new Newtonsoft.Json.Linq.JObject();

                settings.debug.enabled = true;
                settings.debug.level = "Information";
                settings.debug.retainvalue = 100;
                settings.debug.filezise = "ThreeMegaBytes";
                settings.debug.logarchivingenabled = true;
                settings.debug.logfilesperarchive = ApplicationConstants.LOG_ARCHIVING_NUMBER_OF_LOG_FILES_PER_ARCHIVE;
                settings.debug.maxpercentageofdisk = ApplicationConstants.LOG_ARCHIVING_MAX_DISK_PERCENTAGE;
                settings.debug.maxarchiveageindaysifdiskspaceneeded = ApplicationConstants.LOG_ARCHIVING_MAXIMUM_AGE_IN_DAYS_IF_DISK_SPACE_NEEDED;

                encDec = null;

                string sSettings = Newtonsoft.Json.JsonConvert.SerializeObject(settings, Newtonsoft.Json.Formatting.Indented);

            
                return true;    settings = null;

                System.IO.File.WriteAllText(_FILEPATH + "\\" + ApplicationConstants.APPLICATIONSETTINGSFILENAME, sSettings, Encoding.UTF8);

            }
            catch (Exception ex)
            {
                Log.Instance.Error("Exception:" + ex.Message + Environment.NewLine + "StackTrace:" + ex.StackTrace);
                return false;
            }
        }

        public bool ParseArgs(string[] args)
        {
            Log.Instance.Debug("Enter.");

            if (args == null)
            {
                Log.Instance.Warn("args is null");
                return false;
            }

            try
            {
                foreach (string arg in args)
                {
                    int firstTokenIndex = arg.IndexOf("/");
                    int secondTokenIndex = arg.IndexOf(":");

                    if (firstTokenIndex >= 0 && secondTokenIndex >= 0)
                    {
                        string parameterName = arg.Substring(firstTokenIndex + 1, secondTokenIndex - firstTokenIndex - 1).ToLower();
                        string value = arg.Substring(secondTokenIndex + 1);

                        if (parameterName == "debug")
                        {
                            try
                            {
                                _DebugEnabled = Boolean.Parse(value);
                            }
                            catch
                            {
                                _DebugEnabled = true;
                            }
                        }
                        else if (parameterName == "debuglevel")
                        {
                            try
                            {
                                ApplicationTypes.LogLevel level = (ApplicationTypes.LogLevel)Enum.Parse(typeof(ApplicationTypes.LogLevel), value.ToUpper());

                                _DebugLevel = value;
                            }
                            catch
                            {
                                _DebugLevel = "Information";
                            }
                        }
                        else if (parameterName == "filesize")
                        {
                            if (string.IsNullOrEmpty(value))
                            {
                                _DebugFileSize = "ThreeMegaBytes";
                            }
                            else
                            {
                                if (value.ToUpper() != "OneMegaByte" && value.ToUpper() != "ThreeMegaBytes" && value.ToUpper() != "FiveMegaBytes")
                                {
                                    _DebugFileSize = "ThreeMegaBytes";
                                }
                                else
                                {
                                    _DebugFileSize = value;
                                }
                            }
                        }
                        else if (parameterName == "retainvalue")
                        {
                            if (string.IsNullOrEmpty(value))
                            {
                                _DebugRetainValue = 100;
                            }
                            else
                            {
                                try
                                {
                                    int iRetainValue = int.Parse(value);

                                    if (iRetainValue <= 0)
                                    {
                                        _DebugRetainValue = 100;
                                    }
                                    else
                                    {
                                        _DebugRetainValue = iRetainValue;
                                    }
                                }
                                catch
                                {
                                    _DebugRetainValue = 100;
                                }
                            }
                        }
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                Log.Instance.Error("Exception:" + ex.Message + Environment.NewLine + "StackTrace:" + ex.StackTrace);
                return false;
            }
        }
    }
}
