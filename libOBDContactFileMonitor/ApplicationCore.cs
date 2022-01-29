using System;
using System.Collections.Generic;
using System.Linq;

namespace com.workflowconcepts.applications.filemonitor
{
    public class ApplicationCore
    {
        com.workflowconcepts.utilities.LogFilesArchiver.LogArchiver _LogArchiver = null;
        
        ApplicationSettings _Settings = null;
        
        EmailNotifier _EmailNotifier = null;
        ApplicationTypes.EmailNotifierDelegate _EmailNotifierDelegate = null;
        DateTime dNetworkUnavailable = DateTime.MinValue;
        FileMonitor _fileMonitor = null;

        public void OnStart(string[] args)
        {
            Log.Instance.Debug($"Enter");

            _LogArchiver = new utilities.LogFilesArchiver.LogArchiver();

            _LogArchiver.ApplicationName = System.Windows.Forms.Application.ProductName;
            _LogArchiver.LogDirectory = "c:\\" + System.Windows.Forms.Application.CompanyName + "\\" + System.Windows.Forms.Application.ProductName;
            _LogArchiver.ArchiveDirectory = "c:\\" + System.Windows.Forms.Application.CompanyName + "\\" + System.Windows.Forms.Application.ProductName + " Log Archive";

            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

            System.Net.NetworkInformation.NetworkChange.NetworkAvailabilityChanged += new System.Net.NetworkInformation.NetworkAvailabilityChangedEventHandler(NetworkChange_NetworkAvailabilityChanged);
            System.Net.NetworkInformation.NetworkChange.NetworkAddressChanged += new System.Net.NetworkInformation.NetworkAddressChangedEventHandler(NetworkChange_NetworkAddressChanged);

            _EmailNotifierDelegate = new ApplicationTypes.EmailNotifierDelegate(__EmailNotifierDelegate);

            Log.Instance.Debug("Application settings location: " + ApplicationConstants.ApplicationSettingsFilePath + "\\" + ApplicationConstants.APPLICATIONSETTINGSFILENAME);

            _Settings = new ApplicationSettings(ApplicationConstants.ApplicationSettingsFilePath);

            if (!System.IO.File.Exists(ApplicationConstants.ApplicationSettingsFilePath + "\\" + ApplicationConstants.APPLICATIONSETTINGSFILENAME))
            {
                Log.Instance.Warn(ApplicationConstants.ApplicationSettingsFilePath + "\\" + ApplicationConstants.APPLICATIONSETTINGSFILENAME + " was created with default settings.");

                if (_Settings.CreateDefaultSettings())
                {
                    Log.Instance.Debug("Default settings file was created.");
                }
                else
                {
                    Log.Instance.Warn("Default settings file was not created.");
                }
            }

            if (_Settings.Load())
            {
                Log.Instance.Debug("_Settings.Load() returned true");

                _Settings.ParseArgs(args);

                _EmailNotifier = new EmailNotifier(_Settings.SMTPServer, _Settings.SMTPPort, _Settings.EmailEnableSSL, _Settings.SMTPUserName, _Settings.SMTPPassword, _Settings.EmailOnSuccess, _Settings.EmailOnFailure, string.Empty);

                ResetLogging();

                ResetManagementRemotingChannel();

                if (_EmailNotifierDelegate != null)
                {
                    _EmailNotifierDelegate.BeginInvoke(Environment.MachineName + " - " + ApplicationConstants.SERVICEDISPLAYNAME + " - Service started", string.Empty, ApplicationTypes.NotificationType.SUCCESS, null, null);
                }

                Log.Instance.Debug("Service started");
            }
            else
            {
                Log.Instance.Error("_Settings.Load() returned false");

                if (_EmailNotifierDelegate != null)
                {
                    _EmailNotifierDelegate.BeginInvoke(Environment.MachineName + " - " + ApplicationConstants.SERVICEDISPLAYNAME + " - Service failed to start", string.Empty, ApplicationTypes.NotificationType.FAILURE, null, null);
                }

                Log.Instance.Fatal("Service did not start");

                TerminateService(true, -1);
            }
            // rjm 1/25/2022
            Log.Instance.Debug("Launch FileMonitor");
            _fileMonitor = new FileMonitor(_Settings);
        }

        public void OnStop()
        {
            Log.Instance.Debug($"Enter");

            TerminateService(true, 0);
        }

