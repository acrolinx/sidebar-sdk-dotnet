/* Copyright (c) 2016 Acrolinx GmbH */

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
namespace Acrolinx.Sdk.Sidebar.Documents
{
    [ContractClass(typeof(IDocumentContracts))]
    public interface IDocument
    {
        string Content { get;  }
        Format Format { get;  }
        string Reference { get;  }
        IReadOnlyList<IRange> Selections { get; }
        Format StringToFormat(String inputFormat);
    }
    
    [ContractClassFor(typeof(IDocument))]
    public abstract class IDocumentContracts : IDocument
    {
        private IDocumentContracts()
        {

        }
        public string Content
        {
            get { 
                Contract.Ensures(Contract.Result<string>() != null);
                return "";
            }
        }

        public Format Format
        {
            get { throw new NotImplementedException(); }
        }

        public string Reference
        {
            get
            {
                Contract.Ensures(Contract.Result<string>() != null);
                return "";
            }
        }

        public IReadOnlyList<IRange> Selections
        {
            get
            {
                return new List<IRange>();
            }
        }

        public Format StringToFormat(string format)
        {
            return Format.Auto;
        }
    }
}
