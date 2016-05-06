/* Copyright (c) 2016 Acrolinx GmbH */

using System;
using System.Diagnostics.Contracts;
namespace Acrolinx.Sdk.Sidebar.Documents
{
    [ContractClass(typeof(IDocumentContracts))]
    public interface IDocument
    {
        string Content { get;  }
        Format Format { get;  }
        string Reference { get;  }
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
    }
}
