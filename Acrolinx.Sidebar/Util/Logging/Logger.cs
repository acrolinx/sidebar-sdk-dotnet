using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Xml;
using log4net.Config;

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
                return Path.GetTempPath() + "Acrolinx\\logs";
            }
        }

        /* This function is deprecated use InitializeLog*/
        public static void LogToConsole()
        {
            /* Create a new console trace listener and add it to the trace listeners. */
            ConsoleTraceListener consoletraceListener = new ConsoleTraceListener();
            Trace.Listeners.Add(consoletraceListener);
            Trace.AutoFlush = true;
        }

        /* This function is deprecated use InitializeLog*/
        public static void LogToFile()
        {
            try
            {
                System.IO.Directory.CreateDirectory(Logging.Logger.Directory);
                Stream fileStream = System.IO.File.Create(Directory + "\\" + DateTime.Now.ToString("yyyy-MM-dd") + "-" + Assembly.GetCallingAssembly().GetName().Name + ".log");

                /* Create a new text writer using the output stream, and add it to
                 * the trace listeners. */
                TextWriterTraceListener logTextListener = new TextWriterTraceListener(fileStream);
                Trace.Listeners.Add(logTextListener);
                Trace.AutoFlush = true;
            }
            catch (Exception e)
            {
                // To default trace
                Trace.WriteLine(e.Message);
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
                    docConfig.LoadXml(Properties.Config.Config_Resource_log4net);
                    XmlElement eleConfig = docConfig.DocumentElement;
                    XmlConfigurator.Configure(eleConfig);
                }
            }
            catch (Exception e)
            {
                // To default trace
                Trace.WriteLine(e.Message);
            }
        }

    }
}
