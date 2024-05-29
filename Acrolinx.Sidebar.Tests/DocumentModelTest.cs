/* Copyright (c) 2016 Acrolinx GmbH */

using Acrolinx.Sdk.Sidebar;
using Acrolinx.Sdk.Sidebar.Documents;
using Acrolinx.Sdk.Sidebar.Util.Changetracking;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Range = Acrolinx.Sdk.Sidebar.Documents.Range;

namespace Acrolinx.Sdk.Sidebar.Tests
{
    [TestClass]
    public class DocumentModelTest
    {

        [TestMethod]
        public void EmptyIsEmptyAndNotNull()
        {
            var map = new DocumentMap<string>();
            Assert.AreEqual(0, map.GetRelativeRange(new Range(0, 7)).Count);
        }

        [TestMethod]
        public void InvalidDoesNotCrash()
        {
            var map = new DocumentMap<string>();
            Assert.AreEqual(0, map.GetRelativeRange(new Range(-5, 0)).Count);
        }

        [TestMethod]
        public void GetPart()
        {
            var map = new DocumentMap<string>();
            map.Add(5, "test");

            var relativeRanges = map.GetRelativeRange(new Range(1, 7));
            Assert.AreEqual(1, relativeRanges.Count);
            Assert.AreEqual("test", relativeRanges[0].Source);
            Assert.AreEqual(new Range(1,5), relativeRanges[0].Range);
        }

        [TestMethod]
        public void GetOverlap()
        {
            var map = new DocumentMap<string>();
            map.Add(5, "test");
            map.Add(2, "test2");

            var relativeRanges = map.GetRelativeRange(new Range(1, 7));
            Assert.AreEqual(2, relativeRanges.Count);
            Assert.AreEqual("test", relativeRanges[0].Source);
            
            Assert.AreEqual(new Range(1, 5), relativeRanges[0].Range);
            Assert.AreEqual("test2", relativeRanges[1].Source);
            Assert.AreEqual(new Range(0, 2), relativeRanges[1].Range);
        }

        [TestMethod]
        public void GetEmpty()
        {
            var map = new DocumentMap<string>();
            map.Add(5, "test");
            map.Add(0, "test3");
            map.Add(2, "test2");

            var relativeRanges = map.GetRelativeRange(new Range(1, 7));
            Assert.AreEqual(3, relativeRanges.Count);

            Assert.AreEqual("test3", relativeRanges[1].Source);
            Assert.AreEqual(new Range(0,0), relativeRanges[1].Range);
        }
    }
}
