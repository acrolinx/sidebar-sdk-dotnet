/* Copyright (c) 2016 Acrolinx GmbH */

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acrolinx.Sdk.Sidebar.Documents
{
    public class Match
    {
        public Match(string content, IRange range)
        {
            Contract.Requires(content != null);
            Contract.Requires(range != null);
            Content = content;
            Range = range;
        }
        public string Content { get; private set; }
        public IRange Range { get; private set; }
    }
}
