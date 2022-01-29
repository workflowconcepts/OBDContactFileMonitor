using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace com.workflowconcepts.applications.filemonitor
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            System.Threading.Thread.CurrentThread.Name = "main";

            Log.ConfigureFileTarget(Environment.ExpandEnvironmentVariables("%SystemDrive%"), System.Windows.Forms.Application.CompanyName, System.Windows.Forms.Application.ProductName, NLog.LogLevel.Trace, 5, 3145728);

            Log.Instance.Info("Enter.");

            Log.Instance.Info("OS Version: " + Environment.OSVersion);
            Log.Instance.Info("# of CPUs: " + Environment.ProcessorCount);
            Log.Instance.Info("Environment Version: " + Environment.Version);

            if (Environment.Is64BitOperatingSystem)
            {
                Log.Instance.Info("Architecture: 64bit");
            }
            else
            {
                Log.Instance.Info("Architecture: 32bit");
            }

            if (Environment.Is64BitProcess)
            {
                Log.Instance.Info("Process: 64bit");
            }
            else
            {
                Log.Instance.Info("Process: 32bit");
            }

            Log.Instance.Info("Product Version: " + System.Windows.Forms.Application.ProductVersion);

            Log.Instance.Info("Main thread ID: " + System.Threading.Thread.CurrentThread.ManagedThreadId.ToString());

#if (TRACE)
            Log.Instance.Info("TRACE is defined");
#endif

#if (DEBUG)

            Log.Instance.Info("DEBUG is defined");

#if (DEBUG_LOCALHOST)

            Log.Instance.Info("DEBUG_LOCALHOST is defined");

#endif

#if (DEBUG_SANDBOX)
            
            Log.Instance.Info("DEBUG_SANDBOX is defined");

#endif

#else
            Log.Instance.Info("DEBUG is not defined -> release version");
#endif

            bool SingleInstance = false;

            System.Threading.Mutex mutex = new System.Threading.Mutex(true, Application.ProductName, out SingleInstance);

            if (!SingleInstance)
            {
                MessageBox.Show("Application is already running." + Environment.NewLine + "Please close other instances.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);

                Log.Instance.Error("Another instance of this application is already running." + Environment.NewLine + "This instance will terminate.");

                Application.Exit();
            }
            else
            {
                Log.Instance.Warn("First instance of this application.");
            }

            Log.Instance.Info("Runtime: " + Environment.Version.ToString());
            Log.Instance.Info("Product Version: " + Application.ProductVersion);

            try
            {
                System.Security.Principal.WindowsPrincipal wp = new System.Security.Principal.WindowsPrincipal(System.Security.Principal.WindowsIdentity.GetCurrent());

                if (!wp.IsInRole(System.Security.Principal.WindowsBuiltInRole.Administrator))
                {
                    MessageBox.Show("Current user is not part of BuiltIn\\Administrators." + Environment.NewLine + "Application will not work properly." + Environment.NewLine + "Please contact your System's Administrator.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    Log.Instance.Warn("User is not part of BuiltIn\\Administrators");
                }
                else
                {
                    Log.Instance.Info("User is part of BuiltIn\\Administrators");
                }
            }
            catch (Exception ex)
            {
                Log.Instance.Error("Exception determining user group:" + ex.Message + Environment.NewLine + "Stack Trace:" + ex.StackTrace);
                MessageBox.Show("Exception determining user group:" + Environment.NewLine + ex.Message + Environment.NewLine + "Application will not work properly." + Environment.NewLine + "Please contact your System's Administrator.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmManager());

            GC.KeepAlive(mutex);
        }
    }
}