        #region "Network Events"

        void NetworkChange_NetworkAddressChanged(object sender, EventArgs e)
        {
            Log.Instance.Debug("Enter.");
        }

        void NetworkChange_NetworkAvailabilityChanged(object sender, System.Net.NetworkInformation.NetworkAvailabilityEventArgs e)
        {
            Log.Instance.Debug("Enter.");

            if (e.IsAvailable)
            {
                Log.Instance.Debug("Network became available.");

                System.Threading.Thread.Sleep(5000);

                try
                {
                    if (_EmailNotifierDelegate != null)
                    {
                        _EmailNotifierDelegate.BeginInvoke(Environment.MachineName + " - " + ApplicationConstants.SERVICEDISPLAYNAME + " - Network availability changed", "Network became unavailable at " + dNetworkUnavailable.ToString() + Environment.NewLine + "It is now available, but we recommend you contact your System Administrator.", ApplicationTypes.NotificationType.FAILURE, null, null);
                    }
                }
                catch
                {
                }

                dNetworkUnavailable = DateTime.MinValue;
            }
            else
            {
                Log.Instance.Warn("Network became unavailable.");

                dNetworkUnavailable = DateTime.Now;
            }
        }

        #endregion

        void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Log.Instance.Debug("Enter.");

            try
            {
                Exception ex = (Exception)e.ExceptionObject;

                Log.Instance.Error("UnhandledException:" + ex.Message + Environment.NewLine + "StackTrace:" + ex.StackTrace);

                string sSubject = Environment.MachineName + " - " + ApplicationConstants.SERVICEDISPLAYNAME + " - Unhandled Exception";
                string sBody = " Unhandled Exception detected in this service. Appliaction will terminate." + Environment.NewLine + Environment.NewLine + "UnhandledException:" + ex.Message + Environment.NewLine + "StackTrace:" + ex.StackTrace;

                if (_EmailNotifierDelegate != null)
                {
                    _EmailNotifierDelegate.BeginInvoke(sSubject, sBody, ApplicationTypes.NotificationType.FAILURE, null, null);
                }
            }
            catch
            {

            }

            TerminateService(true, -1);
        }

        void ResetLogging()
        {
            Log.Instance.Debug("Enter");

            if (_Settings != null)
            {
                if (_Settings.Debug)
                {
                    Log.ConfigureFileTarget(Environment.ExpandEnvironmentVariables("%SystemDrive%"), System.Windows.Forms.Application.CompanyName, System.Windows.Forms.Application.ProductName, _Settings.DebugLevel, _Settings.DebugRetainValue.ToString(), _Settings.DebugFileSize);

                    _LogArchiver.Stop();

                    if (_Settings.ArchivingEnabled)
                    {
                        Log.Instance.Debug("Log archiving is enabled");

                        _LogArchiver.NumberOfFilesToArchive = _Settings.LogFilesPerArchive;
                        _LogArchiver.MaximumArchiveDirectorySizeAsPercentageOfTotalDisk = _Settings.MaxDiskPercentageForLogArchiving;
                        _LogArchiver.MaximumArchiveAgeInDaysIfDiskSpaceNeeded = _Settings.MaximumArchiveAgeInDaysIfDiskSpaceNeeded;

                        _LogArchiver.Start();
                    }
                    else
                    {
                        Log.Instance.Debug("Log archiving is disabled");
                    }
                }
                else //if(_Settings.Debug)
                {
                    Log.Instance.Debug("Logging has been disabled");

                    _LogArchiver.Stop();

                    Log.RemoveAllLogging();

                }//if(_Settings.Debug)
            }
            else //if(_Settings != null)
            {
                Log.Instance.Warn("_Settings is null");

            }//if(_Settings != null)
        }

