using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.workflowconcepts.applications.filemonitor
{
    public class ServiceStatus
    {
        ApplicationTypes.iCounter _SessionsCounter = null;

        public string version
        {
            get
            {
                return System.Windows.Forms.Application.ProductVersion;
            }
        }

        public string startedon
        {
            get
            {
                return System.Diagnostics.Process.GetCurrentProcess().StartTime.ToUniversalTime().ToString();
            }
        }

        public string processortime
        {
            get
            {
                return string.Format("{0:0.00}", System.Diagnostics.Process.GetCurrentProcess().TotalProcessorTime.TotalSeconds) + " secs";
            }
        }

        public string threads
        {
            get
            {
                return System.Diagnostics.Process.GetCurrentProcess().Threads.Count.ToString();
            }
        }

        public string id
        {
            get
            {
                return System.Diagnostics.Process.GetCurrentProcess().Id.ToString();
            }
        }

        public string machinename
        {
            get
            {
                return System.Diagnostics.Process.GetCurrentProcess().MachineName;
            }
        }

        public string pagedmemorysize64
        {
            get
            {
                return (System.Diagnostics.Process.GetCurrentProcess().PagedMemorySize64 / (1024 * 1024)) + " MB";
            }
        }

        public string peakpagedmemorysize64
        {
            get
            {
                return (System.Diagnostics.Process.GetCurrentProcess().PeakPagedMemorySize64 / (1024 * 1024)) + " MB";
            }
        }

        public string virtualmemorysize64
        {
            get
            {
                return (System.Diagnostics.Process.GetCurrentProcess().VirtualMemorySize64 / (1024 * 1024)) + " MB";
            }
        }

        public string peakvirtualmemorysize64
        {
            get
            {
                return (System.Diagnostics.Process.GetCurrentProcess().PeakVirtualMemorySize64 / (1024 * 1024)) + " MB";
            }
        }

        public string physicalmemory
        {
            get
            {
                return (System.Diagnostics.Process.GetCurrentProcess().WorkingSet64 / (1024 * 1024)) + " MB";
            }
        }

        public string peakphysicalmemory
        {
            get
            {
                return (System.Diagnostics.Process.GetCurrentProcess().PeakWorkingSet64 / (1024 * 1024)) + " MB";
            }
        }

        public string systemtype
        {
            get
            {
                if (Environment.Is64BitOperatingSystem)
                {
                    return "64 bit";
                }
                else
                {
                    return "32 bit";
                }
            }
        }

        public string processtype
        {
            get
            {
                if (Environment.Is64BitProcess)
                {
                    return "64 bit";
                }
                else
                {
                    return "32 bit";
                }
            }
        }

        public int sessionsinuse
        {
            get
            {
                return _SessionsCounter.Count();
            }
        }

        public ServiceStatus(ApplicationTypes.iCounter SessionsCounter)
        {
            _SessionsCounter = SessionsCounter;
        }
    }
}
