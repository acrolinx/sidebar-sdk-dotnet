/* Copyright (c) 2024 Acrolinx GmbH */

using Acrolinx.Sdk.Sidebar.Documents;
using Acrolinx.Sdk.Sidebar.Util.Changetracking;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using Range = Acrolinx.Sdk.Sidebar.Documents.Range;

namespace Acrolinx.Sdk.Sidebar.Tests
{
    [TestClass]
    public class LookupPerformaceTests
    {
        [TestMethod]
        public void PerfromanceTestXML20KB()
        {
            string xml = readResource("Acrolinx.Sdk.Sidebar.Tests.testFiles.LookupXML_1_20KB.xml");
            string text = readResource("Acrolinx.Sdk.Sidebar.Tests.testFiles.LookupXML_1_20KB.txt");
            string match = "acrolinx";
            performanceTestHelper(xml, text, match, 500);
        }

        [TestMethod]
        public void PerfromanceTestXML200KB()
        {
            string xml = readResource("Acrolinx.Sdk.Sidebar.Tests.testFiles.LookupXML_2_200KB.xml");
            string text = readResource("Acrolinx.Sdk.Sidebar.Tests.testFiles.LookupXML_2_200KB.txt");
            string match = "AcroMatch1";
            performanceTestHelper(xml, text, match, 500);
        }

        public void performanceTestHelper(string originalMarkup, string innerText, string match, int expectedCompletionTimeInMs)
        {
            int start = originalMarkup.IndexOf(match);
            var lookup = new Lookup(originalMarkup, Lookup.LookupStrategy.TEXTDIFF);
            lookup.diffOptions.diffInputFormat = DiffInputFormat.MARKUP;
            var ranges = new Range[] { new Range(start, start + match.Length) };

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            var result = lookup.Search(innerText, ranges);
            stopwatch.Stop();

            Assert.AreEqual(1, result.Count);

            int expectedStart = innerText.IndexOf(match);
            int expectedEnd = expectedStart + match.Length;
            Assert.AreEqual(expectedStart, result[0].Start);
            Assert.AreEqual(expectedEnd, result[0].End);
            Console.WriteLine("Time Elapse for Diff in Ms: " + stopwatch.ElapsedMilliseconds);

            Assert.IsTrue(stopwatch.ElapsedMilliseconds < expectedCompletionTimeInMs,
                "Diff took more than " + expectedCompletionTimeInMs + "ms. Required Time: " + stopwatch.ElapsedMilliseconds);
        }


        private string readResource(string resourceName)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var x = assembly.GetManifestResourceNames();


            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                string result = reader.ReadToEnd();
                return result;
            }
        }
    }
}
