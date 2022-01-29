using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ServiceProcess;

namespace com.workflowconcepts.applications.filemonitor
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Threading.Thread.CurrentThread.Name = "main";

            Log.ConfigureFileTarget(Environment.ExpandEnvironmentVariables("%SystemDrive%"), System.Windows.Forms.Application.CompanyName, System.Windows.Forms.Application.ProductName, NLog.LogLevel.Trace, 5, 204800);

            Log.Instance.Debug("Enter.");

            Log.Instance.Debug("OS Version: " + Environment.OSVersion);
            Log.Instance.Debug("# of CPUs: " + Environment.ProcessorCount);
            Log.Instance.Debug("Environment Version: " + Environment.Version);

            if (Environment.Is64BitOperatingSystem)
            {
                Log.Instance.Debug("Architecture: 64bit");
            }
            else
            {
                Log.Instance.Debug("Architecture: 32bit");
            }

            if (Environment.Is64BitProcess)
            {
                Log.Instance.Debug("Process: 64bit");
            }
            else
            {
                Log.Instance.Debug("Process: 32bit");
            }

            Log.Instance.Debug("Product Version: " + System.Windows.Forms.Application.ProductVersion);

            Log.Instance.Debug("Main thread ID: " + System.Threading.Thread.CurrentThread.ManagedThreadId.ToString());

#if (TRACE)
            Log.Instance.Debug("TRACE is defined");
#endif

#if (DEBUG)

            Log.Instance.Debug("DEBUG is defined");

#if (DEBUG_LOCALHOST)

            Log.ConfigureConsoleTarget(NLog.LogLevel.Trace);
            Log.Instance.Debug("DEBUG_LOCALHOST is defined -> run application as a Console app");
            RunAsConsoleApp(args);

#elif (DEBUG_SANDBOX)
            
            Log.ConfigureConsoleTarget(NLog.LogLevel.Trace);
            Log.Instance.Debug("DEBUG_SANDBOX is defined -> run application as a Console app");
            RunAsConsoleApp(args);
#else
            Log.Instance.Debug("DEBUG is not defined -> release version  -> run application as a Windows Service app");

            RunAsWindowsService(args);
#endif

#endif
        }

        static void RunAsConsoleApp(string[] args)
        {
            ApplicationCore a = new ApplicationCore();
            
            a.OnStart(args);

            Console.WindowHeight = Console.LargestWindowHeight - 30;
            Console.WindowWidth = Console.LargestWindowWidth - 10;
            
            Console.ReadKey();
        }

        static void RunAsWindowsService(string[] args)
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[] { new com.workflowconcepts.applications.filemonitor.Service() };
            ServiceBase.Run(ServicesToRun);
        }
    }
}
