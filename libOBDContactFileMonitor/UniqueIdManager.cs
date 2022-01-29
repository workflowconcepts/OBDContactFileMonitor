using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.workflowconcepts.applications.filemonitor
{
    public static class UniqueIdManager
    {
        const long INITIAL_UNIQUE_ID = 10000;
        const long DISTANCE_FROM_MAX_VALUE = 1000000;

        static long _CurrentID = INITIAL_UNIQUE_ID;

        static object objLock = null;

        public static long GetNextUniqueID
        {
            get
            {
                if (objLock == null)
                {
                    objLock = new object();
                }

                lock (objLock)
                {
                    if (_CurrentID >= (long.MaxValue - DISTANCE_FROM_MAX_VALUE))
                    {
                        _CurrentID = INITIAL_UNIQUE_ID;
                    }

                    return _CurrentID++;

                }//lock (objLock)
            }
        }

        public static string GetNextUniqueIDAsString
        {
            get
            {
                return GetNextUniqueID.ToString();
            }
        }
    }
}
