/* Copyright (c) 2020 Acrolinx GmbH */

using System;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.IO;
using System.Reflection;
using System.Threading;
using Acrolinx.Sdk.Sidebar.Util.Logging;

namespace Acrolinx.Sdk.Sidebar.Util
{
    internal class FileUtil
    {
        public static bool CopyFileWithRetries(string srcPath, string destPath, uint numTries)
        {
            Contract.Requires(!string.IsNullOrEmpty(srcPath));
            Contract.Requires(!string.IsNullOrEmpty(destPath));

            do
            {
                try
                {
                    File.Copy(srcPath, destPath, true);
                    return true;
                }
                catch (IOException ex)
                {
                    Logger.AcroLog.Info("An I/O error has occurred, may be start page is locked.");
                    Logger.AcroLog.Debug(ex.Message);
                    Thread.Sleep(50);
                }
                catch (Exception ex)
                {
                    Logger.AcroLog.Error(ex.Message);
                    break;
                }
                numTries--;
            } while (numTries > 0);
            return false;
        }

        public static void CopyStream(Stream stream, string destPath)
        {
            using (var fileStream = new FileStream(destPath, FileMode.Create, FileAccess.Write))
            {
                stream.CopyTo(fileStream);
            }
        }

        public static string GetAppDataPath()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        }

        public static string GetFileVersion(string filePath)
        {
            Contract.Requires(!string.IsNullOrEmpty(filePath));
            string version = null;
            try
            {
                version = FileVersionInfo.GetVersionInfo(filePath).FileVersion;
            }
            catch (FileNotFoundException ex)
            {
                Logger.AcroLog.Debug(ex.Message);
            }
            return version;
        }

        public static void WriteResourceToFile(string resourceName, string fileName)
        {
            using (var resource = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName))
            {
                using (var file = new FileStream(fileName, FileMode.Create, FileAccess.Write))
                {
                    resource.CopyTo(file);
                }
            }
        }

        public static void ExtractZipFile(string sourcePath, string destinationPath, bool overwrite = false)
        {
            if (overwrite)
            {
                System.IO.DirectoryInfo di = new DirectoryInfo(destinationPath);

                if (di.Exists)
                {
                    foreach (FileInfo file in di.GetFiles())
                    {
                        file.Delete();
                    }
                    foreach (DirectoryInfo dir in di.GetDirectories())
                    {
                        dir.Delete(true);
                    }
                }
            }
            System.IO.Compression.ZipFile.ExtractToDirectory(sourcePath, destinationPath);
        }
    }
}
