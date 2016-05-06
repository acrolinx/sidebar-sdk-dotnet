/* Copyright (c) 2016 Acrolinx GmbH */

using Acrolinx.Sdk.Sidebar.Documents;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acrolinx.Sdk.Sidebar.Util.Changetracking
{
    /// <summary>
    /// RelativeRange is the result of a request made to the <see cref="DocumentMap"/>.
    /// </summary>
    /// <typeparam name="SourceType">The source could be something like a control of an application, a path expression like XPath or an id.</typeparam>
    public class RelativeRange<SourceType>
    {
        public RelativeRange(IRange range, SourceType source)
        {
            Contract.Requires(range != null);
            Contract.Requires(source != null);

            this.Range = range;
            this.Source = source;
        }

        /// <summary>
        /// The relative range inside the Source.
        /// </summary>
        public IRange Range { get; private set; }

        /// <summary>
        /// The source of the document part. 
        /// </summary>
        public SourceType Source { get; private set; }
    }
}
