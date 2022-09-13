/* Copyright (c) 2022-present Acrolinx GmbH */

using Acrolinx.Sdk.Sidebar.Util;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace Acrolinx.Sdk.Sidebar.Tests
{
    [TestClass]
    public class RegistryUtilTest
    {

        [TestMethod]
        public void ReadKeyFromHKCU()
        {
            var value = RegistryUtil.ReadHKCU(@"Software\Acrolinx\PlugIns", "randomKey465762");
            Assert.IsNull(value);
        }
    }
}
