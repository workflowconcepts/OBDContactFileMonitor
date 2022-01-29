using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.workflowconcepts.applications.filemonitor
{
    public abstract class _InterprocessCommunicationBase : MarshalByRefObject
    {
        protected static ApplicationTypes.iApplicationSettings _ApplicationSettings = null;
        protected static ApplicationTypes.SettingsCallback _SettingsChangedCallBack = null;

        public abstract ApplicationTypes.iApplicationSettings GetApplicationSettings();    
        
        public abstract bool SaveApplicationSettings(ApplicationTypes.iApplicationSettings Settings);
        
        public abstract string GetVersion();
     
        public static void SetSettingsChangedCallBack(ApplicationTypes.SettingsCallback Reference)
        {
            _SettingsChangedCallBack = Reference;
        }

        public static void SetApplicationSettingsReference(ApplicationTypes.iApplicationSettings Reference)
        {
            _ApplicationSettings = Reference;
        }
    }
}
