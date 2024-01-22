/* Copyright (c) 2018 Acrolinx GmbH */

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
        private readonly short diffDualThreshold = 32;
        private readonly float matchThreshold = 0.5f;
        private readonly int matchDistance = 1000;
        private readonly float patchDeleteThreshold = 0.5f;
        private readonly short patchMargin = 4;
        private readonly int matchMaxBits = 32;
        private readonly int diffEditCost = 4;

        private string originalText;
        private readonly DiffOptions diffOptions;
        public DiffBasedLookup(string originalText)
        {
            Contract.Requires(originalText != null);
            this.originalText = originalText;
            diffOptions = new DiffOptions();
        }

        public DiffBasedLookup(string originalText, DiffOptions diffOptions)
        {
            Contract.Requires(originalText != null);
            this.originalText = originalText;
            this.diffOptions = diffOptions;
        }

        public IReadOnlyList<IRange> TextDiffSearch(string currentText, IReadOnlyList<IRange> ranges)
        {
            Contract.Requires(currentText != null);
            Contract.Requires(ranges != null);
            Contract.Requires(ranges.All(range => range != null));

            int offsetCountOld = 0;
            int currentDiffOffset = 0;

            Tuple<string, List<Tuple<double, double>>> cleaningResult =
                this.diffOptions.diffInputFormat == DiffInputFormat.MARKUP ?
                MarkupReducer.reduce(originalText) :
                new Tuple<string, List<Tuple<double, double>>>(originalText,
                    new List<Tuple<double, double>>());

            string cleanedCheckedDocument = cleaningResult.Item1;
            List<Tuple<double, double>> cleaningOffsetMappingArray = cleaningResult.Item2;

            List<Tuple<double, double>> offsetMappingList = new List<Tuple<double, double>>();

            DiffMatchPatch.DiffMatchPatch dmp =
                new DiffMatchPatch.DiffMatchPatch(
                    diffOptions.diffTimeoutInSeconds,
                    diffDualThreshold,
                    diffEditCost,
                    matchThreshold,
                    matchDistance,
                    matchMaxBits,
                    patchDeleteThreshold,
                    patchMargin
                );

            List<Diff> diff = dmp.DiffMain(cleanedCheckedDocument, currentText);
            diff.ForEach(d =>
            {
                switch (d.Operation.ToString())
                {
                    case "Equal":
                        offsetCountOld += d.Text.Length;
                        break;
                    case "Delete":
                        offsetCountOld += d.Text.Length;
                        currentDiffOffset -= d.Text.Length;
                        break;
                    case "Insert":
                        currentDiffOffset += d.Text.Length;
                        break;
                    default:
                        break;
                }
                Tuple<double, double> offsetMap = new Tuple<double, double>(offsetCountOld, currentDiffOffset);
                offsetMappingList.Add(offsetMap);
            });

            offsetMappingList.Sort(Comparer<Tuple<double, double>>.Default);

            int startOffest = 0;
            int endOffset = 0;
            List<IRange> result = new List<IRange>();

            foreach (var range in ranges)
            {
                startOffest = range.Start;
                endOffset = range.End;

                int beginAfterCleaning = FindNewIndex(cleaningOffsetMappingArray, startOffest);
                int endAfterCleaning = FindNewIndex(cleaningOffsetMappingArray, endOffset);
                int alignedBegin = FindNewIndex(offsetMappingList, beginAfterCleaning);
                int lastCharacterPos = endAfterCleaning - 1;
                int alignedEnd = FindNewIndex(offsetMappingList, lastCharacterPos) + 1;
                string originalMatchSurface = originalText.Substring(startOffest, range.Length);
                string currentMatchSurface = string.Empty;
                try
                {
                    currentMatchSurface = currentText.Substring(alignedBegin, alignedEnd - alignedBegin);
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    Logger.AcroLog.Info("Modified content has no matches for the given range. Original Surface ranges: " + new Range(startOffest, endOffset) + " Current Surface ranges: " + new Range(alignedBegin, alignedEnd));
                    Logger.AcroLog.Debug(ex.Message);
                }
                if (originalMatchSurface.Equals(currentMatchSurface))
                {
                    result.Add(new Range(alignedBegin, alignedEnd));
                }

            }
            return result;
        }

        internal int FindNewIndex(List<Tuple<double, double>> offsetMappingList, int originalIndex)
        {
            return originalIndex + FindDisplacement(offsetMappingList, originalIndex);
        }

        private int FindDisplacement(List<Tuple<double, double>> offsetMappingList, int originalIndex)
        {
            int displacement = 0;
            Tuple<double, double> offsetMap = new Tuple<double, double>(originalIndex + 0.1, originalIndex);
            offsetMappingList.Add(offsetMap);
            offsetMappingList.Sort(Comparer<Tuple<double, double>>.Default);
            var findIndex = offsetMappingList.IndexOf(offsetMap);
            if (findIndex > 0)
            {
                displacement = Convert.ToInt32(offsetMappingList[findIndex - 1].Item2);
            }
            offsetMappingList.RemoveAt(findIndex);
            return displacement;
        }
    }
}
