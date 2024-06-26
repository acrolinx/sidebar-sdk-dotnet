﻿/* Copyright (c) 2016 Acrolinx GmbH */

using Acrolinx.Sdk.Sidebar.Documents;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using Range = Acrolinx.Sdk.Sidebar.Documents.Range;

namespace Acrolinx.Sdk.Sidebar
{
    [ContractClass(typeof(ISidebarContract))]
    public interface ISidebar
    {
        event SidebarLoadedEventHandler SidebarLoaded;
        event SidebarInitFinishedEventHandler InitFinished;
        event SidebarSourceNotReachableEventHandler SidebarSourceNotReachable;
        event SidebarDocumentLoadedEventHandler DocumentLoaded;
        event SidebarCheckedEventHandler Checked;
        event SidebarCheckRequestedEventHandler RequestCheck;
        event SidebarSelectRangesEventHandler SelectRanges;
        event SidebarReplaceRangesEventHandler ReplaceRanges;
        event SidebarProcessEmbedCheckDataEventHandler ProcessEmbedCheckData;
        event SidebarOpenBrowserEventHandler OpenBrowser;

        string ClientSignature
        {
            get;
            set;
        }
        string MinimumSidebarVersion
        {
            get;
            set;
        }
        string ServerAddress
        {
            get;
            set;
        }
        bool ShowServerSelector
        {
            get;
            set;
        }
        bool ReadOnlySuggestions
        {
            get;
            set;
        }
        bool FixFocusWorkaround
        {
            get;
            set;
        }
        bool SupportCheckSelection { get; set; }
        string StartPageSourceLocation { get; set; }

        System.Threading.Tasks.Task<string> Check(IDocument document);

        void CancelCheck();
    }

    [ContractClassFor(typeof(ISidebar))]
    public abstract class ISidebarContract : ISidebar
    {
        public Task<string> Check(IDocument document)
        {
            Contract.Requires(document != null);
            return Task.Run(() => string.Empty);
        }

        public void CancelCheck()
        {
            throw new NotImplementedException();
        }
#pragma warning disable
        public event SidebarInitFinishedEventHandler InitFinished;

        public event SidebarSourceNotReachableEventHandler SidebarSourceNotReachable;

        public event SidebarDocumentLoadedEventHandler DocumentLoaded;

        public event SidebarCheckedEventHandler Checked;

        public event SidebarCheckRequestedEventHandler RequestCheck;

        public event SidebarSelectRangesEventHandler SelectRanges;

        public event SidebarReplaceRangesEventHandler ReplaceRanges;

        public event SidebarLoadedEventHandler SidebarLoaded;

        public event SidebarProcessEmbedCheckDataEventHandler ProcessEmbedCheckData;

        public event SidebarOpenBrowserEventHandler OpenBrowser;

