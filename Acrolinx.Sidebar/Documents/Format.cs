/* Copyright (c) 2016 Acrolinx GmbH */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Acrolinx.Sdk.Sidebar.Documents
{
    public enum Format
    {
        XML, Text, HTML, Word_XML,
        Markdown,
        /// <summary>
        /// Use server-side detection of input format based on file name. 5.2 server is required.
        /// </summary>
        Auto
    }
}
