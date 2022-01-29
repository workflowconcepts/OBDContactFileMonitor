using System;
using System.Windows.Forms;


namespace com.workflowconcepts.applications.filemonitor
{
    public partial class ucWindowsServiceController : UserControl
    {
        public event EventHandler<WindowsServiceControllerEventArgs> StatusChanged;

        System.ServiceProcess.ServiceControllerStatus _PreviousStatus = System.ServiceProcess.ServiceControllerStatus.PausePending;

        WindowsServiceController _SvcController = null;

        string _ServiceName = string.Empty;

        bool _UseProcessInfo = false;

        public string ServiceName
        {
            get { return _ServiceName; }
            set { _ServiceName = value; }
        }

        public bool UseProcessInfo
        {
            get { return _UseProcessInfo; }
            set { _UseProcessInfo = value; }
        }

        public ucWindowsServiceController()
        {
            InitializeComponent();
        }

        private void ucWindowsServiceController_Load(object sender, EventArgs e)
        {
            btnStart.Enabled = false;
            btnStop.Enabled = false;
        }

        public bool StartMonitoring()
        {
           Log.Instance.Info("Enter.");

            if (_ServiceName == string.Empty)
            {
                Log.Instance.Warn("ServiceName has not been set.");
                return false;
            }

            if (_SvcController != null)
            {
                Log.Instance.Warn("Service is already being monitored.");
                return false;
            }

            lblServiceName.Text = _ServiceName;

            try
            {
                _SvcController = new WindowsServiceController(_ServiceName);
                tmrCheckService.Enabled = true;
                return true;
            }
            catch (Exception ex)
            {
                Log.Instance.Error("Exception:" + ex.Message + Environment.NewLine + "Stack Trace:" + ex.StackTrace);
                return false;
            }            
        }

        public bool StopMonitoring()
        {
           Log.Instance.Info("Enter.");

            if (_SvcController == null)
            {
                Log.Instance.Warn("Service is not being monitored.");
                return false;
            }

            tmrCheckService.Enabled = false;

            _SvcController.Dispose();
            _SvcController = null;

            return true;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
           Log.Instance.Info("Enter.");

            btnStart.Enabled = false;
            btnStop.Enabled = false;

            lblServiceStatus.Text = string.Empty;
            lblStatusDescription.Text = string.Empty;

            if (_SvcController != null)
            {
                try
                {
                    if (_SvcController.Start())
                    {
                       Log.Instance.Info("Service " + _ServiceName  + " started.");
                    }
                    else
                    {
                        Log.Instance.Warn("Service " + _ServiceName + " did not start.");

                        lblStatusDescription.Text = _SvcController.ErrorMessage;
                    }
                }
                catch (Exception ex)
                {
                    Log.Instance.Error("Exception:" + ex.Message + Environment.NewLine + "StackTrace:" + ex.StackTrace);
                }
            }
            else
            {
                Log.Instance.Warn("Service controller is null.");
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
           Log.Instance.Info("Enter.");

            btnStart.Enabled = false;
            btnStop.Enabled = false;

            lblServiceStatus.Text = string.Empty;
            lblStatusDescription.Text = string.Empty;

            if (_SvcController != null)
            {
                try
                {
                    if (_SvcController.Stop())
                    {
                       Log.Instance.Info("Service " + _ServiceName + " stopped.");
                    }
                    else
                    {
                        Log.Instance.Warn("Service " + _ServiceName + " did not stop.");

                        lblStatusDescription.Text = _SvcController.ErrorMessage;
                    }
                }
                catch (Exception ex)
                {
                    Log.Instance.Error("Exception:" + ex.Message + Environment.NewLine + "StackTrace:" + ex.StackTrace);
                }
            }
            else
            {
                Log.Instance.Warn("Service controller is null.");
            }
        }

        private void tmrCheckService_Tick(object sender, EventArgs e)
        {
            try
            {
                lblServiceStatus.Text = _SvcController.Status().ToString();

                if (_SvcController.Status() != _PreviousStatus)
                {
                    if (StatusChanged != null)
                    {
                        StatusChanged(this, new WindowsServiceControllerEventArgs(_SvcController.Status()));
                    }

                    _PreviousStatus = _SvcController.Status();
                }

                switch (_SvcController.Status())
                {
                    case System.ServiceProcess.ServiceControllerStatus.Running:

                        if (_UseProcessInfo)
                        {
                            if (IsProcessRunning())
                            {
                                btnStart.Enabled = false;
                                btnStop.Enabled = true;

                                lblStatusDescription.Text = "This service is currently running.";
                            }
                            else
                            {
                                lblStatusDescription.Text = "Waiting for service to start...";
                                Log.Instance.Warn("This is an unexpected state!!!");
                            }    
                        }
                        else
                        {
                            btnStart.Enabled = false;
                            btnStop.Enabled = true;

                            lblStatusDescription.Text = "This service is currently running.";
                        }                   

                        break;

                    case System.ServiceProcess.ServiceControllerStatus.Stopped:

                        if (_UseProcessInfo)
                        {
                            if (IsProcessRunning())
                            {
                                lblStatusDescription.Text = "Waiting for the process to stop...";
                            }
                            else
                            {
                                btnStart.Enabled = true;
                                btnStop.Enabled = false;

                                lblStatusDescription.Text = "This service is currently stopped.";
                            }    
                        }
                        else
                        {
                            btnStart.Enabled = true;
                            btnStop.Enabled = false;

                            lblStatusDescription.Text = "This service is currently stopped.";
                        }                   

                        break;

                    case System.ServiceProcess.ServiceControllerStatus.Paused:

                        btnStart.Enabled = false;
                        btnStop.Enabled = false;

                        lblStatusDescription.Text = "This service is currently paused. Please use the Services dialog to resume it.";

                        break;

                    default:

                        btnStart.Enabled = false;
                        btnStop.Enabled = false;

                        //lblStatusDescription.Text = "This service is currently in an intermediate state.";

                        break;
                }
            }
            catch (Exception ex)
            {
                Log.Instance.Error("Exception:" + ex.Message + Environment.NewLine + "Stack Trace:" + ex.StackTrace);
            }    
        }

        private bool IsProcessRunning()
        {
            try
            {
                System.Diagnostics.Process[] runningProcs = System.Diagnostics.Process.GetProcessesByName(_ServiceName);

                if (runningProcs != null)
                {
                    if (runningProcs.Length == 0)
                    {
                        Log.Instance.Warn("runningProcs is empty.");
                        runningProcs = null;
                        return false;
                    }
                    else
                    {
                        //Log.Instance.Debug("runningProcs is not empty.");
                        runningProcs = null;
                        return true;
                    }                    
                }
                else
                {
                    Log.Instance.Warn("runningProcs is null.");

                    return false;
                }
            }
            catch (Exception ex)
            {
                Log.Instance.Error("Exception:" + ex.Message + Environment.NewLine + "Stack Trace:" + ex.StackTrace);
                return false;
            }
        }
    }
}
