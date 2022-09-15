/* Copyright (c) 2022-present Acrolinx GmbH */
using Microsoft.Win32;

namespace Acrolinx.Sdk.Sidebar.Util
{
    public class RegistryUtil
    {
        private static string regKeyPath = @"Software\Acrolinx\PlugIns";
        public static object ReadHKCU(string keyPath, string key)
        {
            RegistryKey sk = Registry.CurrentUser.OpenSubKey(regKeyPath + keyPath);
            return sk?.GetValue(key);
        }
    }
}
