/* Copyright 2021-present Acrolinx GmbH */

using Microsoft.Win32;

namespace Acrolinx.Sdk.Sidebar.Util
{
    internal static class NetFrameworkVersionUtil
    {
        internal static bool IsNetFramework45PlusInstalled()
        {
            // .NET Framework 4.5 or above settings are stored in the following regedit
            const string subkey = @"SOFTWARE\Microsoft\NET Framework Setup\NDP\v4\Full\";

            using (RegistryKey ndpKey = Registry.LocalMachine.OpenSubKey(subkey))
            {
                if (ndpKey == null)
                {
                    return false;
                }
                if (ndpKey.GetValue("Version") != null)
                {
                    return true;
                }
                else
                {
                    if (ndpKey != null && ndpKey.GetValue("Release") != null)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
