using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Acrolinx.Sdk.Sidebar.Util.Logging
{
    public static class Logger
    {
        public static string DirPath {
            get {
                return Path.GetTempPath() + "Acrolinx\\logs";
            }
        }

        public static void LogToConsole()
        {
            /* Create a new console trace listener and add it to the trace listeners. */
            ConsoleTraceListener consoletraceListener = new ConsoleTraceListener();
            Trace.Listeners.Add(consoletraceListener);
            Trace.AutoFlush = true;
        }

        public static void LogToFile()
        {
            try
            {
                System.IO.Directory.CreateDirectory(Logging.Logger.DirPath);
                Stream fileStream = System.IO.File.Create(DirPath + "\\" + DateTime.Now.ToString("yyyy-MM-dd") + "-" + Assembly.GetCallingAssembly().GetName().Name + ".log");

                /* Create a new text writer using the output stream, and add it to
                 * the trace listeners. */
                TextWriterTraceListener logTextListener = new TextWriterTraceListener(fileStream);
                Trace.Listeners.Add(logTextListener);
                Trace.AutoFlush = true;
            }catch (Exception e)
            {
                // To default trace
                Trace.WriteLine(e.Message);
            }
        }
    }
}
