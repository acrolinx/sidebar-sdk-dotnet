/* Copyright (c) 2022-present Acrolinx GmbH */
using Microsoft.Win32;

namespace Acrolinx.Sdk.Sidebar.Util
{
    public class RegistryUtil
    {
        public static object ReadHKCU(string keyPath, string key)
        {
            RegistryKey sk = Registry.CurrentUser.OpenSubKey(keyPath);
            return sk?.GetValue(key);
        }
    }
}
