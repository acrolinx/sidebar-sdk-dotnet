using Acrolinx.Sdk.Sidebar.Util.Logging;
using Microsoft.Win32;
using System;

namespace Acrolinx.Sdk.Sidebar.Storage
{
    public sealed class RegistryAcrolinxStorage : IAcrolinxStorage
    {
        private static RegistryAcrolinxStorage instance;
        private const string baseKeyPath = @"Software\Acrolinx\PlugIns\";
        private const string storeKey = @"Storage\AcrolinxStorage";
        private const string keyPath = baseKeyPath + storeKey;
        private RegistryAcrolinxStorage() { }
        static public RegistryAcrolinxStorage Instance
        {
            get
            {
                return GetInstance();
            }
        }
        static private RegistryAcrolinxStorage GetInstance()
        {
            if (instance == null)
            {
                instance = new RegistryAcrolinxStorage();
            }

            return instance;
        }
        public string GetItem(string key)
        {
            RegistryKey sk = Registry.CurrentUser.OpenSubKey(keyPath);
            string value = null;

            value = sk?.GetValue(key) as string;

            if (string.IsNullOrEmpty(value))
            {
                sk = Registry.LocalMachine.OpenSubKey(keyPath);

                value = sk?.GetValue(key) as string;
            }
            return value;
        }
        public void RemoveItem(string key)
        {
            RegistryKey sk = Registry.CurrentUser.OpenSubKey(keyPath, true);
            try
            {
                sk?.DeleteValue(key);
            }
            catch (Exception ex)
            {
                Logger.AcroLog.Error("Key could not be deleted" + ex.Message);
            }
        }
        public void SetItem(string key, string value)
        {
            RegistryKey sk = Registry.CurrentUser.CreateSubKey(keyPath);
            sk.SetValue(key, value);
        }

    }
}
