/* Copyright (c) 2016 Acrolinx GmbH */

using Acrolinx.Sdk.Sidebar.Documents;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acrolinx.Sdk.Sidebar.Exceptions
{
    public class RangesNotFoundException : AcrolinxException
    {
        public RangesNotFoundException(string message, IReadOnlyList<IRange> ranges, Exception inner)
            : base(string.Join(Environment.NewLine, message, string.Join(Environment.NewLine, ranges.Select(range=> range + " not found."), inner)))
        {
            Contract.Requires(message != null);
            Contract.Requires(ranges != null);
        }
        public IRange Range { get; private set; }
    }
}
