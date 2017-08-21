using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Acrolinx.Sdk.Sidebar.Util.Logging;
using System.Diagnostics.Contracts;

namespace Acrolinx.Sdk.Sidebar
{
    class StartPageInstaller
    {
        public static readonly string acrolinxStartPageDir = Path.GetTempPath() + @"acrolinx_start_page_" + GetStartPageVersion() + @"\";

        internal static string GetStartPageVersion()
        {
            var executingAssembly = Assembly.GetExecutingAssembly();

            var startPageVersion = executingAssembly.GetManifestResourceNames().
                Where(r => r.Contains("acrolinx_sidebar_startpage") && r.Contains("version.properties")).Single();

            Contract.Assume(!string.IsNullOrEmpty(startPageVersion));

            using (StreamReader stremReader = new StreamReader(executingAssembly.GetManifestResourceStream(startPageVersion)))
            {
                startPageVersion = stremReader.ReadToEnd();
                Contract.Assume(!string.IsNullOrEmpty(startPageVersion));
            }
            return startPageVersion.Split('=').Last();
        }

        internal static void ExportStartPageResources()
        {
            GetDefaultStartPageInstallLocation();

            var executingAssembly = Assembly.GetExecutingAssembly();

            var resourceNames = executingAssembly.GetManifestResourceNames();

            foreach (string r in resourceNames.Where(r => r.Contains("acrolinx_sidebar_startpage")))
            {
                using (Stream streamReader = executingAssembly.GetManifestResourceStream(r))
                {
                    if (!File.Exists(acrolinxStartPageDir + r))
                    {
                        using (var fileStream = new FileStream(acrolinxStartPageDir + r, FileMode.Create, FileAccess.Write))
                        {
                            streamReader.CopyTo(fileStream);
                        }
                    }
                }
            }
            Logger.AcroLog.Debug("Acrolinx start page resource extraction is complete");

            var files = Directory.GetFiles(acrolinxStartPageDir);
            var listFile = files.Where(r => r.Contains("files.txt")).Single();
            string[] lines = System.IO.File.ReadAllLines(listFile);
            if (File.Exists(listFile))
            {
                File.Move(listFile, acrolinxStartPageDir + "/files.txt");
            }
            foreach (string line in lines)
            {
                string dirName = line.Substring(0, line.LastIndexOf('/'));
                string objName = line.Split('/').Last();
                Directory.CreateDirectory(acrolinxStartPageDir + dirName);
                var mvFile = files.Where(r => r.Contains(objName)).Single();
                if (File.Exists(mvFile))
                {
                    File.Move(mvFile, acrolinxStartPageDir + dirName + "/" + objName);
                }
            }

            Logger.AcroLog.Debug("Exporting acrolinx start page resource is complete");
        }

        internal static string GetDefaultStartPageInstallLocation()
        {
            try
            {
                if (!Directory.Exists(acrolinxStartPageDir))
                {
                    Directory.CreateDirectory(acrolinxStartPageDir);
                    Logger.AcroLog.Debug("Creating acrolinx start page directory in: " + acrolinxStartPageDir);
                }
                return acrolinxStartPageDir;
            }
            catch (Exception ex)
            {
                Logger.AcroLog.Error(ex.Message);
            }
            return null;
        }

        internal static string GetStartPageURL()
        {
            GetDefaultStartPageInstallLocation();

            if (!File.Exists(acrolinxStartPageDir + @"version.properties")) // checking last file in list is enough
            {
                Logger.AcroLog.Debug("Acrolinx start page not present!");
                ExportStartPageResources();
            }

            return new Uri(acrolinxStartPageDir + @"index.html").AbsoluteUri;
        }
    }
}
