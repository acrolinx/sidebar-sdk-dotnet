/* Copyright (c) 2016 Acrolinx GmbH */

using Acrolinx.Sdk.Sidebar.Documents;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Range = Acrolinx.Sdk.Sidebar.Documents.Range;

namespace Acrolinx.Sdk.Sidebar.Util.Changetracking
{
    /// <summary>
    /// DocumentMap creates a mapping between the source or content and the part in the submitted document. 
    /// It helps mapping the absolute positions the sidebar returns to relative positions inside the source of the content.
    /// </summary>
    /// <typeparam name="SourceType">The source could be something like a control of an application, a path expression like XPath or an id.</typeparam>
    public class DocumentMap<SourceType>
    {
        private readonly List<int> lengths = new List<int>();
        private readonly List<SourceType> parts = new List<SourceType>();

        public void Add(int length, SourceType documentPart)
        {
            Contract.Requires(length >= 0);

            parts.Add(documentPart);
            lengths.Add(length);
        }

        public IReadOnlyList<RelativeRange<SourceType>> GetRelativeRange(IRange lookupRange)
        {
            Contract.Requires(lookupRange != null);

            var result = new List<RelativeRange<SourceType>>();

            var offset = 0;
            for (int i = 0; i < parts.Count; i++)
            {
                var part = parts[i];
                if (part != null)
                {
                    var range = new Range(offset, offset + lengths[i]);
                    if (Range.Intersects(range, lookupRange))
                    {
                        var relativeStart = Math.Max(0, lookupRange.Start - range.Start);
                        var relativeEnd = (Math.Min(range.End, lookupRange.End) - range.Start);
                        var relativeRange = new Range(relativeStart, relativeEnd);
                        result.Add(new RelativeRange<SourceType>(relativeRange, part));
                    }
                }

                offset += lengths[i];
            }

            return result;
        }
    }
}
