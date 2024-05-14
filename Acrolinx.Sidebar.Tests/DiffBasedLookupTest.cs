/* Copyright (c) 2018 Acrolinx GmbH */

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Acrolinx.Sdk.Sidebar.Util.Changetracking;
using Acrolinx.Sdk.Sidebar.Documents;
using Range = Acrolinx.Sdk.Sidebar.Documents.Range;

namespace Acrolinx.Sdk.Sidebar.Tests
{
    [TestClass]
    public class DiffBasedLookupTest
    {
        [TestMethod]
        public void SimpleSearch()
        {
            var lookup = new DiffBasedLookup("This is an test");
            var range = new Range[] { new Range(0, "This".Length) };
            var result = lookup.TextDiffSearch("This is an test", range);

            Assert.AreEqual(result[0], range[0]);
        }

        [TestMethod]
        public void TextHasMoved()
        {
            var lookup = new DiffBasedLookup("This is an test");
            var range = new Range[] { new Range(0, "This".Length) };
            var result = lookup.TextDiffSearch("AA This is an test", range);

            Assert.AreEqual(result[0], new Range("AA ".Length, "AA This".Length));
        }

        [TestMethod]
        public void MultiRangeTextHasMoved()
        {
            var lookup = new DiffBasedLookup("This is an test");
            var range1 = new Range(0, "This".Length);
            var range2 = new Range("This ".Length, "This is".Length);
            var ranges = new Range[] { range1, range2 };
            var result = lookup.TextDiffSearch("AA This is an test", ranges);

            Assert.AreEqual(2, result.Count);

            Assert.AreEqual(result[0], new Range("AA ".Length, "AA This".Length));
            Assert.AreEqual(result[1], new Range("AA This ".Length, "AA This is".Length));
        }

        [TestMethod]
        public void MultiTextFoundAtCorrectPosition()
        {
            var lookup = new DiffBasedLookup("A A");
            var range1 = new Range[] { new Range(0, 1) };

            var result = lookup.TextDiffSearch("A A", range1);

            Assert.AreEqual(1, result.Count);

            Assert.AreEqual(result[0], new Range(0, 1));
        }

        [TestMethod]
        public void MultiTextFoundAtCorrectPosition2()
        {
            var lookup = new DiffBasedLookup("A A");
            var range1 = new Range[] { new Range(2, 3) };

            var result = lookup.TextDiffSearch("A A", range1);

            Assert.AreEqual(1, result.Count);

            Assert.AreEqual(result[0], new Range(2, 3));
        }

        [TestMethod]
        public void MultiTextFoundAtCorrectPosition3()
        {
            var lookup = new DiffBasedLookup("A A");
            var range1 = new Range[] { new Range(0, 1) };

            var result = lookup.TextDiffSearch(" A A", range1);

            Assert.AreEqual(1, result.Count);

            Assert.AreEqual(result[0], new Range(1, 2));
        }

        [TestMethod]
        public void MultiTextFoundAtCorrectPosition4()
        {
            var lookup = new DiffBasedLookup("A A");
            var range1 = new Range[] { new Range(2, 3) };

            var result = lookup.TextDiffSearch(" A A", range1);

            Assert.AreEqual(1, result.Count);

            Assert.AreEqual(result[0], new Range(3, 4));
        }

        [TestMethod]
        public void PartOfTextIsFound()
        {
            var lookup = new DiffBasedLookup("AB");
            var range1 = new Range[] { new Range(1, 2) };

            var result = lookup.TextDiffSearch(" AB", range1);

            Assert.AreEqual(1, result.Count);

            Assert.AreEqual(result[0], new Range(2, 3));
        }

        [TestMethod]
        public void PartOfTextIsFound2()
        {
            var lookup = new DiffBasedLookup("ABCD");
            var range1 = new Range[] { new Range(1, 3) };

            var result = lookup.TextDiffSearch(" ABCD", range1);

            Assert.AreEqual(1, result.Count);

            Assert.AreEqual(result[0], new Range(2, 4));
        }

