/* Copyright (c) 2020 Acrolinx GmbH */

using System;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.IO;
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
    }
}
