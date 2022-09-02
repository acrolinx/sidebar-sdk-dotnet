/* Copyright 2021-present Acrolinx GmbH */

using Microsoft.Win32;
using System;

namespace Acrolinx.Sdk.Sidebar.Util
{
    internal static class NetFrameworkVersionUtil
    {

        internal static bool IsDotNetFramework472PlusInstalled()
        {

            using (RegistryKey ndpKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32).OpenSubKey("SOFTWARE\\Microsoft\\NET Framework Setup\\NDP\\v4\\Full\\"))
            {
                int releaseKey = Convert.ToInt32(ndpKey.GetValue("Release"));

                // Value obtained from official MS docs
                // https://docs.microsoft.com/en-us/dotnet/framework/migration-guide/how-to-determine-which-versions-are-installed?redirectedfrom=MSDN#minimum-version
                if (releaseKey >= 461808)
                {
                    return true;
                }
                return false;
            }
        }
    }
}