        public string ClientSignature
        {
            get
            {
                Contract.Ensures(Contract.Result<string>() != null);
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public string MinimumSidebarVersion
        {
            get
            {
                Contract.Ensures(Contract.Result<string>() != null);
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public string ServerAddress
        {
            get
            {
                Contract.Ensures(Contract.Result<string>() != null);
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public bool ShowServerSelector
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public bool FixFocusWorkaround
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public string StartPageSourceLocation
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public bool ReadOnlySuggestions
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public bool SupportCheckSelection
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }
#pragma warning restore
    }

    public class CheckedEventArgs : CheckEventArgs
    {
        internal CheckedEventArgs(string checkId, Range range) : base(checkId)
        {
            Range = range;
        }

        public Range Range { get; private set; }
    }
    public class CheckRequestedEventArgs : EventArgs
    {
        internal CheckRequestedEventArgs(ICheckOptions options)
        {
            Contract.Requires(options != null);
            Options = options;
        }

        public ICheckOptions Options { get; private set; }
    }

    public class CheckEventArgs : EventArgs
    {
        internal CheckEventArgs(string checkId)
        {
            Contract.Requires(checkId != null);
            CheckId = checkId;
        }
        public string CheckId { get; private set; }
    }

    public class MatchesEventArgs : CheckEventArgs
    {
        internal MatchesEventArgs(string checkId, IEnumerable<Match> matches)
            : base(checkId)
        {
            Contract.Requires(matches != null);
            Matches = new List<Match>(matches);
        }
        public IReadOnlyList<Match> Matches { get; private set; }
    }

    public class MatchesWithReplacementEventArgs : CheckEventArgs
    {
        internal MatchesWithReplacementEventArgs(string checkId, IEnumerable<MatchWithReplacement> matches)
            : base(checkId)
        {
            Contract.Requires(matches != null);
            Matches = new List<MatchWithReplacement>(matches);
        }
        public IReadOnlyList<MatchWithReplacement> Matches { get; private set; }
    }

    public class SidebarUrlEvenArgs : EventArgs
    {
        public SidebarUrlEvenArgs(Uri url)
        {
            Contract.Requires(url != null);
            this.Url = url;
        }
        public Uri Url { get; private set; }
    }
    public class SidebarDocumentLoadedEvenArgs : SidebarUrlEvenArgs
    {
        public SidebarDocumentLoadedEvenArgs(bool isValidSidebar, Uri url) : base(url)
        {
            Contract.Requires(url != null);
            this.ValidSidebar = isValidSidebar;
        }
        public bool ValidSidebar { get; private set; }
    }

    public class OpenBrowserEventArgs : SidebarUrlEvenArgs
    {
        public OpenBrowserEventArgs(Uri url) : base(url)
        {
            Contract.Requires(url != null);
        }

        public bool Cancel { get; set; }
    }

    public class ProcessEmbedCheckDataEventArgs : EventArgs
    {
        internal ProcessEmbedCheckDataEventArgs(IDictionary<string, string> embedCheckInformation, Format inputFormat)
        {
            EmbedCheckInformation = embedCheckInformation;
            InputFormat = inputFormat;
        }

        public IDictionary<string, string> EmbedCheckInformation { get; private set; }
        public Format InputFormat { get; private set; }

        private string EmbedCheckToString(IDictionary<string, string> source, string keyValueSeparator, string sequenceSeparator)
        {
            var pairs = source.Select(x => string.Format("{0}{1}{2}", x.Key, keyValueSeparator, x.Value));
            return string.Join(sequenceSeparator, pairs);
        }

        public string GetEmbedCheckDataAsEmbeddableString(Format inputFormat = Format.Auto)
        {
            XmlDocument doc = new XmlDocument();
            doc.XmlResolver = null;
            var keyValueString = EmbedCheckToString(EmbedCheckInformation, "=", " ");

            inputFormat = (inputFormat == Format.Auto) ? InputFormat : inputFormat;
            if (inputFormat == Format.XML)
            {
                XmlProcessingInstruction procInstruction = doc.CreateProcessingInstruction("acrolinxCheckData", keyValueString);
                return procInstruction.OuterXml;
            }

            if (inputFormat == Format.HTML)
            {
                XmlElement metaElement = doc.CreateElement("meta");
                metaElement.SetAttribute("name", "acrolinxCheckData");
                foreach (var pair in EmbedCheckInformation)
                {
                    metaElement.SetAttribute(pair.Key, pair.Value);
                }
                return metaElement.OuterXml;
            }

            if (inputFormat == Format.Markdown)
            {
                string markDownStr = "name=\"acrolinxCheckData\" " + keyValueString;
                XmlComment markDown = doc.CreateComment(markDownStr);
                return markDown.OuterXml;
            }

            var jsonPairs = EmbedCheckInformation.Select(x => string.Format("{0}{1}{2}{3}{4}", "{\"key\":\"", x.Key, "\",\"value\":\"", x.Value, "\"}"));
            var jsonString = "{\"embedCheckInformation\":[" + string.Join(",", jsonPairs) + "]}";

            return jsonString;
        }
    }

    public delegate void SidebarInitFinishedEventHandler(object sender, EventArgs e);
    public delegate void SidebarLoadedEventHandler(object sender, SidebarUrlEvenArgs e);
    public delegate void SidebarSourceNotReachableEventHandler(object sender, SidebarUrlEvenArgs e);
    public delegate void SidebarDocumentLoadedEventHandler(object sender, SidebarDocumentLoadedEvenArgs e);
    public delegate void SidebarCheckRequestedEventHandler(object sender, CheckRequestedEventArgs e);
    public delegate void SidebarCheckedEventHandler(object sender, CheckedEventArgs e);
    public delegate void SidebarSelectRangesEventHandler(object sender, MatchesEventArgs e);
    public delegate void SidebarReplaceRangesEventHandler(object sender, MatchesWithReplacementEventArgs e);
    public delegate void SidebarProcessEmbedCheckDataEventHandler(object sender, ProcessEmbedCheckDataEventArgs e);
    public delegate void SidebarOpenBrowserEventHandler(object sender, OpenBrowserEventArgs e);
}