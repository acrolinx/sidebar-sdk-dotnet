/* Copyright (c) 2016 Acrolinx GmbH */

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Acrolinx.Sdk.Sidebar.Util.Changetracking;
using Acrolinx.Sdk.Sidebar.Documents;

namespace Acrolinx.Sdk.Sidebar.Tests
{
    [TestClass]
    public class LookupTest
    {
        [TestMethod]
        public void SimpleSearch()
        {
            var lookup = new Lookup("This is an test");
            var range = new Range(0, "This".Length);
            var result = lookup.Search("This is an test", range);

            Assert.AreEqual(result[0], range);
        }
        
        [TestMethod]
        public void TextHasMoved()
        {
            var lookup = new Lookup("This is an test");
            var range = new Range(0, "This".Length);
            var result = lookup.Search("AA This is an test", range);

            Assert.AreEqual(result[0], new Range("AA ".Length, "AA This".Length));
        }

        [TestMethod]
        public void MultiRangeTextHasMoved()
        {
            var lookup = new Lookup("This is an test");
            var range1 = new Range(0, "This".Length);
            var range2 = new Range("This ".Length, "This is".Length);
            var result = lookup.Search("AA This is an test", range1, range2);

            Assert.AreEqual(2, result.Count);

            Assert.AreEqual(result[0], new Range("AA ".Length, "AA This".Length));
            Assert.AreEqual(result[1], new Range("AA This ".Length, "AA This is".Length));
        }

        [TestMethod]
        public void MultiTextFoundAtCorrectPosition()
        {
            var lookup = new Lookup("A A");
            var range1 = new Range(0, 1);

            var result = lookup.Search("A A", range1);

            Assert.AreEqual(1, result.Count);

            Assert.AreEqual(result[0], new Range(0, 1));
        }

        [TestMethod]
        public void MultiTextFoundAtCorrectPosition2()
        {
            var lookup = new Lookup("A A");
            var range1 = new Range(2, 3);

            var result = lookup.Search("A A", range1);

            Assert.AreEqual(1, result.Count);

            Assert.AreEqual(result[0], new Range(2, 3));
        }

        [TestMethod]
        public void MultiTextFoundAtCorrectPosition3()
        {
            var lookup = new Lookup("A A");
            var range1 = new Range(0, 1);

            var result = lookup.Search(" A A", range1);

            Assert.AreEqual(1, result.Count);

            Assert.AreEqual(result[0], new Range(1, 2));
        }

        [TestMethod]
        public void MultiTextFoundAtCorrectPosition4()
        {
            var lookup = new Lookup("A A");
            var range1 = new Range(2, 3);

            var result = lookup.Search(" A A", range1);

            Assert.AreEqual(1, result.Count);

            Assert.AreEqual(result[0], new Range(3, 4));
        }

        [TestMethod]
        public void PartOfTextIsFound()
        {
            var lookup = new Lookup("AB");
            var range1 = new Range(1, 2);

            var result = lookup.Search(" AB", range1);

            Assert.AreEqual(1, result.Count);

            Assert.AreEqual(result[0], new Range(2, 3));
        }

        [TestMethod]
        public void PartOfTextIsFound2()
        {
            var lookup = new Lookup("ABCD");
            var range1 = new Range(1, 3);

            var result = lookup.Search(" ABCD", range1);

            Assert.AreEqual(1, result.Count);

            Assert.AreEqual(result[0], new Range(2, 4));
        }
        
        [TestMethod]
        public void TextFoundIfOriginalDeleted()
        {
            var lookup = new Lookup("A A");
            var range1 = new Range(2, 3);

            var result = lookup.Search("A", range1);

            Assert.AreEqual(1, result.Count);

            Assert.AreEqual(result[0], new Range(0, 1));
        }

        [TestMethod]
        public void TextFoundIfOriginalDeleted2()
        {
            var lookup = new Lookup("A A");
            var range1 = new Range(0, 1);

            var result = lookup.Search("  A", range1);

            Assert.AreEqual(1, result.Count);

            Assert.AreEqual(result[0], new Range(2, 3));
        }

        [TestMethod]
        public void TextFoundIfOriginalDeleted3()
        {
            var lookup = new Lookup("A A A B");
            var range1 = new Range(5, 6);

            var result = lookup.Search("  A A B", range1);

            Assert.AreEqual(1, result.Count);

            Assert.AreEqual(result[0], new Range(5, 6));
        }

        [TestMethod]
        public void ChangedTextIsFound()
        {
            var lookup = new Lookup("This is an test");
            var range = new Range(0, "This".Length);
            var result = lookup.Search("AAThis is an test", range);

            Assert.AreEqual(result[0], new Range(2, 6));
        }

        [TestMethod]
        public void PartContainingXMLSimple()
        {
            var lookup = new Lookup("<x>a<b>c");
            var ranges = new Range[] { new Range(7, 8)};

            var result = lookup.Search("<x> a<b>c", ranges);

            Assert.AreEqual(1, result.Count);

            Assert.AreEqual(result[0], new Range(8,9));
        }

