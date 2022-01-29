using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace com.workflowconcepts.applications.filemonitor
{
    public class InterprocessCommunicationServer : _InterprocessCommunicationBase
    {  
        public override ApplicationTypes.iApplicationSettings GetApplicationSettings()
        {
            return _ApplicationSettings;
        }

        public override bool SaveApplicationSettings(ApplicationTypes.iApplicationSettings Settings)
        {
             Log.Instance.Info("Enter");

            try
            {
                if (_SettingsChangedCallBack != null)
                {
                    _SettingsChangedCallBack(Settings);
                }

                return true;
            }
            catch (Exception ex)
            {
                Log.Instance.Error("Exception:" + ex.Message + Environment.NewLine + "StackTrace:" + ex.StackTrace);

                return false;
            }
        }

        public override string GetVersion()
        {
            return typeof(InterprocessCommunicationServer).Assembly.GetName().Version.ToString();
        }
    }
}
