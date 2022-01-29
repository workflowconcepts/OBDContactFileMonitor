using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;

namespace com.workflowconcepts.applications.filemonitor
{
    public class Log
    {
        static Logger _Instance = null;

        public static Logger Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = LogManager.GetCurrentClassLogger();
                }

                return _Instance;
            }
        }

        public static void ConfigureFileTarget(string Drive, string CompanyName, string ProductName, LogLevel MinimumLogLevel, int MaxNumberOfArchiveFiles, long ArchiveFilesAboveSize)
        {
            try
            {
                var fileTarget = new NLog.Targets.FileTarget("fileTarget")
                {
                    FileName = Drive + "/" + CompanyName + "/" + ProductName + "/${processname}_.log"

                    ,
                    Layout = "${longdate}|${level:uppercase=true}|PID:${processid} THR Name:${threadname} Id:${threadid}|${callsite:className=True:methodName=True:includeNamespace=False}()|${message}"

                    ,
                    ArchiveFileName = Drive + "/" + CompanyName + "/" + ProductName + "/${processname}_{#}.log"

                    ,
                    ArchiveNumbering = NLog.Targets.ArchiveNumberingMode.Sequence

                    ,
                    ArchiveAboveSize = ArchiveFilesAboveSize

                    ,
                    MaxArchiveFiles = MaxNumberOfArchiveFiles
                };

                NLog.Targets.Wrappers.AsyncTargetWrapper AsyncWrapper = new NLog.Targets.Wrappers.AsyncTargetWrapper(fileTarget, 5000, NLog.Targets.Wrappers.AsyncTargetWrapperOverflowAction.Discard);

                AsyncWrapper.Name = "asyncwrapper";

                NLog.Config.LoggingRule FileTargetRule = new NLog.Config.LoggingRule("*", MinimumLogLevel, LogLevel.Fatal, AsyncWrapper);

                FileTargetRule.RuleName = "FileTargetRule";

                if (NLog.LogManager.Configuration == null)
                {
                    NLog.LogManager.Configuration = new NLog.Config.LoggingConfiguration();
                }

                NLog.LogManager.Configuration.RemoveTarget(AsyncWrapper.Name);

                NLog.LogManager.Configuration.RemoveRuleByName(FileTargetRule.RuleName);

                NLog.LogManager.Configuration.AddTarget(AsyncWrapper.Name, AsyncWrapper);
                NLog.LogManager.Configuration.LoggingRules.Add(FileTargetRule);

                NLog.LogManager.ReconfigExistingLoggers();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception:{ex.Message} {Environment.NewLine} StackTrace:{ex.StackTrace}");
            }
        }

        public static void ConfigureConsoleTarget(LogLevel MinimumLogLevel)
        {
            try
            {
                var consoleTarget = new NLog.Targets.ConsoleTarget("consoleTarget")
                {
                    Layout = "${longdate}|${level:uppercase=true}|PID:${processid} THR Name:${threadname} Id:${threadid}|${callsite:className=True:methodName=True:includeNamespace=False}()|${message}"
                };

                var ConsoleTargetRule = new NLog.Config.LoggingRule("*", MinimumLogLevel, LogLevel.Fatal, consoleTarget);

                ConsoleTargetRule.RuleName = "ConsoleTargetRule";

                NLog.LogManager.Configuration.RemoveTarget(consoleTarget.Name);

                NLog.LogManager.Configuration.RemoveRuleByName(ConsoleTargetRule.RuleName);

                NLog.LogManager.Configuration.AddTarget(consoleTarget.Name, consoleTarget);

                NLog.LogManager.Configuration.LoggingRules.Add(ConsoleTargetRule);

                NLog.LogManager.ReconfigExistingLoggers();
            }
            catch
            {

            }
        }

        public static void ConfigureFileTarget(string Drive, string CompanyName, string ProductName, string DebugLevel, string MaxNumberOfArchiveFiles, string ArchiveFilesAboveSize)
        {
            try
            {
                NLog.LogLevel _level = NLog.LogLevel.Info;

                long lFileSize = 0L;

                int iMaxNumberOfArchiveFiles = 0;

                if (!String.IsNullOrEmpty(DebugLevel))
                {
                    try
                    {
                        ApplicationTypes.LogLevel level = (ApplicationTypes.LogLevel)Enum.Parse(typeof(ApplicationTypes.LogLevel), DebugLevel.ToUpper());

                        switch (level)
                        {
                            case ApplicationTypes.LogLevel.TRACE:

                                _level = NLog.LogLevel.Trace;

                                break;

                            case ApplicationTypes.LogLevel.DEBUG:

                                _level = NLog.LogLevel.Debug;

                                break;

                            case ApplicationTypes.LogLevel.INFORMATION:

                                _level = NLog.LogLevel.Info;

                                break;

                            case ApplicationTypes.LogLevel.WARNING:

                                _level = NLog.LogLevel.Warn;

                                break;

                            case ApplicationTypes.LogLevel.ERROR:

                                _level = NLog.LogLevel.Error;

                                break;

                            case ApplicationTypes.LogLevel.FATAL:

                                _level = NLog.LogLevel.Fatal;

                                break;

                            default:

                                _level = NLog.LogLevel.Info;

                                break;
                        }
                    }
                    catch
                    {
                        _level = NLog.LogLevel.Info;
                    }
                }
                else
                {
                    _level = NLog.LogLevel.Info;
                }

                if (!String.IsNullOrEmpty(ArchiveFilesAboveSize))
                {
                    if (ArchiveFilesAboveSize.ToUpper() == "ONEMEGABYTE")
                    {
                        lFileSize = 1048576;
                    }
                    else if (ArchiveFilesAboveSize.ToUpper() == "THREEMEGABYTES")
                    {
                        lFileSize = 3145728;
                    }
                    else if (ArchiveFilesAboveSize.ToUpper() == "FIVEMEGABYTES")
                    {
                        lFileSize = 5242880;
                    }
                    else
                    {
                        lFileSize = 3145728;
                    }
                }
                else
                {
                    lFileSize = 3145728;
                }

                if (!string.IsNullOrEmpty(MaxNumberOfArchiveFiles))
                {
                    iMaxNumberOfArchiveFiles = 0;

                    if (int.TryParse(MaxNumberOfArchiveFiles, out iMaxNumberOfArchiveFiles))
                    {

                    }
                    else
                    {
                        iMaxNumberOfArchiveFiles = 100;
                    }
                }
                else
                {
                    iMaxNumberOfArchiveFiles = 100;
                }

                Log.ConfigureFileTarget(Drive, CompanyName, ProductName, _level, iMaxNumberOfArchiveFiles, lFileSize);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception:{ex.Message} {Environment.NewLine} StackTrace:{ex.StackTrace}");
            }
        }

        public static void RemoveFileTarget()
        {
            try
            {
                if (NLog.LogManager.Configuration != null)
                {
                    NLog.LogManager.Configuration.RemoveTarget("asyncwrapper");

                    NLog.LogManager.Configuration.RemoveRuleByName("FileTargetRule");

                    NLog.LogManager.ReconfigExistingLoggers();
                }
            }
            catch
            {

            }
        }

        public static void RemoveConsoleTarget()
        {
            try
            {
                if (NLog.LogManager.Configuration != null)
                {
                    NLog.LogManager.Configuration.RemoveTarget("consoleTarget");

                    NLog.LogManager.Configuration.RemoveRuleByName("ConsoleTargetRule");

                    NLog.LogManager.ReconfigExistingLoggers();
                }
            }
            catch
            {

            }
        }

        public static void RemoveAllLogging()
        {
            try
            {
                if (NLog.LogManager.Configuration != null)
                {
                    List<string> ToBeRemoved = new List<string>();

                    foreach (NLog.Targets.Target t in NLog.LogManager.Configuration.AllTargets)
                    {
                        if (!string.IsNullOrEmpty(t.Name))
                        {
                            ToBeRemoved.Add(t.Name);
                        }

                    }//foreach(NLog.Targets.Target t in NLog.LogManager.Configuration.AllTargets)

                    foreach (string s in ToBeRemoved)
                    {
                        NLog.LogManager.Configuration.RemoveTarget(s);

                    }//foreach(string s in ToBeRemoved)

                    NLog.LogManager.Configuration.LoggingRules.Clear();

                    NLog.LogManager.ReconfigExistingLoggers();
                }
            }
            catch
            {

            }
        }
    }
}
