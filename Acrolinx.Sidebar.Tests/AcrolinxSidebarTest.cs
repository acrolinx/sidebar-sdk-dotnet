/* Copyright (c) 2016 Acrolinx GmbH */

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Acrolinx.Sdk.Sidebar.Tests
{
    [TestClass]
    public class AcrolinxSidebarTest
    {
        AcrolinxSidebar sidebar;
        [TestMethod]
        public void SupportCheckSelectionIsSet()
        {
            sidebar = new AcrolinxSidebar();
            Assert.IsFalse(sidebar.SupportCheckSelection);
            sidebar.SupportCheckSelection = true;
            Assert.IsTrue(sidebar.SupportCheckSelection);
        }

    }
}
