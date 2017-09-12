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
        public void ClearItem()
        {
            RegistryAcrolinxStorage storage = RegistryAcrolinxStorage.Instance;

            storage.SetItem("acro.test.token1", "tokendata1");
            storage.SetItem("acro.test.token2", "tokendata2");
            storage.Clear();

            var value = storage.GetItem("acro.test.token1");

            Assert.AreEqual(value, null);

            value = storage.GetItem("acro.test.token2");

            Assert.AreEqual(value, null);
        }
    }
}
