/* Copyright (c) 2024 Acrolinx GmbH */

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Acrolinx.Sdk.Sidebar.Util.Changetracking.Tests
{
    [TestClass()]
    public class TextExtractionTests
    {
        [TestMethod()]
        public void TwoTags()
        {
            string html = "01<t/>67<li/>34";
            Tuple<string, List<Tuple<double, double>>> extraction = TextExtraction.extractText(html);

            Assert.AreEqual("016734", extraction.Item1);

            DiffBasedLookup dbl = new DiffBasedLookup(html);
            Assert.AreEqual(0, dbl.FindNewIndex(extraction.Item2, 0));
            Assert.AreEqual(1, dbl.FindNewIndex(extraction.Item2, 1));
            Assert.AreEqual(6 - 4, dbl.FindNewIndex(extraction.Item2, 6));
            Assert.AreEqual(7 - 4, dbl.FindNewIndex(extraction.Item2, 7));
            Assert.AreEqual(13 - 9, dbl.FindNewIndex(extraction.Item2, 13));
            Assert.AreEqual(14 - 9, dbl.FindNewIndex(extraction.Item2, 14));
        }

        [TestMethod()]
        public void InlineTags()
        {
            string html = "1<b>2</b>3";
            Tuple<string, List<Tuple<double, double>>> extraction = TextExtraction.extractText(html);

            Assert.AreEqual("123", extraction.Item1);
        }

        [TestMethod()]
        public void LineBreakingEndTags()
        {
            string html = "<p>1</p>2";
            Tuple<string, List<Tuple<double, double>>> extraction = TextExtraction.extractText(html);

            Assert.AreEqual("1\n2", extraction.Item1);
        }

        [TestMethod()]
        public void LineBreakingSelfClosingTags()
        {
            string html = "1<br/>2";
            Tuple<string, List<Tuple<double, double>>> extraction = TextExtraction.extractText(html);

            Assert.AreEqual("1\n2", extraction.Item1);
        }

        [TestMethod()]
        public void LineBreakingAutoSelfClosingTags()
        {
            string html = "1<br>2";
            Tuple<string, List<Tuple<double, double>>> extraction = TextExtraction.extractText(html);

            Assert.AreEqual("1\n2", extraction.Item1);
        }


        [TestMethod()]
        public void EntitiesTest()
        {
            string html = "0&amp;1";
            Tuple<string, List<Tuple<double, double>>> extraction = TextExtraction.extractText(html);

            Assert.AreEqual("0&1", extraction.Item1);
        }

        [TestMethod()]
        public void ReplaceScriptsWithEmptyString()
        {
            string html = "1<script>2</script>3";
            Tuple<string, List<Tuple<double, double>>> extraction = TextExtraction.extractText(html);

            Assert.AreEqual("13", extraction.Item1);

            DiffBasedLookup dbl = new DiffBasedLookup(html);
            Assert.AreEqual(0, dbl.FindNewIndex(extraction.Item2, 0));
            Assert.AreEqual(1, dbl.FindNewIndex(extraction.Item2, html.IndexOf("3")));

        }

        [TestMethod()]
        public void ReplaceComplicatedScriptsWithEmptyString()
        {
            string html = "1<script type=\"text/javascript\">alert(\"<script>\");\n</script>3";
            Tuple<string, List<Tuple<double, double>>> extraction = TextExtraction.extractText(html);

            Assert.AreEqual("13", extraction.Item1);
        }

        [TestMethod()]
        public void ReplaceStyleWithWithEmptyString()
        {
            string html = "1<style>2</style>3";
            Tuple<string, List<Tuple<double, double>>> extraction = TextExtraction.extractText(html);

            Assert.AreEqual("13", extraction.Item1);
        }
    }
}