        [TestMethod]
        public void PartContainingXMLandNl()
        {
            var lookup = new Lookup("<x>\n<b>c");
            var ranges = new Range[] { new Range(4, 5), new Range(5, 6), new Range(6, 7), new Range(7, 8) };

            var result = lookup.Search("<x>a\n<b>c", ranges);

            Assert.AreEqual(4, result.Count);

            Assert.AreEqual(result[0], new Range(5, 6));
        }

        [TestMethod]
        public void PartContainingXMLandNl2()
        {
            var lookup = new Lookup("<x>\r\n<b>c");
            var ranges = new Range[] { new Range(5, 6), new Range(6, 7), new Range(7, 8), new Range(8, 9) };

            var result = lookup.Search("<x>a\r\n<b>c", ranges);

            Assert.AreEqual(4, result.Count);

            Assert.AreEqual(result[0], new Range(6, 7));
        }

        [TestMethod]
        public void PartContainingXMLSelectingNl()
        {
            var lookup = new Lookup("<x>\r\n<b>\r\n");
            var ranges = new Range[] { new Range(8, 9), new Range(9, 10)};

            var result = lookup.Search("<x>a\r\n<b>\r\n", ranges);

            Assert.AreEqual(2, result.Count);

            Assert.AreEqual(result[0], new Range(9, 10));
        }

        [TestMethod]
        public void PartContainingXML()
        {
            var lookup = new Lookup("<x>\r\n    <some>Tesst</some>");
            var ranges = new Range[] { new Range(15, 20)};

            var result = lookup.Search("<x>a\r\n    <some>Tesst</some>\r\n    <structured>data sdfs</structured>\r\n</x>", ranges);

            Assert.AreEqual(1, result.Count);

            Assert.AreEqual(result[0], new Range(16, 21));
        }

        [TestMethod]
        public void PartContainingXML2()
        {
            var lookup = new Lookup("<x>\r\n    <some>Tesst</some>\r\n    ");
            var ranges = new Range[] { new Range(15, 20), new Range(27, 29) };

            var result = lookup.Search("<x>a\r\n    <some>Tesst</some>\r\n    <structured>data sdfs</structured>\r\n</x>", ranges);

            Assert.AreEqual(2, result.Count);

            Assert.AreEqual(result[0], new Range(16, 21));
            Assert.AreEqual(result[1], new Range(28, 30));
        }

        [TestMethod]
        public void PartContainingXML2Simple()
        {
            var lookup = new Lookup("<x>\r\n<s>Tesst</s>\r\n");
            var ranges = new Range[] { new Range(8, 13), new Range(17, 19) };

            var result = lookup.Search("<x>a\r\n<s>Tesst</s>\r\n", ranges);

            Assert.AreEqual(2, result.Count);

            Assert.AreEqual(result[0], new Range(9, 14));
            Assert.AreEqual(result[1], new Range(18, 20));
        }

        [TestMethod]
        public void PartContainingXML3()
        {
            var lookup = new Lookup("<x>\r\n    <some>Tesst</some>\r\n    <structured>data sdfs</structured>\r\n</x>");
            var ranges = new Range[]{ new Range(15,20), new Range(27, 29),new Range(29, 30),new Range(30, 31), new Range(31, 32), new Range(32, 33), new Range(45, 49)};

            var result = lookup.Search("<x>a\r\n    <some>Tesst</some>\r\n    <structured>data sdfs</structured>\r\n</x>", ranges);

            Assert.AreEqual(7, result.Count);

            Assert.AreEqual(result[0], new Range(16, 21));
        }

        [TestMethod]
        public void PartContainingXML4()
        {
            var lookup = new Lookup("<x>\r\n    <some>Tesst</some>\r\n    <structured>data sdfs</structured>\r\n</x>");
            var ranges = new Range[] { new Range(15, 20), new Range(30, 31), new Range(31, 32), new Range(32, 33), new Range(45, 49) };

            var result = lookup.Search("<x>a\r\n    <some>Tesst</some>\r\n    <structured>data sdfs</structured>\r\n</x>", ranges);

            Assert.AreEqual(5, result.Count);

            Assert.AreEqual(result[0], new Range(16, 21));
        }

        [TestMethod]
        public void PartContainingXML5()
        {
            var lookup = new Lookup("<x>\r\n    <some>Tesst</some>\r\n    <structured>data sdfs</structured>\r\n</x>");
            var ranges = new Range[] { new Range(15, 20), new Range(45, 49) };

            var result = lookup.Search("<x>a\r\n    <some>Tesst</some>\r\n    <structured>data sdfs</structured>\r\n</x>", ranges);

            Assert.AreEqual(2, result.Count);

            Assert.AreEqual(result[0], new Range(16, 21));
        }
    }
}
