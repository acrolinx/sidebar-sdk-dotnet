/* Copyright (c) 2016 Acrolinx GmbH */

using Acrolinx.Sdk.Sidebar.Documents;
using Acrolinx.Sdk.Sidebar.Util.Changetracking;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
namespace Acrolinx.Sdk.Sidebar.Util.Adapter
{
    /// <summary>
    /// IAdapter is used by <see cref="MultiAdapter"/> to extract text and select or replace parts of the text.
    /// </summary>
    [ContractClass(typeof(IAdapterContract))]
    public interface IAdapter
    {
        string Extract(Format format);
        void SelectRanges(IReadOnlyList<Match> matches, DocumentModel model, Format format);
        void ReplaceRanges(IReadOnlyList<MatchWithReplacement> matches, DocumentModel model, Format format);
    }

    [ContractClassFor(typeof(IAdapter))]
    public abstract class IAdapterContract : IAdapter
    {
        private IAdapterContract()
        {

        }

        public string Extract(Format format)
        {
            Contract.Ensures(Contract.Result<string>() != null);
            return "";
        }
        public string TagName
        {
            get
            {
                Contract.Ensures(Contract.Result<string>() != null);
                return "";
            }
        }
        public void SelectRanges(IReadOnlyList<Match> matches, DocumentModel model, Format format)
        {
            Contract.Requires(matches != null);
            Contract.Requires(model != null);
        }
        public void ReplaceRanges(IReadOnlyList<MatchWithReplacement> matches, DocumentModel model, Format format)
        {
            Contract.Requires(matches != null);
            Contract.Requires(model != null);
        }
    }
}
