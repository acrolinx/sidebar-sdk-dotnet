/* Copyright (c) 2016 Acrolinx GmbH */

using Acrolinx.Sdk.Sidebar.Documents;
using Acrolinx.Sdk.Sidebar.Exceptions;
using Acrolinx.Sdk.Sidebar.Util.Changetracking;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Acrolinx.Sdk.Sidebar.Util.Adapter
{
    /// <summary>
    /// In many applications different sources should be combined into one check request. The MultiAdapter helps to map back and forth.
    /// </summary>
    public class MultiAdapter 
    {
        private StringBuilder extraction = new StringBuilder();
        private Format format;

        private DocumentMap<IAdapter> documentMap = new DocumentMap<IAdapter>();
        private Dictionary<IAdapter, DocumentModel> documentModels = new Dictionary<IAdapter, DocumentModel>();

        public MultiAdapter(IAdapter adapter, Format format) : this("", new IAdapter[] { adapter }, format)
        {

        }
        public MultiAdapter(string documentTag, IEnumerable<IAdapter> adapterList, Format format)
        {
            Contract.Requires(documentTag != null);
            Contract.Requires(adapterList != null);

            this.format = format;
            AddTagStart(documentTag);

            foreach (IAdapter adapter in adapterList)
            {
                AddContentUpdateDocumentMap(adapter, adapter.Extract(format));
            }
            AddTagEnd(documentTag);
        }
        
        private int lastDocumentMapOffset = 0;
        private void AddContentUpdateDocumentMap(IAdapter adapter, string content){
            var oldOffset = extraction.Length;
            documentMap.Add(extraction.Length - lastDocumentMapOffset, null);

            extraction.Append(content);
            documentModels.Add(adapter, new DocumentModel(content));

            documentMap.Add(extraction.Length - oldOffset, adapter);

            lastDocumentMapOffset = extraction.Length;
        }

        private void AddTagStart(string tagName)
        {
            if (format != Format.Text && !String.IsNullOrWhiteSpace(tagName))
            {
                extraction.Append("<");
                extraction.Append(tagName);
                extraction.Append(">");
            }
        }

        private void AddTagEnd(string tagName)
        {
            if (format != Format.Text && !String.IsNullOrWhiteSpace(tagName))
            {
                extraction.Append("</");
                extraction.Append(tagName);
                extraction.Append(">");
            }
            else
            {
                extraction.Append(Environment.NewLine);
                extraction.Append(Environment.NewLine);
            }
        }


        public IDocument Document
        {
            get
            {
                var doc = new Document();
                doc.Reference = DocumentReference;
                doc.Content = extraction.ToString();
                doc.Format = format;
                return doc;
            }
        }
        
        public string DocumentReference { get; set; }

        public void SelectRanges(IReadOnlyList<Match> matches)
        {
            Contract.Requires(matches != null);

            var relativeMatches = MatchesToRelative(matches, (oldMatch, newRange) => new Match(oldMatch.Content, newRange));

            for (var i = relativeMatches.Count - 1; i >= 0; i--)
            {
                var forCurrentAdapter = relativeMatches[i];
                var adapter = forCurrentAdapter.Item1;

                adapter.SelectRanges(forCurrentAdapter.Item2, documentModels[adapter], format);
            }
        }

        private delegate M CreateMatchDelegate<M>(M match, IRange newRange);

        private IReadOnlyList<Tuple<IAdapter, IReadOnlyList<M>>> MatchesToRelative<M>(IReadOnlyList<M> matches, CreateMatchDelegate<M> createMatchDelegate) 
            where M : Match
        {
            var relativeMatches = new List<Tuple<IAdapter, IReadOnlyList<M>>>();
            var dic = new Dictionary<IAdapter, List<M>>();
            foreach (M match in matches)
            {
                var relativeRanges = documentMap.GetRelativeRange(match.Range);

                if (relativeRanges.Count == 0)
                {
                    throw new MatchesNotFoundException("", new Match[] { match }, null);
                }

                for (int i = 0; i < relativeRanges.Count; i++)
                {
                    RelativeRange<IAdapter> relativeAdjustedRange = relativeRanges[i];

                    var adapter = relativeAdjustedRange.Source;
                    if(!dic.ContainsKey(adapter)){
                        var list = new List<M>();
                        dic.Add(relativeAdjustedRange.Source, list);
                        var item = Tuple.Create<IAdapter, IReadOnlyList<M>>(adapter, list);
                        relativeMatches.Add(item);
                    }
                    dic[adapter].Add(createMatchDelegate(match, relativeRanges[i].Range));
                }
            }
            return relativeMatches;
        }

        public void ReplaceRanges(IReadOnlyList<MatchWithReplacement> matches)
        {
            Contract.Requires(matches != null);

            var relativeMatches = MatchesToRelative(matches, (oldMatch, newRange) => new MatchWithReplacement(new Match(oldMatch.Content, newRange), oldMatch.Replacement));

            for (var i = relativeMatches.Count - 1; i >= 0; i--)
            {
                var forCurrentAdapter = relativeMatches[i];
                var adapter = forCurrentAdapter.Item1;

                adapter.ReplaceRanges(forCurrentAdapter.Item2, documentModels[adapter], format);
            }
        }
    }
}
