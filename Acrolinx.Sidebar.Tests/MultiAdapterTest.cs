/* Copyright (c) 2016 Acrolinx GmbH */

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Moq;
using Acrolinx.Sdk.Sidebar.Util.Adapter;
using Acrolinx.Sdk.Sidebar.Documents;
using Newtonsoft.Json.Linq;

namespace Acrolinx.Sdk.Sidebar.Tests
{
    [TestClass]
    public class MultiAdapterTest
    {
        [TestMethod]
        public void AdapterIsExtracted()
        {
            var adapter = new Mock<IAdapter>();

            adapter.Setup(service => service.Extract(Format.XML)).Returns("<x>test</x>");
            var multiAdapter = new MultiAdapter("foo", new IAdapter[] { adapter.Object }, Format.XML);

            Assert.AreEqual("<foo><x>test</x></foo>", multiAdapter.Document.Content);
        }
    }
}
