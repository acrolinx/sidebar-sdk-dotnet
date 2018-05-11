/* Copyright (c) 2018 Acrolinx GmbH */

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Acrolinx.Sdk.Sidebar.Documents;

namespace Acrolinx.Sdk.Sidebar.Tests
{
    [TestClass]
    public class DocumentTest
    {
        [TestMethod]
        public void StringToFormat()
        {
            var document = new Document();
            Format actualFormat = document.StringToFormat("HTML");
            Format expectedFormat = Format.HTML;

            Assert.AreEqual(expectedFormat, actualFormat);
        }
        [TestMethod]
        public void StringToFormatInvalidFormat()
        {
            var document = new Document("", Format.HTML, "");
            Format actualFormat = document.StringToFormat("DITA");
            Format expectedFormat = Format.HTML;

            Assert.AreEqual(expectedFormat, actualFormat);
        }
    }
}
