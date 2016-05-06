/* Copyright (c) 2016 Acrolinx GmbH */

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acrolinx.Sdk.Sidebar.Documents
{
    public class MatchWithReplacement : Match
    {
        public MatchWithReplacement (Match match, string replacement) : base(match.Content, match.Range)
        {
            Contract.Requires(match != null);
            Contract.Requires(replacement != null);
            Replacement = replacement;
        }
        public string Replacement { get; private set; }
    }
}
