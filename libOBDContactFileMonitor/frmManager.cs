using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace com.workflowconcepts.applications.filemonitor
{
    public partial class frmManager : Form
    {
        private enum Operation { Save, Close };

        InterprocessCommunicationServer _remObj = null;

        ApplicationSettings _ApplicationSettings = null;
        
        System.ServiceProcess.ServiceControllerStatus WindowsServiceControllerStatus = System.ServiceProcess.ServiceControllerStatus.Stopped;

        bool _ChangesDetected = false;
        bool _ChangesSaved = false;

        public frmManager()
        {
            InitializeComponent();

            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this._FormClosing);
        }

        #region "Form Events"

        private void frmConnectorManager_Load(object sender, EventArgs e)
        {
            pbCompanyLogo.Image = Properties.Resources.Workflow_Concepts_logo;
            pbCompanyLogo.SizeMode = PictureBoxSizeMode.Zoom;

            //Task#1: Disable UI controls
            ucDatabaseConnectionInformation.Enabled = false;
            ucRemotingInformation.Enabled = false;
            ucEmailSettings.Enabled = false;
            ucDebugSettings.Enabled = false;
            ucFolderMonitor.Enabled = false;

            this.btnSave.Enabled = false;

            //Task#3: Add Change event handler for the different UI controls
            ucDatabaseConnectionInformation.Changed += new EventHandler(ucDatabaseConnectionInformation_Changed);
            ucRemotingInformation.Changed += new EventHandler(ucRemotingInformation_Changed);
            ucEmailSettings.Changed += new EventHandler(ucEmailSettings_Changed);
            ucDebugSettings.Changed += new EventHandler(ucDebugSettings_Changed);
            ucFolderMonitor.Changed += ucFolderMonitor_Changed;

            //Task#4: Set service controller name and statuschanged event handler
            ucWindowsServiceController.StatusChanged += new EventHandler<WindowsServiceControllerEventArgs>(ucWindowsServiceController_StatusChanged);
            ucWindowsServiceController.ServiceName = ApplicationConstants.SERVICEDISPLAYNAME;
            ucWindowsServiceController.UseProcessInfo = true;

            //Task#6: Start monitoring service
            if (ucWindowsServiceController.StartMonitoring())
            {
                 Log.Instance.Info("ucWindowsServiceController.StartMonitoring() returned true.");
            }
            else
            {
                 Log.Instance.Warn("ucWindowsServiceController.StartMonitoring() returned false.");
            }

            this.Text = System.Windows.Forms.Application.ProductName + " Manager - Version " + Application.ProductVersion;

#if (DEBUG)

            Log.Instance.Info("DEBUG is defined");

#if (DEBUG_LOCALHOST)

            Log.Instance.Info("DEBUG_LOCALHOST is defined");

            this.btnFakeServiceRunning.Visible = true;
            this.btnFakeServiceStopped.Visible = true;

#elif (DEBUG_SANDBOX)

            Log.Instance.Info("DEBUG_SANDBOX is NOT defined");

            this.btnFakeServiceRunning.Visible = true;
            this.btnFakeServiceStopped.Visible = true;
#endif

#else
            Log.Instance.Info("DEBUG is NOT defined");
#endif

#if (TRACE)
            Log.Instance.Info("TRACE is defined");
#endif

            _ChangesDetected = false;
            _ChangesSaved = false;
        }

        private void _FormClosing(object sender, FormClosingEventArgs e)
        {
             Log.Instance.Info("Enter.");

            AssertChangesDetected(e, Operation.Close, true);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
             Log.Instance.Info("Enter.");

            Cursor.Current = Cursors.WaitCursor;

            AssertChangesDetected(e, Operation.Save, false);

            Cursor.Current = Cursors.Default;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
             Log.Instance.Info("Enter.");

            AssertChangesDetected(e, Operation.Close, true);
        }

        #endregion

        #region "Private Methods"

        private void AssertChangesDetected(EventArgs e, Operation Op, bool TerminateApplication)
        {
             Log.Instance.Info("Enter.");

            if (!_ChangesDetected)
            {
                 Log.Instance.Info("No changes were detected.");

                if (Op == Operation.Close)
                {
                     Log.Instance.Info("Operation = " + Op.ToString() + "; application will terminate.");

                    //_CloseForm();
                    if (TerminateApplication)
                    {
                        this.FormClosing -= new System.Windows.Forms.FormClosingEventHandler(this._FormClosing);
                        this.Close();
                    }
                }
                else if (Op == Operation.Save)
                {
                     Log.Instance.Info("Operation = " + Op.ToString() + "; application will remain open.");
                }
                else
                {
                     Log.Instance.Warn("Operation = " + Op.ToString() + "; unknown operation.");
                }

            }
            else
            {
                 Log.Instance.Info("Changes were detected.");

                if (Op == Operation.Close)
                {
                     Log.Instance.Info("Operation = " + Op.ToString());

                    switch (MessageBox.Show("Do you want to save your changes before exiting the this form?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                    {
                        case System.Windows.Forms.DialogResult.Yes:

                            if (e is FormClosingEventArgs)
                            {
                                ((FormClosingEventArgs)e).Cancel = true;
                            }

                            switch (SaveChanges())
                            {
                                case ApplicationTypes.ApplicationSettingsReturn.SUCCESS:

                                     Log.Instance.Info("SaveChanges() returned true.");

                                    _ChangesDetected = false;
                                    _ChangesSaved = true;

                                    //_CloseForm();
                                    if (TerminateApplication)
                                    {
                                        this.FormClosing -= new System.Windows.Forms.FormClosingEventHandler(this._FormClosing);
                                        this.Close();
                                    }

                                    break;

                                case ApplicationTypes.ApplicationSettingsReturn.ERROR:

                                    MessageBox.Show("Could not save changes." + Environment.NewLine + "Please contact your system's administrator.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);

                                     Log.Instance.Warn("SaveChanges() returned false.");

                                    break;
                            }

                            break;

                        case System.Windows.Forms.DialogResult.No:

                             Log.Instance.Warn("User chose not to save changes.");

                            //_CloseForm();
                            if (TerminateApplication)
                            {
                                this.FormClosing -= new System.Windows.Forms.FormClosingEventHandler(this._FormClosing);
                                this.Close();
                            }
                            break;
                    }
                }
                else if (Op == Operation.Save)
                {
                     Log.Instance.Info("Operation = " + Op.ToString());

                    switch (SaveChanges())
                    {
                        case ApplicationTypes.ApplicationSettingsReturn.SUCCESS:

                             Log.Instance.Info("SaveChanges() returned true");

                            _ChangesSaved = true;
                            _ChangesDetected = false;

                            break;

                        case ApplicationTypes.ApplicationSettingsReturn.ERROR:

                            MessageBox.Show("Could not save changes." + Environment.NewLine + "Please contact your system's administrator.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                             Log.Instance.Warn("SaveChanges() returned false");

                            break;
                    }
                }
                else
                {
                     Log.Instance.Warn("Operation = " + Op.ToString() + "; unknown operation.");
                }

            }

             Log.Instance.Info("Exit.");
        }

        private ApplicationTypes.ApplicationSettingsReturn SaveChanges()
        {
            Log.Instance.Info("Enter.");

            //Remoting

            int iRemotingPort = 0;

            if (string.IsNullOrEmpty(ucRemotingInformation.txtPort.Text))
            {
                MessageBox.Show("Invalid value for Remoting Port. Default value of " + ApplicationConstants.IPC_PORT + " will be assumed.", System.Windows.Forms.Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tabControl.SelectedIndex = 0;
                ucRemotingInformation.txtPort.Text = ApplicationConstants.IPC_PORT.ToString();
                iRemotingPort = ApplicationConstants.IPC_PORT;
                ucRemotingInformation.txtPort.Focus();
                ucRemotingInformation.txtPort.SelectAll();
                //return ApplicationTypes.ApplicationSettingsReturn.INVALID_VALUE;
            }
            else
            {
                if (!Utilities.ValidatePortNumber(ucRemotingInformation.txtPort.Text, out iRemotingPort))
                {
                    MessageBox.Show("Value specified for the Remoting Port is invalid.", System.Windows.Forms.Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    tabControl.SelectedIndex = 0;
                    ucRemotingInformation.txtPort.Focus();
                    ucRemotingInformation.txtPort.SelectAll();
                    return ApplicationTypes.ApplicationSettingsReturn.INVALID_VALUE;
                }
            }

            if (string.IsNullOrEmpty(ucRemotingInformation.txtPrefix.Text))
            {
                MessageBox.Show("Invalid value for Remoting Prefix. Default value of " + ApplicationConstants.IPC_URI + " will be assumed.", System.Windows.Forms.Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tabControl.SelectedIndex = 0;
                ucRemotingInformation.txtPrefix.Text = ApplicationConstants.IPC_URI;
                ucRemotingInformation.txtPrefix.Focus();
                ucRemotingInformation.txtPrefix.SelectAll();
                //return ApplicationTypes.ApplicationSettingsReturn.INVALID_VALUE;
            }

            int iSMTPPort = 0;

            //Email Ssettings
            if (this.ucEmailSettings.ckbEmailFailureNotifications.Checked || this.ucEmailSettings.ckbEmailSuccessNotifications.Checked)
            {
                if (string.IsNullOrEmpty(ucEmailSettings.txtEmailFrom.Text))
                {
                    MessageBox.Show("Please specify a valid value for the Email From field.", System.Windows.Forms.Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    tabControl.SelectedIndex = 0;
                    ucEmailSettings.txtEmailFrom.Focus();
                    ucEmailSettings.txtEmailFrom.SelectAll();
                    return ApplicationTypes.ApplicationSettingsReturn.INVALID_VALUE;
                }

                if (string.IsNullOrEmpty(ucEmailSettings.txtEmailTo.Text))
                {
                    MessageBox.Show("Please specify a valid value for the Email To field.", System.Windows.Forms.Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    tabControl.SelectedIndex = 0;
                    ucEmailSettings.txtEmailTo.Focus();
                    ucEmailSettings.txtEmailTo.SelectAll();
                    return ApplicationTypes.ApplicationSettingsReturn.INVALID_VALUE;
                }

                if (string.IsNullOrEmpty(ucEmailSettings.txtSMTPServer.Text))
                {
                    MessageBox.Show("Please specify a valid value for the SMTP Server field.", System.Windows.Forms.Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    tabControl.SelectedIndex = 0;
                    ucEmailSettings.txtSMTPServer.Focus();
                    ucEmailSettings.txtSMTPServer.SelectAll();
                    return ApplicationTypes.ApplicationSettingsReturn.INVALID_VALUE;
                }

                if (string.IsNullOrEmpty(ucEmailSettings.txtSMTPPort.Text))
                {
                    MessageBox.Show("Please specify a valid value for the SMTP Port field.", System.Windows.Forms.Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    tabControl.SelectedIndex = 0;
                    ucEmailSettings.txtSMTPPort.Focus();
                    ucEmailSettings.txtSMTPPort.SelectAll();
                    return ApplicationTypes.ApplicationSettingsReturn.INVALID_VALUE;
                }
                else
                {
                    if (!Utilities.ValidatePortNumber(ucEmailSettings.txtSMTPPort.Text, out iSMTPPort))
                    {
                        MessageBox.Show("Value specified for the SMTP Port is invalid.", System.Windows.Forms.Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        tabControl.SelectedIndex = 0;
                        ucEmailSettings.txtSMTPPort.Focus();
                        ucEmailSettings.txtSMTPPort.SelectAll();
                        return ApplicationTypes.ApplicationSettingsReturn.INVALID_VALUE;
                    }
                }
            }

            //Folder Monitoring

            if (string.IsNullOrEmpty(ucFolderMonitor.txtFolderPath.Text))
            {
                MessageBox.Show("Invalid value for Folder Path. Application won't function properly", System.Windows.Forms.Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tabControl.SelectedIndex = 0;
                ucFolderMonitor.txtFolderPath.Focus();
                //return ApplicationTypes.ApplicationSettingsReturn.INVALID_VALUE;
            }
            else
            {
                if(!System.IO.Directory.Exists(ucFolderMonitor.txtFolderPath.Text))
                {
                    MessageBox.Show("Value provided for Folder Path does not exist. Application won't function properly", System.Windows.Forms.Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    tabControl.SelectedIndex = 0;
                    ucFolderMonitor.txtFolderPath.Focus();
                    ucFolderMonitor.txtFolderPath.SelectAll();
                }
            }

            if(ucFolderMonitor.ckbArchiveProcessedFiles.Checked)
            {
                if (string.IsNullOrEmpty(ucFolderMonitor.txtArchivePath.Text))
                {
                    MessageBox.Show("Invalid value for Archive Path. Application won't function properly", System.Windows.Forms.Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    tabControl.SelectedIndex = 0;
                    ucFolderMonitor.txtArchivePath.Focus();
                    //return ApplicationTypes.ApplicationSettingsReturn.INVALID_VALUE;
                }
                else
                {
                    if (!System.IO.Directory.Exists(ucFolderMonitor.txtArchivePath.Text))
                    {
                        MessageBox.Show("Value provided for Archive Path does not exist. Application won't function properly", System.Windows.Forms.Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        tabControl.SelectedIndex = 0;
                        ucFolderMonitor.txtArchivePath.Focus();
                        ucFolderMonitor.txtArchivePath.SelectAll();
                    }
                }
            }

            try
            {
                _ApplicationSettings.SqlServerHostIp = ucDatabaseConnectionInformation.txtServer.Text;
                _ApplicationSettings.SqlDatabaseName = ucDatabaseConnectionInformation.txtDatabase.Text;
                _ApplicationSettings.SqlUserName = ucDatabaseConnectionInformation.txtUsername.Text;
                _ApplicationSettings.SqlPassword = ucDatabaseConnectionInformation.txtPassword.Text;

                _ApplicationSettings.InputDirectory = ucFolderMonitor.txtFolderPath.Text;
                _ApplicationSettings.ArchiveFiles = ucFolderMonitor.ckbArchiveProcessedFiles.Checked;
                _ApplicationSettings.ArchiveDirectory = ucFolderMonitor.txtArchivePath.Text;

                _ApplicationSettings.Remoting_Port = iRemotingPort;
                _ApplicationSettings.Remoting_Prefix = ucRemotingInformation.txtPrefix.Text;

                _ApplicationSettings.EmailOnFailure = ucEmailSettings.ckbEmailFailureNotifications.Checked;
                _ApplicationSettings.EmailOnSuccess = ucEmailSettings.ckbEmailSuccessNotifications.Checked;
                _ApplicationSettings.EmailFrom = ucEmailSettings.txtEmailFrom.Text;
                _ApplicationSettings.EmailTo = ucEmailSettings.txtEmailTo.Text;
                _ApplicationSettings.SMTPServer = ucEmailSettings.txtSMTPServer.Text;
                _ApplicationSettings.SMTPPort = iSMTPPort;
                _ApplicationSettings.SMTPUserName = ucEmailSettings.txtSMTPUser.Text;
                _ApplicationSettings.SMTPPassword = ucEmailSettings.txtSMTPPassword.Text;
                _ApplicationSettings.EmailEnableSSL = ucEmailSettings.ckbEnableSSL.Checked;

                _ApplicationSettings.Debug = ucDebugSettings.ckbDebugEnabled.Checked;
                _ApplicationSettings.DebugLevel = ucDebugSettings.cbDebugLevel.Text;

                int iValue = 0;

                if (int.TryParse(ucDebugSettings.txtRetainValue.Text, out iValue))
                {
                    _ApplicationSettings.DebugRetainValue = iValue;
                }
                else
                {
                    _ApplicationSettings.DebugRetainValue = 100;
                }

                _ApplicationSettings.DebugFileSize = ucDebugSettings.cbFileSize.Text;

                _ApplicationSettings.ArchivingEnabled = ucDebugSettings.ckbEnableLogArchiving.Checked;

                if (int.TryParse(ucDebugSettings.txtLogFilesPerArchive.Text, out iValue))
                {
                    _ApplicationSettings.LogFilesPerArchive = iValue;
                }
                else
                {
                    _ApplicationSettings.LogFilesPerArchive = ApplicationConstants.LOG_ARCHIVING_NUMBER_OF_LOG_FILES_PER_ARCHIVE;
                }

                if (int.TryParse(ucDebugSettings.txtMaxPercentDiskForLogArchiving.Text, out iValue))
                {
                    _ApplicationSettings.MaxDiskPercentageForLogArchiving = iValue;
                }
                else
                {
                    _ApplicationSettings.MaxDiskPercentageForLogArchiving = ApplicationConstants.LOG_ARCHIVING_MAX_DISK_PERCENTAGE;
                }

                if (int.TryParse(ucDebugSettings.txtMaxArchiveAgeIfDiskSpaceNeeded.Text, out iValue))
                {
                    _ApplicationSettings.MaximumArchiveAgeInDaysIfDiskSpaceNeeded = iValue;
                }
                else
                {
                    _ApplicationSettings.MaximumArchiveAgeInDaysIfDiskSpaceNeeded = ApplicationConstants.LOG_ARCHIVING_MAXIMUM_AGE_IN_DAYS_IF_DISK_SPACE_NEEDED;
                }

                if (_remObj.SaveApplicationSettings(_ApplicationSettings))
                {
                    Log.Instance.Info("_remObj_HTTPHandler.SaveApplicationSettings() returned true.");

                    _remObj = (InterprocessCommunicationServer)Activator.GetObject(typeof(InterprocessCommunicationServer), "tcp://127.0.0.1:" + _ApplicationSettings.Remoting_Port + "/" + _ApplicationSettings.Remoting_Prefix);

                }
                else
                {
                    Log.Instance.Warn("_remObj.SaveApplicationSettings() returned false.");
                }

                return ApplicationTypes.ApplicationSettingsReturn.SUCCESS;
            }
            catch (Exception ex)
            {
                Log.Instance.Error("Exception:" + ex.Message + Environment.NewLine + "StackTrace:" + ex.StackTrace);
                return ApplicationTypes.ApplicationSettingsReturn.ERROR;
            }
        }

        #endregion

        #region "Menu Events Handlers"

        private void mnuMainFileExit_Click(object sender, EventArgs e)
        {
             Log.Instance.Info("Enter.");

            AssertChangesDetected(e, Operation.Close, true);
        }

        private void mnuMainAboutCompany_Click(object sender, EventArgs e)
        {
             Log.Instance.Info("Enter.");

            System.Diagnostics.Process.Start(ApplicationConstants.COMPANY_WEB_SITE);
        }

        private void mnuMainCallbackAdministration_Click(object sender, EventArgs e)
        {
             Log.Instance.Info("Enter.");
        }

        private void mnuMainCallbackDiagnostics_Click(object sender, EventArgs e)
        {
             Log.Instance.Info("Enter.");
        }

        #endregion

        #region "Changed Events"

        void ucDatabaseConnectionInformation_Changed(object sender, EventArgs e)
        {
             Log.Instance.Info("Enter.");
            _ChangesDetected = true;
        }

        void ucRemotingInformation_Changed(object sender, EventArgs e)
        {
             Log.Instance.Info("Enter.");
            _ChangesDetected = true;
        }

        void ucEmailSettings_Changed(object sender, EventArgs e)
        {
             Log.Instance.Info("Enter.");
            _ChangesDetected = true;
        }

        void ucDebugSettings_Changed(object sender, EventArgs e)
        {
             Log.Instance.Info("Enter.");
            _ChangesDetected = true;
        }

        private void ucFolderMonitor_Changed(object sender, EventArgs e)
        {
            Log.Instance.Info("Enter.");
            _ChangesDetected = true;
        }

        void ucWindowsServiceController_StatusChanged(object sender, WindowsServiceControllerEventArgs e)
        {
             Log.Instance.Info("Service Status: " + e.CurrentStatus.ToString());

            switch (e.CurrentStatus)
            {
                case System.ServiceProcess.ServiceControllerStatus.Running:

                    WindowsServiceControllerStatus = System.ServiceProcess.ServiceControllerStatus.Running;

                    try
                    {
                        if (_remObj != null)
                        {
                            _remObj = null;
                        }

                        _ApplicationSettings = new ApplicationSettings(ApplicationConstants.ApplicationSettingsFilePath);

                        if (!System.IO.File.Exists(ApplicationConstants.ApplicationSettingsFilePath + "\\" + ApplicationConstants.APPLICATIONSETTINGSFILENAME))
                        {
                             Log.Instance.Warn("The application settings file should exist under this condition.");

                            if (_ApplicationSettings.CreateDefaultSettings())
                            {
                                 Log.Instance.Info("Default settings file was created.");
                            }
                            else
                            {
                                 Log.Instance.Warn("Default settings file was not created.");
                            }
                        }

                        if(!_ApplicationSettings.Load())
                        {
                             Log.Instance.Warn("Loading application settings failed, remoting will likely fail.");
                        }

                         Log.Instance.Info("tcp://127.0.0.1:" + _ApplicationSettings.Remoting_Port + "/" + _ApplicationSettings.Remoting_Prefix);

                        _remObj = (InterprocessCommunicationServer)Activator.GetObject(typeof(InterprocessCommunicationServer), "tcp://127.0.0.1:" + _ApplicationSettings.Remoting_Port + "/" + _ApplicationSettings.Remoting_Prefix);

                        if (_ApplicationSettings != null)
                        {
                            _ApplicationSettings = null;
                        }

                        _ApplicationSettings = (ApplicationSettings)_remObj.GetApplicationSettings();

                        ucDatabaseConnectionInformation.txtServer.Text = _ApplicationSettings.SqlServerHostIp;
                        ucDatabaseConnectionInformation.txtDatabase.Text = _ApplicationSettings.SqlDatabaseName;
                        ucDatabaseConnectionInformation.txtUsername.Text = _ApplicationSettings.SqlUserName;
                        ucDatabaseConnectionInformation.txtPassword.Text = _ApplicationSettings.SqlPassword;

                        ucRemotingInformation.txtPort.Text = _ApplicationSettings.Remoting_Port.ToString();
                        ucRemotingInformation.txtPrefix.Text = _ApplicationSettings.Remoting_Prefix;

                        ucEmailSettings.txtEmailFrom.Text = _ApplicationSettings.EmailFrom;
                        ucEmailSettings.txtEmailTo.Text = _ApplicationSettings.EmailTo;
                        ucEmailSettings.txtSMTPServer.Text = _ApplicationSettings.SMTPServer;
                        ucEmailSettings.txtSMTPPort.Text = _ApplicationSettings.SMTPPort.ToString();
                        ucEmailSettings.txtSMTPUser.Text = _ApplicationSettings.SMTPUserName;
                        ucEmailSettings.txtSMTPPassword.Text = _ApplicationSettings.SMTPPassword;
                        ucEmailSettings.ckbEnableSSL.Checked = !_ApplicationSettings.EmailEnableSSL;
                        ucEmailSettings.ckbEnableSSL.Checked = _ApplicationSettings.EmailEnableSSL;

                        ucEmailSettings.ckbEmailFailureNotifications.Checked = !_ApplicationSettings.EmailOnFailure;
                        ucEmailSettings.ckbEmailFailureNotifications.Checked = _ApplicationSettings.EmailOnFailure;
                        ucEmailSettings.ckbEmailSuccessNotifications.Checked = !_ApplicationSettings.EmailOnSuccess;
                        ucEmailSettings.ckbEmailSuccessNotifications.Checked = _ApplicationSettings.EmailOnSuccess;

                        ucDebugSettings.ckbDebugEnabled.Checked = !_ApplicationSettings.Debug;
                        ucDebugSettings.ckbDebugEnabled.Checked = _ApplicationSettings.Debug;

                        ucDebugSettings.cbDebugLevel.Text = _ApplicationSettings.DebugLevel;

                        if (string.IsNullOrEmpty(ucDebugSettings.cbDebugLevel.Text))
                        {
                            ucDebugSettings.cbDebugLevel.Text = "Information";
                        }

                        ucDebugSettings.cbRetainUnit.Text = "Files";

                        ucDebugSettings.cbFileSize.Text = _ApplicationSettings.DebugFileSize;

                        if (string.IsNullOrEmpty(ucDebugSettings.cbFileSize.Text))
                        {
                            ucDebugSettings.cbFileSize.Text = "ThreeMegaBytes";
                        }

                        ucDebugSettings.txtRetainValue.Text = _ApplicationSettings.DebugRetainValue.ToString();

                        if (string.IsNullOrEmpty(ucDebugSettings.txtRetainValue.Text))
                        {
                            ucDebugSettings.txtRetainValue.Text = "100";
                        }

                        ucDebugSettings.ckbEnableLogArchiving.Checked = !_ApplicationSettings.ArchivingEnabled;
                        ucDebugSettings.ckbEnableLogArchiving.Checked = _ApplicationSettings.ArchivingEnabled;

                        ucDebugSettings.txtLogFilesPerArchive.Text = _ApplicationSettings.LogFilesPerArchive.ToString();
                        ucDebugSettings.txtMaxPercentDiskForLogArchiving.Text = _ApplicationSettings.MaxDiskPercentageForLogArchiving.ToString();
                        ucDebugSettings.txtMaxArchiveAgeIfDiskSpaceNeeded.Text = _ApplicationSettings.MaximumArchiveAgeInDaysIfDiskSpaceNeeded.ToString();

                        ucFolderMonitor.txtFolderPath.Text = _ApplicationSettings.InputDirectory;

                        ucFolderMonitor.ckbArchiveProcessedFiles.Checked = !_ApplicationSettings.ArchiveFiles;
                        ucFolderMonitor.ckbArchiveProcessedFiles.Checked = _ApplicationSettings.ArchiveFiles;

                        ucFolderMonitor.txtArchivePath.Text = _ApplicationSettings.ArchiveDirectory;
                    }
                    catch (Exception ex)
                    {
                         Log.Instance.Warn(ex.Message + Environment.NewLine + ex.StackTrace);

                        MessageBox.Show(ex.Message + Environment.NewLine + ex.StackTrace);
                    }

                    ucDatabaseConnectionInformation.Enabled = true;
                    ucRemotingInformation.Enabled = true;
                    ucEmailSettings.Enabled = true;
                    ucDebugSettings.Enabled = true;
                    ucFolderMonitor.Enabled = true;

                    btnSave.Enabled = true;

                    _ChangesDetected = false;
                    _ChangesSaved = false;

                    break;

                case System.ServiceProcess.ServiceControllerStatus.Stopped:

                    WindowsServiceControllerStatus = System.ServiceProcess.ServiceControllerStatus.Stopped;

                    ucDatabaseConnectionInformation.Enabled = false;
                    ucRemotingInformation.Enabled = false;
                    ucEmailSettings.Enabled = false;
                    ucDebugSettings.Enabled = false;

                    btnSave.Enabled = false;

                    _ChangesDetected = false;
                    _ChangesSaved = false;

                    break;

                default:

                    WindowsServiceControllerStatus = System.ServiceProcess.ServiceControllerStatus.Stopped;

                    break;
            }
        }

        #endregion

        private void btnFakeServiceRunning_Click(object sender, EventArgs e)
        {
            //lock (objLock)
            //{
                try
                {
                    ucWindowsServiceController_StatusChanged(this, new WindowsServiceControllerEventArgs(System.ServiceProcess.ServiceControllerStatus.Running));
                }
                catch (Exception ex)
                {

                }

            //}//lock(objLock)
        }

        private void btnFakeServiceStopped_Click(object sender, EventArgs e)
        {
            //lock (objLock)
            //{
                try
                {
                    ucWindowsServiceController_StatusChanged(this, new WindowsServiceControllerEventArgs(System.ServiceProcess.ServiceControllerStatus.Stopped));
                }
                catch (Exception ex)
                {

                }

            //}//lock(objLock)
        }
    }
}
