/* Copyright (c) 2016 Acrolinx GmbH */

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Acrolinx.Sdk.Sidebar.Util.Changetracking;
using Acrolinx.Sdk.Sidebar.Documents;
using Acrolinx.Sdk.Sidebar.Storage;
using System.IO;

namespace Acrolinx.Sdk.Sidebar.Tests
{
    [TestClass]
    public class AcrolinxStorageTest
    {
        [TestMethod]
        public void DefaultStorage()
        {
            JSONAcrolinxStorage storage = JSONAcrolinxStorage.Instance;
            storage.InitStorage(@"abc:\test_dotnet_sdk\test.json");

            storage.SetItem("acro.test.token1", "tokendata1");
            storage.SetItem("acro.test.token2", "tokendata2");

            var value = storage.GetItem("acro.test.token1");

            Assert.AreEqual(value, "tokendata1");

            value = storage.GetItem("acro.test.token2");

            Assert.AreEqual(value, "tokendata2");

            string defaultPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\.acrolinx\" + Util.AssemblyUtil.AppInfo()["applicationName"] + "_localStorage.json";

            Assert.AreEqual(File.Exists(defaultPath), true);

            File.Delete(defaultPath);
        }

        [TestMethod]
        public void SetItemGetItemInitStorage()
        {
            JSONAcrolinxStorage storage = JSONAcrolinxStorage.Instance;

            storage.InitStorage(Path.GetTempPath() + @"test_dotnet_sdk\test.json");

            storage.SetItem("acro.test.token1", "tokendata1");
            storage.SetItem("acro.test.token2", "tokendata2");

            var value = storage.GetItem("acro.test.token1");

            Assert.AreEqual(value, "tokendata1");

            value = storage.GetItem("acro.test.token2");

            Assert.AreEqual(value, "tokendata2");

            Assert.AreEqual(File.Exists(Path.GetTempPath() + "/test_dotnet_sdk/test.json"), true);

            File.Delete(Path.GetTempPath() + "/test_dotnet_sdk/test.json");
        }

        [TestMethod]
        public void RemoveItem()
        {
            JSONAcrolinxStorage storage = JSONAcrolinxStorage.Instance;

            storage.InitStorage(Path.GetTempPath() + "/test_dotnet_sdk/test.json");

            storage.SetItem("acro.test.token1", "tokendata1");
            storage.SetItem("acro.test.token2", "tokendata2");
            storage.RemoveItem("acro.test.token2");

            var value = storage.GetItem("acro.test.token2");

            Assert.AreEqual(value, null);

            value = storage.GetItem("acro.test.token1");

            Assert.AreEqual(value, "tokendata1");

            File.Delete(Path.GetTempPath() + "/test_dotnet_sdk/test.json");
        }

        [TestMethod]
        public void ClearItem()
        {
            JSONAcrolinxStorage storage = JSONAcrolinxStorage.Instance;

            storage.InitStorage(Path.GetTempPath() + "/test_dotnet_sdk/test.json");

            storage.SetItem("acro.test.token1", "tokendata1");
            storage.SetItem("acro.test.token2", "tokendata2");
            storage.Clear();

            var value = storage.GetItem("acro.test.token1");

            Assert.AreEqual(value, null);

            value = storage.GetItem("acro.test.token2");

            Assert.AreEqual(value, null);

            File.Delete(Path.GetTempPath() + "/test_dotnet_sdk/test.json");
            storage = null;
        }
    }
}
