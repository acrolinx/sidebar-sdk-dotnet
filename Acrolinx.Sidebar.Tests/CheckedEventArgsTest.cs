/* Copyright (c) 2016 Acrolinx GmbH */
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Acrolinx.Sdk.Sidebar.Documents;

namespace Acrolinx.Sdk.Sidebar.Tests
{
    [TestClass]
    public class CheckedEventArgsTest
    {
        [TestMethod]
        public void GetEmbedCheckDataAsEmbeddableStringForXML()
        {
            Dictionary<string, string> embedCheckInfo = new Dictionary<string, string>();
            embedCheckInfo.Add("timeStarted", "2018-05-10T10:59:41Z");
            embedCheckInfo.Add("score", "85");
            embedCheckInfo.Add("status", "green");
            embedCheckInfo.Add("scorecardUrl", "http://brd11158:8031/output/en/Welcome_htm_reshma_3b55676acd8a9c85_1517131731_report.html");

            CheckedEventArgs checkArgs = new CheckedEventArgs("id0", new Range(10,20), embedCheckInfo, Format.XML);
            string actualResult = checkArgs.GetEmbedCheckDataAsEmbeddableString();
            string expectedResult = "<?acrolinxCheckData timeStarted=\"2018-05-10T10:59:41Z\" score=\"85\" status=\"green\" scorecardUrl=\"http://brd11158:8031/output/en/Welcome_htm_reshma_3b55676acd8a9c85_1517131731_report.html\" ?>";

            Assert.AreEqual(expectedResult, actualResult);
        }
        [TestMethod]
        public void GetEmbedCheckDataAsEmbeddableStringForHTML()
        {
            Dictionary<string, string> embedCheckInfo = new Dictionary<string, string>();
            embedCheckInfo.Add("timeStarted", "2018-05-10T11:04:04Z");
            embedCheckInfo.Add("score", "85");
            embedCheckInfo.Add("status", "green");
            embedCheckInfo.Add("scorecardUrl", "http://brd11158:8031/output/en/Welcome_htm_reshma_3b55676acd8a9c85_1946298077_report.html");

            CheckedEventArgs checkArgs = new CheckedEventArgs("id0", new Range(10, 20), embedCheckInfo, Format.HTML);
            string actualResult = checkArgs.GetEmbedCheckDataAsEmbeddableString();
            string expectedResult = "<meta name=\"acrolinxCheckData\" timeStarted=\"2018-05-10T11:04:04Z\" score=\"85\" status=\"green\" scorecardUrl=\"http://brd11158:8031/output/en/Welcome_htm_reshma_3b55676acd8a9c85_1946298077_report.html\" />";

            Assert.AreEqual(expectedResult, actualResult);
        }
        [TestMethod]
        public void GetEmbedCheckDataAsEmbeddableStringForMARKDOWN()
        {
            Dictionary<string, string> embedCheckInfo = new Dictionary<string, string>();
            embedCheckInfo.Add("timeStarted", "2018-05-10T11:12:17Z");
            embedCheckInfo.Add("score", "85");
            embedCheckInfo.Add("status", "green");
            embedCheckInfo.Add("scorecardUrl", "http://brd11158:8031/output/en/Welcome_htm_reshma_3b55676acd8a9c85_118482580_report.html");

            CheckedEventArgs checkArgs = new CheckedEventArgs("id0", new Range(10, 20), embedCheckInfo, Format.Markdown);
            string actualResult = checkArgs.GetEmbedCheckDataAsEmbeddableString();
            string expectedResult = "<!--name=\"acrolinxCheckData\" timeStarted=\"2018-05-10T11:12:17Z\" score=\"85\" status=\"green\" scorecardUrl=\"http://brd11158:8031/output/en/Welcome_htm_reshma_3b55676acd8a9c85_118482580_report.html\" -->";

            Assert.AreEqual(expectedResult, actualResult); 
        }
    }



}