        [TestMethod]
        public void TextFoundIfOriginalDeleted()
        {
            var lookup = new DiffBasedLookup("A A");
            var range1 = new Range[] { new Range(2, 3) };

            var result = lookup.TextDiffSearch("A", range1);

            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void TextFoundIfOriginalDeleted2()
        {
            var lookup = new DiffBasedLookup("A A");
            var range1 = new Range[] { new Range(0, 1) };

            var result = lookup.TextDiffSearch("  A", range1);

            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void TextFoundIfOriginalDeleted3()
        {
            var lookup = new DiffBasedLookup("A A A B");
            var range1 = new Range[] { new Range(5, 6) };

            var result = lookup.TextDiffSearch("  A A B", range1);

            Assert.AreEqual(1, result.Count);

            Assert.AreEqual(result[0], new Range(5, 6));
        }

        [TestMethod]
        public void ChangedTextIsFound()
        {
            var lookup = new DiffBasedLookup("This is an test");
            var ranges = new Range[] { new Range(0, "This".Length) };
            var result = lookup.TextDiffSearch("AAThis is an test", ranges);

            Assert.AreEqual(result[0], new Range(2, 6));
        }

        [TestMethod]
        public void PartContainingXMLSimple()
        {
            var lookup = new DiffBasedLookup("<x>a<b>c");
            var ranges = new Range[] { new Range(7, 8) };

            var result = lookup.TextDiffSearch("<x> a<b>c", ranges);

            Assert.AreEqual(1, result.Count);

            Assert.AreEqual(result[0], new Range(8, 9));
        }

        [TestMethod]
        public void PartContainingXMLandNl()
        {
            var lookup = new DiffBasedLookup("<x>\n<b>c");
            var ranges = new Range[] { new Range(4, 5), new Range(5, 6), new Range(6, 7), new Range(7, 8) };

            var result = lookup.TextDiffSearch("<x>a\n<b>c", ranges);

            Assert.AreEqual(4, result.Count);

            Assert.AreEqual(result[0], new Range(5, 6));
        }

        [TestMethod]
        public void PartContainingXMLandNl2()
        {
            var lookup = new DiffBasedLookup("<x>\r\n<b>c");
            var ranges = new Range[] { new Range(5, 6), new Range(6, 7), new Range(7, 8), new Range(8, 9) };

            var result = lookup.TextDiffSearch("<x>a\r\n<b>c", ranges);

            Assert.AreEqual(4, result.Count);

            Assert.AreEqual(result[0], new Range(6, 7));
        }

        [TestMethod]
        public void PartContainingXMLSelectingNl()
        {
            var lookup = new DiffBasedLookup("<x>\r\n<b>\r\n");
            var ranges = new Range[] { new Range(8, 9), new Range(9, 10) };

            var result = lookup.TextDiffSearch("<x>a\r\n<b>\r\n", ranges);

            Assert.AreEqual(2, result.Count);

            Assert.AreEqual(result[0], new Range(9, 10));
        }

        [TestMethod]
        public void PartContainingXML()
        {
            var lookup = new DiffBasedLookup("<x>\r\n    <some>Tesst</some>");
            var ranges = new Range[] { new Range(15, 20) };

            var result = lookup.TextDiffSearch("<x>a\r\n    <some>Tesst</some>\r\n    <structured>data sdfs</structured>\r\n</x>", ranges);

            Assert.AreEqual(1, result.Count);

            Assert.AreEqual(result[0], new Range(16, 21));
        }

        [TestMethod]
        public void PartContainingXML2()
        {
            var lookup = new DiffBasedLookup("<x>\r\n    <some>Tesst</some>\r\n    ");
            var ranges = new Range[] { new Range(15, 20), new Range(27, 29) };

            var result = lookup.TextDiffSearch("<x>a\r\n    <some>Tesst</some>\r\n    <structured>data sdfs</structured>\r\n</x>", ranges);

            Assert.AreEqual(2, result.Count);

            Assert.AreEqual(result[0], new Range(16, 21));
            Assert.AreEqual(result[1], new Range(28, 30));
        }

        [TestMethod]
        public void PartContainingXML2Simple()
        {
            var lookup = new DiffBasedLookup("<x>\r\n<s>Tesst</s>\r\n");
            var ranges = new Range[] { new Range(8, 13), new Range(17, 19) };

            var result = lookup.TextDiffSearch("<x>a\r\n<s>Tesst</s>\r\n", ranges);

            Assert.AreEqual(2, result.Count);

            Assert.AreEqual(result[0], new Range(9, 14));
            Assert.AreEqual(result[1], new Range(18, 20));
        }

        [TestMethod]
        public void PartContainingXML3()
        {
            var lookup = new DiffBasedLookup("<x>\r\n    <some>Tesst</some>\r\n    <structured>data sdfs</structured>\r\n</x>");
            var ranges = new Range[] { new Range(15, 20), new Range(27, 29), new Range(29, 30), new Range(30, 31), new Range(31, 32), new Range(32, 33), new Range(45, 49) };

            var result = lookup.TextDiffSearch("<x>a\r\n    <some>Tesst</some>\r\n    <structured>data sdfs</structured>\r\n</x>", ranges);

            Assert.AreEqual(7, result.Count);

            Assert.AreEqual(result[0], new Range(16, 21));
        }

        [TestMethod]
        public void PartContainingXML4()
        {
            var lookup = new DiffBasedLookup("<x>\r\n    <some>Tesst</some>\r\n    <structured>data sdfs</structured>\r\n</x>");
            var ranges = new Range[] { new Range(15, 20), new Range(30, 31), new Range(31, 32), new Range(32, 33), new Range(45, 49) };

            var result = lookup.TextDiffSearch("<x>a\r\n    <some>Tesst</some>\r\n    <structured>data sdfs</structured>\r\n</x>", ranges);

            Assert.AreEqual(5, result.Count);

            Assert.AreEqual(result[0], new Range(16, 21));
        }

        [TestMethod]
        public void PartContainingXML5()
        {
            var lookup = new DiffBasedLookup("<x>\r\n    <some>Tesst</some>\r\n    <structured>data sdfs</structured>\r\n</x>");
            var ranges = new Range[] { new Range(15, 20), new Range(45, 49) };

            var result = lookup.TextDiffSearch("<x>a\r\n    <some>Tesst</some>\r\n    <structured>data sdfs</structured>\r\n</x>", ranges);

            Assert.AreEqual(2, result.Count);

            Assert.AreEqual(result[0], new Range(16, 21));
        }
    }
}
