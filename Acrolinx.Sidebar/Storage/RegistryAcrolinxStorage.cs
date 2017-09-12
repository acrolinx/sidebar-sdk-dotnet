using Acrolinx.Sdk.Sidebar.Util.Logging;
using Microsoft.Win32;
using System;

namespace Acrolinx.Sdk.Sidebar.Storage
{
    public sealed class RegistryAcrolinxStorage : IAcrolinxStorage
    {
        private static readonly WeakReference instanceRef = new WeakReference(new RegistryAcrolinxStorage());
        private const string baseKeyPath = @"Software\Acrolinx\PlugIns\";
        private const string storeKey = @"Storage";
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
            RegistryAcrolinxStorage instance = instanceRef.Target as RegistryAcrolinxStorage;
            if (instance == null)
            {
                instance = new RegistryAcrolinxStorage();
                instanceRef.Target = instance;
            }

            return instance;
        }
        public string GetItem(string key)
        {
            RegistryKey sk = Registry.CurrentUser.OpenSubKey(keyPath);
            string value = null;

            if (sk != null)
            {
                value = sk.GetValue(key) as string;
            }
            if (string.IsNullOrEmpty(value))
            {
                sk = Registry.LocalMachine.OpenSubKey(keyPath);
                if (sk != null)
                {
                    value = sk.GetValue(key) as string;
                }
            }
            return value;
        }
        public void RemoveItem(string key)
        {
            RegistryKey sk = Registry.CurrentUser.OpenSubKey(keyPath, true);
            if (sk != null)
            {
                sk.DeleteValue(key);
            }
        }
        public void SetItem(string key, string value)
        {
            RegistryKey sk = Registry.CurrentUser.CreateSubKey(keyPath);
            sk.SetValue(key, value);
        }

    }
}
