/* Copyright (c) 2016 Acrolinx GmbH */

using Acrolinx.Sdk.Sidebar.Documents;
using Acrolinx.Sdk.Sidebar.Exceptions;
using Acrolinx.Sdk.Sidebar.Util.Changetracking;
using Acrolinx.Sdk.Sidebar.Util.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Range = Acrolinx.Sdk.Sidebar.Documents.Range;

namespace Acrolinx.Sdk.Sidebar.Util.Adapter
{
    /// <summary>
    /// UniversalAdapter provides functionality to adjust offsets based on actions performed by the sidebar and the user. It can be used for XML as well as plain text.
    /// </summary>
    public abstract class UniversalAdapter : IAdapter
    {
        public UniversalAdapter(string tagName, bool encodeContent)
        {
            Contract.Requires(tagName != null);

            TagName = tagName;
            this.EncodeContent = encodeContent;
        }

        public virtual XElement CreateXElement(){
            return new XElement(TagName);
        }

        private string TagName
        {
            get;
            set;
        }
        
        protected abstract string ExtractRaw();

        public virtual string Extract(Format format)
        {
            if (EncodeContent && format != Format.Text)
            {
                // Wrap the raw content into some format-specific element.
                var element = CreateXElement();
                element.Value = ExtractRaw();
                return element.ToString();
            }

            return ExtractRaw();
        }

        protected bool EncodeContent
        {
            get;
            private set;
        }

        protected virtual string XmlEncode(string text)
        {
            Contract.Requires(text != null);

            var xe = XElement.Parse("<x></x>");
            xe.Value = text;
            var xmlEncoded = String.Join("", xe.Nodes().Select(x => x.ToString()).ToArray());
            return xmlEncoded;
        }

        protected virtual string XmlDecode(string xml)
        {
            Contract.Requires(xml != null);

            var xe = XElement.Parse("<x>" + xml + "</x>", LoadOptions.PreserveWhitespace);
            return xe.Value;
        }

        private int DecodedLength(string text)
        {
            // Workaround required as the parser does not provide offset information. 
            // Calculate length considering the offset shifts caused by the entities.
            System.Text.RegularExpressions.MatchCollection entities = System.Text.RegularExpressions.Regex.Matches(text, "&\\w+?;");
            var length = 0;
            var lastMatchEnd = 0;
            foreach (System.Text.RegularExpressions.Match match in entities)
            {
                length += match.Index - lastMatchEnd;
                lastMatchEnd = match.Index + match.Length;
                var resolvedEntity = XmlDecode(match.Value);
                length += resolvedEntity.Length;
            }
            length += text.Length - lastMatchEnd;
            return length;
        }

        public void SelectRanges(IReadOnlyList<Match> matches, DocumentModel model, Format format)
        {
            var modifiedRanges = new List<IRange>(matches.Select<Match, IRange>(x => model.OriginalToModifiedRange(x.Range)));

            var currentExtraction = Extract(format);
            
            var lookup = new Lookup(model.Content);
            var searchResult = lookup.Search(currentExtraction, modifiedRanges);
           
            if(searchResult.Count == 0)
            {
                throw new MatchesNotFoundException("Lookup returned an empty list. Maybe source document was change by user?", matches, null);
            }

            var rangeWithTag = new Range(searchResult[0].Start, searchResult[searchResult.Count - 1].End);

            IRange range = ToRawRange(format, currentExtraction, rangeWithTag, false);
            SelectRawRange(range, format);            
        }

