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
        public void SetItemGetItemInitStorage()
        {
            RegistryAcrolinxStorage storage = RegistryAcrolinxStorage.Instance;

            storage.SetItem("acro.test.token1", "tokendata1");
            storage.SetItem("acro.test.token2", "tokendata2");

            var value = storage.GetItem("acro.test.token1");

            Assert.AreEqual(value, "tokendata1");

            value = storage.GetItem("acro.test.token2");

            Assert.AreEqual(value, "tokendata2");
        }

        [TestMethod]
        public void GetAllItems()
        {
            RegistryAcrolinxStorage storage = RegistryAcrolinxStorage.Instance;

            storage.SetItem("acro.test.token1", "tokendata1");
            storage.SetItem("acro.test.token2", "tokendata2");
            storage.SetItem("acro.test.token3", "tokendata3");
            storage.SetItem("acro.test.token4", "tokendata4");
            storage.SetItem("acro.test.token5", "tokendata5");


            var items = storage.GetAllItems();

            Assert.AreEqual(items.GetValue("acro.test.token1"), "tokendata1");
            Assert.AreEqual(items.GetValue("acro.test.token2"), "tokendata2");
            Assert.AreEqual(items.GetValue("acro.test.token3"), "tokendata3");
            Assert.AreEqual(items.GetValue("acro.test.token4"), "tokendata4");
            Assert.AreEqual(items.GetValue("acro.test.token5"), "tokendata5");
        }

        [TestMethod]
        public void GetItemFromHKLM()
        {
            RegistryAcrolinxStorage storage = RegistryAcrolinxStorage.Instance;

            var value = storage.GetItem("unknown");

            Assert.AreEqual(value, null);
        }

        [TestMethod]
        public void RemoveItem()
        {
            RegistryAcrolinxStorage storage = RegistryAcrolinxStorage.Instance;

            storage.SetItem("acro.test.token1", "tokendata1");
            storage.SetItem("acro.test.token2", "tokendata2");
            storage.RemoveItem("acro.test.token2");

            var value = storage.GetItem("acro.test.token2");

            Assert.AreEqual(value, null);

            value = storage.GetItem("acro.test.token1");

            Assert.AreEqual(value, "tokendata1");

        }

        [TestMethod]
        public void OverwriteAcrolinxStorage()
        {
            RegistryAcrolinxStorage storage = RegistryAcrolinxStorage.Instance;

            storage.SetItem("acro.test.token1", "tokendata1");
            storage.SetItem("acro.test.token1", "tokendata2");

            var value = storage.GetItem("acro.test.token1");

            Assert.AreEqual(value, "tokendata2");
        }

    }
}
