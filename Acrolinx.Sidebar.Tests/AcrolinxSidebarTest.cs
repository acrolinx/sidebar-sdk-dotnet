﻿using Acrolinx.Sdk.Sidebar;
/* Copyright (c) 2016 Acrolinx GmbH */

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Web.WebView2.WinForms;

namespace Acrolinx.Sdk.Sidebar.Tests
{
    [TestClass]
    public class AcrolinxSidebarTest
    {
        [TestMethod]
        public void SupportCheckSelectionIsSet()
        {
            AcrolinxSidebar sidebar = new AcrolinxSidebar();
            Assert.IsFalse(sidebar.SupportCheckSelection);
            sidebar.SupportCheckSelection = true;
            Assert.IsTrue(sidebar.SupportCheckSelection);
        }

        [TestMethod()]
        public void CheckRequestWithNoOptionsTest()
        {
            AcrolinxSidebar sidebar = new AcrolinxSidebar();
            WebView2 webView2 = new Microsoft.Web.WebView2.WinForms.WebView2();
            var plugin = new AcrolinxPlugin(webView2, sidebar);
            try
            {
                object[] o = { null };
                plugin.requestGlobalCheck(o);
            }
            catch (Exception ex)
            {
                Assert.Fail("Expected no exception, but got: " + ex.Message);
            }
        }

        [TestMethod()]
        public void OpenWindow_ValidHttpsUrl_ReturnsTrue()
        {
            AcrolinxSidebar sidebar = new AcrolinxSidebar();
            WebView2 webView2 = new Microsoft.Web.WebView2.WinForms.WebView2();
            var plugin = new AcrolinxPlugin(webView2, sidebar);
            try
            {
                // Arrange
                string json = "{\"url\": \"https://www.acrolinx.com/\"}";
                dynamic[] parameters = { json };

                // Act
                bool result = plugin.openWindow(parameters);

                // Assert
                Assert.IsTrue(result);
            }
            catch (Exception ex)
            {
                Assert.Fail("Expected no exception, but got: " + ex.Message);
            }
        }

        [TestMethod]
        public void OpenWindow_InvalidUrl_ReturnsFalse()
        {
            AcrolinxSidebar sidebar = new AcrolinxSidebar();
            WebView2 webView2 = new Microsoft.Web.WebView2.WinForms.WebView2();
            var plugin = new AcrolinxPlugin(webView2, sidebar);

            // Arrange
            string json = "{\"url\": \"invalid-url\"}";
            dynamic[] parameters = { json };

            // Act
            bool result = plugin.openWindow(parameters);

            // Assert
            Assert.IsFalse(result);
        }
    }
}