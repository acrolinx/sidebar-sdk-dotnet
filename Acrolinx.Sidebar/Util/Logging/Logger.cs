using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Xml;
using log4net.Config;
using log4net.Repository.Hierarchy;
using log4net;
using log4net.Appender;
using System.Linq;

namespace Acrolinx.Sdk.Sidebar.Util.Logging
{
    public static class Logger
    {
        private static readonly log4net.ILog acroLog = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static log4net.ILog AcroLog{
            get
            {
                return acroLog;
            }
        }

        public static string Directory {
            get {

                var rootAppender = ((Hierarchy)LogManager.GetRepository())
                                         .Root.Appenders.OfType<FileAppender>()
                                         .FirstOrDefault();

                return rootAppender != null ? rootAppender.File : string.Empty;
            }
        }

        public static void InitializeLog()
        {
            try
            {
                log4net.GlobalContext.Properties["appname"] = Util.AssemblyUtil.AppInfo()["applicationName"];
                string dllPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                if (File.Exists(dllPath + "\\Acrolinx.Sidebar.log4net.xml"))
                {
                    XmlConfigurator.Configure(new FileInfo(dllPath + "\\Acrolinx.Sidebar.log4net.xml"));
                }
                else
                {
                    XmlDocument docConfig = new XmlDocument();
                    docConfig.XmlResolver = null;
                    docConfig.LoadXml(Properties.Config.Config_Resource_log4net);
                    XmlElement eleConfig = docConfig.DocumentElement;
                    XmlConfigurator.Configure(eleConfig);
                }
            }
            catch (Exception e)
            {
                Trace.WriteLine(e.Message);
            }
        }

    }
}
