/* Copyright (c) 2016 Acrolinx GmbH */

using Acrolinx.Sdk.Sidebar.Documents;
using Acrolinx.Sdk.Sidebar.Exceptions;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Acrolinx.Sdk.Sidebar.Util.Changetracking
{
    /// <summary>
    /// In most host applications the <see cref="DocumentModel"/> can not be used to track the changes the user made to the source content. The lookup is used to adjust the offset changes makde by the user.
    /// </summary>
    public class Lookup
    {
        public enum LookupStrategy
        {
            SEARCH, NONE
        }

        public Lookup(string originalText) : this(originalText,LookupStrategy.SEARCH)
        {
            Contract.Requires(originalText != null);

        }

        public Lookup(string originalText, LookupStrategy strategy)
        {
            Contract.Requires(originalText != null);

            this.OriginalText = originalText;
            this.Strategy = strategy;
        }

        public string OriginalText { get; private set; }

        public IReadOnlyList<IRange> Search(string currentText, params IRange[] ranges)
        {
            Contract.Requires(currentText != null);
            Contract.Requires(ranges != null);
            Contract.Requires(ranges.All(range => range != null));

            return this.Search(currentText, new List<IRange>(ranges));
        }

        public IReadOnlyList<IRange> Search(string currentText, IReadOnlyList<IRange> ranges)
        {
            Contract.Requires(currentText != null);
            Contract.Requires(ranges != null);
            Contract.Requires(ranges.All(range => range != null));

            if (Strategy == LookupStrategy.NONE || ranges.Count == 0 || currentText.Equals(OriginalText))
            {
                return ranges;
            }

            var regexSearchStr = new StringBuilder();

            var offset = 0;
            for(var i = 0;i < ranges.Count ; i++)
            {
                var range = ranges[i];
                var surface = OriginalText.Substring(range.Start, range.Length);

                if (offset != range.Start)
                {
                    if (range.Start - offset > 1)
                    {
                        regexSearchStr.Append(".*?");
                    }
                    regexSearchStr.Append("(?:");
                    regexSearchStr.Append(Regex.Escape(OriginalText.Substring(range.Start-1,1)));
                    if(offset == 0) {
                        regexSearchStr.Append("|\\A");
                    }
                    regexSearchStr.Append(")");
                }
                offset = range.End;

                regexSearchStr.Append("(" + Regex.Escape(surface) + ")");
            }

            Regex regex = new Regex(regexSearchStr.ToString(), RegexOptions.Singleline);

            var searchTextRange = OriginalText.Substring(0, Math.Min(OriginalText.Length, ranges[ranges.Count -1].End+1));
            var matchesBefore = regex.Matches(searchTextRange).Count;

            //Contract.Assert(matchesBefore >= 1);

            var currentMatches = regex.Matches(currentText);

            List<IRange> result = new List<IRange>();
            {
                var i = 0;
                foreach(System.Text.RegularExpressions.Match match in currentMatches){
                    i++;
                    if(i == Math.Min(matchesBefore, currentMatches.Count))
                    {
                        Contract.Assert(match.Groups.Count -1 == ranges.Count);
                        var r = -1;
                        foreach (Group group in match.Groups)
                        {
                            r++;
                            if (r == 0)
                            {
                                continue;
                            }
                            var range = ranges[r -1];
                            Contract.Assert(OriginalText.Substring(range.Start,range.Length).Equals(group.Value));

                            result.Add(new Range(group.Index, group.Index + group.Length));
                        }
                    }
                }
            }

            return result;
        }

        public LookupStrategy Strategy { get; set; }
    }
}
