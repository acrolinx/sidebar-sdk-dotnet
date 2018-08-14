using Acrolinx.Sdk.Sidebar.Documents;
using Acrolinx.Sdk.Sidebar.Util.Logging;
using DiffMatchPatch;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace Acrolinx.Sdk.Sidebar.Util.Changetracking
{
    public class DiffBasedLookup
    {
        private string originalText;
        public DiffBasedLookup(string originalText)
        {
            Contract.Requires(originalText != null);
            this.originalText = originalText;
        }

        public IReadOnlyList<IRange> TextDiffSearch(string currentText, params IRange[] ranges)
        {
            Contract.Requires(currentText != null);
            Contract.Requires(ranges != null);
            Contract.Requires(ranges.All(range => range != null));

            return this.TextDiffSearch(currentText, new List<IRange>(ranges));
        }

        public IReadOnlyList<IRange> TextDiffSearch(string currentText, IReadOnlyList<IRange> ranges)
        {
            Contract.Requires(currentText != null);
            Contract.Requires(ranges != null);
            Contract.Requires(ranges.All(range => range != null));

            int offsetCountOld = 0;
            int currentDiffOffset = 0;
            List<Tuple<double, double>> offsetMappingSet = new List<Tuple<double, double>>();
            DiffMatchPatch.DiffMatchPatch dmp = DiffMatchPatchModule.Default;
            List<Diff> diff = dmp.DiffMain(originalText, currentText);
            for (int i = 0; i < diff.Count; i++)
            {
                switch (diff[i].Operation.ToString())
                {
                    case "Equal":
                        offsetCountOld += diff[i].Text.Length;
                        break;
                    case "Delete":
                        offsetCountOld += diff[i].Text.Length;
                        currentDiffOffset -= diff[i].Text.Length;
                        break;
                    case "Insert":
                        currentDiffOffset += diff[i].Text.Length;
                        break;
                    default:
                        break;
                }
                Tuple<double, double> offsetMap = new Tuple<double, double>(offsetCountOld, currentDiffOffset);
                offsetMappingSet.Add(offsetMap);
            }
            offsetMappingSet.Sort(Comparer<Tuple<double, double>>.Default);

            int startOffest = 0;
            int endOffset = 0;
            List<IRange> result = new List<IRange>();

            for (var i = 0; i < ranges.Count; i++)
            {
                var range = ranges[i];
                startOffest = range.Start;
                endOffset = range.End;

                int alignedBegin = FindNewIndex(offsetMappingSet, startOffest);
                int alignedEnd = FindNewIndex(offsetMappingSet, endOffset);
                string originalMatchSurface = originalText.Substring(startOffest, range.Length);
                string currentMatchSurface = string.Empty;
                try
                {
                    currentMatchSurface = currentText.Substring(alignedBegin, alignedEnd - alignedBegin);
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    Logger.AcroLog.Info("Original Surface ranges: "+ new Range(startOffest, endOffset)+" does not match Current Surface ranges: " + new Range(alignedBegin, alignedEnd));
                }
                if (originalMatchSurface.Equals(currentMatchSurface))
                {
                    result.Add(new Range(alignedBegin, alignedEnd));
                }
            }
            return result;
        }

        private int FindNewIndex(List<Tuple<double, double>> offsetMappingSet, int originalIndex)
        {
            return originalIndex + FindDisplacement(offsetMappingSet, originalIndex);
        }

        private int FindDisplacement(List<Tuple<double, double>> offsetMappingSet, int originalIndex)
        {
            int displacement = 0;
            Tuple<double, double> offsetMap = new Tuple<double, double>(originalIndex + 0.1, originalIndex);
            offsetMappingSet.Add(offsetMap);
            offsetMappingSet.Sort(Comparer<Tuple<double, double>>.Default);
            var findIndex = offsetMappingSet.IndexOf(offsetMap);
            if (findIndex > 0)
            {
                displacement = Convert.ToInt32(offsetMappingSet[findIndex - 1].Item2);
            }
            offsetMappingSet.RemoveAt(findIndex);
            return displacement;
        }
    }
}