        private IRange ToRawRange(Format format, string currentExtraction, IRange rangeWithTag, bool silent)
        {
            IRange range;
            if (EncodeContent && format != Format.Text)
            {
                var xmlContentStart = currentExtraction.IndexOf(">") + 1;
                var xmlContentEnd = currentExtraction.LastIndexOf("<");

                System.Diagnostics.Trace.Assert(xmlContentStart > 0);
                System.Diagnostics.Trace.Assert(xmlContentEnd > 0);

                if (!silent && !(xmlContentStart <= rangeWithTag.Start && rangeWithTag.End <= xmlContentEnd))
                {
                    throw new RangesNotFoundException(rangeWithTag + " in content: '" + currentExtraction.Substring(rangeWithTag.Start,rangeWithTag.Length) + "' is trying to mark inside tags.", new IRange[]{rangeWithTag}, null);
                }

                var xmlContent = currentExtraction.Substring(xmlContentStart, xmlContentEnd - xmlContentStart);            

                var rangeInContentStart = Math.Min(Math.Max(0, rangeWithTag.Start - xmlContentStart), xmlContent.Length);
                var rangeInContentLength = Math.Min(rangeWithTag.Length, xmlContent.Length - rangeInContentStart);

                var decodedStart = DecodedLength(xmlContent.Substring(0, rangeInContentStart));
                var decodedLength = DecodedLength(xmlContent.Substring(rangeInContentStart, rangeInContentLength));
                range = new Range(decodedStart, decodedStart + decodedLength);
            }
            else
            {
                range = new Range(rangeWithTag.Start, rangeWithTag.End);
            }
            return range;
        }

              
        public void ReplaceRanges(IReadOnlyList<MatchWithReplacement> matches, DocumentModel model, Format format)
        {
            var modifiedRanges = new List<IRange>(matches.Select<Match, IRange>(x => model.OriginalToModifiedRange(x.Range)));

            var currentExtraction = Extract(format);

            var lookup = new Lookup(model.Content);
            var searchResult = lookup.Search(currentExtraction, modifiedRanges);

            var lengthDiff = 0;

            if (searchResult.Count == 0)
            {
                throw new MatchesNotFoundException("Lookup returned an empty list. Maybe source document was change by user?", matches, null);
            }

            for (var i = searchResult.Count - 1; i >= 0; i--)
            {
                var match = searchResult[i];
                var replacement = matches[i].Replacement;

                IRange rawRange= ToRawRange(format, currentExtraction, match,false);

                var rawOriginalTextLength = (!EncodeContent && format != Format.Text) ? DecodedLength(currentExtraction.Substring(match.Start, match.Length)) : match.Length;
                var rawReplacement =  (!EncodeContent && format != Format.Text) ? XmlEncode(replacement) : replacement;

                if (rawRange.Length == 0)
                {
                    throw new MatchesNotFoundException("", new Match[]{matches[i]}, null);
                }

                lengthDiff += rawReplacement.Length - rawOriginalTextLength;

                ReplaceRawRange(rawRange, rawReplacement, format);
                model.Update(matches[i].Range, ((format != Format.Text) ? XmlEncode(replacement) : replacement));
            }

            var rangeWithTag = new Range(searchResult[0].Start, searchResult[searchResult.Count - 1].End);
            IRange range = ToRawRange(format, currentExtraction, rangeWithTag, false);
            SelectRawRange(new Range(range.Start, range.End + lengthDiff), format);
        }

        private string EncodeIfRequried(string text, Format format)
        {
            Contract.Requires(text != null);

            if (EncodeContent && format != Format.Text)
            {
                return XmlEncode(text);
            }

            return text;
        }

        private string DecodeIfRequried(string text, Format format)
        {
            Contract.Requires(text != null);

            if (EncodeContent && format != Format.Text)
            {
                return XmlDecode(text);
            }

            return text;
        }

        protected virtual void SelectRawRange(IRange range, Format format)
        {
            Contract.Requires(range != null);

            Logger.AcroLog.Info("SelectRange must be implemented! Range: " + range);
        }

        protected virtual void ReplaceRawRange(IRange range, string replacement, Format format)
        {
            Contract.Requires(range != null);
            Contract.Requires(replacement != null);

            Logger.AcroLog.Info("ReplaceRange must be implemented! Range: " + range + " Replacement:" + replacement);
        }
    }
}
