/* Copyright (c) 2016 Acrolinx GmbH */

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace Acrolinx.Sdk.Sidebar.Documents
{
    public class Range : Acrolinx.Sdk.Sidebar.Documents.IRange
    {
        public Range(int start, int end)
        {
            Contract.Requires(start <= end);
            
            Start = start;
            End = end;
        }

        public int Start { get; private set; }
        public int End { get; private set; }

        public int Length
        {
            get
            {
                Contract.Ensures(Contract.Result<int>() >= 0);
                return End - Start;
            }
        }
        
        public override string ToString()
        {
            return "[" + Start + ", " + End + "]";
        }

        public override bool Equals(object obj)
        {
            Acrolinx.Sdk.Sidebar.Documents.IRange other = obj as Acrolinx.Sdk.Sidebar.Documents.IRange;

            if(other == null)
            {
                return false;
            }

            return other.Start == this.Start && other.End == this.End;
        }

        public override int GetHashCode()
        {
            return this.Start * this.End;
        }

        internal static bool Intersects(IRange range1, IRange range2)
        {
            Contract.Requires(range1 != null);
            Contract.Requires(range2 != null);

            return range2.End > range1.Start && range1.End > range2.Start;
        }

        internal static bool Includes(IRange range, IRange maybeIncludedRange)
        {
            Contract.Requires(range != null);
            Contract.Requires(maybeIncludedRange != null);

            return range.Start <= maybeIncludedRange.Start && maybeIncludedRange.End <= range.End;
        }
    }
}
