using Acrolinx.Sdk.Sidebar.Util.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acrolinx.Sdk.Sidebar.Storage
{
    public sealed class JSONAcrolinxStorage : IAcrolinxStorage
    {
        private static JObject storage = new JObject();
        private static readonly WeakReference instanceRef = new WeakReference(new JSONAcrolinxStorage());
        private static string jsonFilePath;
        private static string defaultPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\.acrolinx\" + Util.AssemblyUtil.AppInfo()["applicationName"] + "_localStorage.json";

        private JSONAcrolinxStorage(){}
        static public JSONAcrolinxStorage Instance
        {
            get
            {
                return GetInstance();
            }
        }
        static private JSONAcrolinxStorage GetInstance()
        {
            JSONAcrolinxStorage instance = instanceRef.Target as JSONAcrolinxStorage;
            if (instance == null)
            {
                //No need for thread safty
                instance = new JSONAcrolinxStorage();
                instanceRef.Target = instance;
            }

            return instance;
        }
        public void InitStorage(string jsonFile)
        {
            Contract.Requires(jsonFile != null);

            if(IsWritableDirectoryCreated(jsonFile))
            {
                jsonFilePath = jsonFile;
                if (!File.Exists(jsonFilePath))
                {
                    StoreJson();
                }
                using (StreamReader file = File.OpenText(jsonFilePath))
                {
                    using (JsonTextReader reader = new JsonTextReader(file))
                    {
                        storage = (JObject)JToken.ReadFrom(reader);
                    }
                }
            }
            else
            {
                Logger.AcroLog.Error("Fail to create storage file at " + jsonFile);
            }
        }
        public string GetItem(string key)
        {
            if (jsonFilePath == null)
            {
                InitStorage(defaultPath);
            }

            JToken value;
            return storage.TryGetValue(key, out value) ? value.ToString() : null;
        }
        public void RemoveItem(string key)
        {
            storage.Remove(key);
            StoreJson();
        }
        public void SetItem(string key, string data)
        {
            if (jsonFilePath == null)
            {
                InitStorage(defaultPath);
            }
            storage[key] = data;
            StoreJson();
        }
        public void Clear()
        {
            if (jsonFilePath != null)
            {
                storage.RemoveAll();
                StoreJson();
            }
        }

        private void StoreJson()
        {
            using (StreamWriter file = File.CreateText(jsonFilePath))
            {
                using (JsonTextWriter writer = new JsonTextWriter(file))
                {
                    storage.WriteTo(writer);
                }
            }
        }

        private bool IsWritableDirectoryCreated(string jsonFile)
        {
            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(jsonFile));
            }
            catch (Exception ex)
            {
                Logger.AcroLog.Error(ex.Message);
                return false;
            }
            Logger.AcroLog.Info("Trying to create storage at " + jsonFile);
            return true;
        }
    }
}
