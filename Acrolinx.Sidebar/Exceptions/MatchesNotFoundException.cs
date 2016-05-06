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
    public class MatchesNotFoundException : AcrolinxException
    {
        public MatchesNotFoundException(string message,  IReadOnlyList<Match> matches, Exception inner)
            : base(string.Join(Environment.NewLine, message, string.Join(Environment.NewLine, matches.Select(match => "Match: " + match.Content + " " + match.Range + " not found."))), inner)
        {
            Contract.Requires(matches != null);

            this.Matches = matches;
        }

        public IReadOnlyList<Match> Matches { get; set; }
    }
}