        void ResetManagementRemotingChannel()
        {
            Log.Instance.Info("Enter.");

            try
            {


                Log.Instance.Info("Registering IPC channel " + "tcp://127.0.0.1:" + _Settings.Remoting_Port + "/" + _Settings.Remoting_Prefix);

                ApplicationTypes.SettingsCallback _SettingsChangedCallback = new ApplicationTypes.SettingsCallback(__SettingsChangedCallback);

                Log.Instance.Trace($"Number of registered channels:{System.Runtime.Remoting.Channels.ChannelServices.RegisteredChannels.Count()}");

                foreach (System.Runtime.Remoting.Channels.IChannel c in System.Runtime.Remoting.Channels.ChannelServices.RegisteredChannels)
                {                    
                    System.Runtime.Remoting.Channels.ChannelServices.UnregisterChannel(c);

                    Log.Instance.Trace($"Unregistered channel name:{c.ChannelName}");
                }

                System.Runtime.Remoting.Channels.Tcp.TcpChannel _IPC = new System.Runtime.Remoting.Channels.Tcp.TcpChannel(_Settings.Remoting_Port);

                System.Runtime.Remoting.Channels.ChannelServices.RegisterChannel(_IPC, false);

                System.Runtime.Remoting.RemotingConfiguration.RegisterWellKnownServiceType(typeof(InterprocessCommunicationServer), _Settings.Remoting_Prefix, System.Runtime.Remoting.WellKnownObjectMode.Singleton);

                InterprocessCommunicationServer.SetApplicationSettingsReference(_Settings);
                //InterprocessCommunicationServer.SetPerformanceCountersReference(_PerformanceCounters);
                InterprocessCommunicationServer.SetSettingsChangedCallBack(_SettingsChangedCallback);

                Log.Instance.Info("IPC channel has been successfully registered");
            }
            catch (Exception ex)
            {
                Log.Instance.Error($"Exception:{ex.Message} {Environment.NewLine} StackTrace:{ex.StackTrace}");
            }
        }

        void __SettingsChangedCallback(ApplicationTypes.iApplicationSettings Settings)
        {
            Log.Instance.Info("Enter.");

            _Settings = (ApplicationSettings)Settings;

            if (_Settings.Save())
            {
                InterprocessCommunicationServer.SetApplicationSettingsReference(_Settings);

                //_Notifier = null;
                //_Notifier = new Notifier(_Settings.EmailFrom, _Settings.EmailTo, _Settings.SMTPServer, _Settings.SMTPPort, _Settings.EmailEnableSSL, _Settings.SMTPUserName, _Settings.SMTPPassword, _Settings.EmailOnSuccess, _Settings.EmailOnFailure);

                ResetLogging();

                ResetManagementRemotingChannel();

                //if (_Notifier.Email(Environment.MachineName + " - " + ApplicationConstants.SERVICEDISPLAYNAME + " - Settings Updates", String.Empty, String.Empty, ApplicationTypes.NotificationType.SUCCESS))
                //{
                //    Log.Instance.Info("notifier.Email() returned true.");
                //}
                //else
                //{
                //    Log.Instance.Warn("notifier.Email() returned true.");
                //}
            }
            else
            {
                Log.Instance.Warn("Settings file was not updated.");
            }
        }

        private void __EmailNotifierDelegate(string Subject, string Body, ApplicationTypes.NotificationType NotificationType)
        {
            try
            {
                if (_EmailNotifier != null)
                {
                    if (_EmailNotifier.Send(_Settings.EmailFrom, _Settings.EmailTo, Environment.MachineName + " - " + ApplicationConstants.SERVICEDISPLAYNAME + " - " + Subject, Body, null, NotificationType))
                    {
                        Log.Instance.Debug("notifier.Email() returned true.");
                    }
                    else
                    {
                        Log.Instance.Warn("notifier.Email() returned false.");
                    }
                }
            }
            catch(Exception ex)
            {
                Log.Instance.Error($"Exception:{ex.Message} {Environment.NewLine} StackTrace:{ex.StackTrace}");
            }
        }

        void TerminateService(bool ExecuteExit, int ExitCode)
        {
            Log.Instance.Debug("Enter.");

            string sSubject = Environment.MachineName + " - " + ApplicationConstants.SERVICEDISPLAYNAME + " - Service stopped";
            string sBody = string.Empty;

            if (_EmailNotifierDelegate != null)
            {
                _EmailNotifierDelegate.BeginInvoke(sSubject, sBody, ApplicationTypes.NotificationType.FAILURE, null, null);
            }

            if (_EmailNotifier != null)
            {
                _EmailNotifier = null;
            }

            if (_fileMonitor != null)
            {
                _fileMonitor.Dispose();
                _fileMonitor = null;
            }

            if (ExecuteExit)
            {
                Environment.Exit(ExitCode);
            }
        }
    }
}
