/* Copyright (c) 2016-present Acrolinx GmbH */


namespace Acrolinx.Sdk.Sidebar.Documents
{
    public enum Format
    {
        XML, Text, HTML, Word_XML,
        Markdown, CPP, JAVA,
        PROPERTIES, YAML, JSON,
        /// <summary>
        /// Use server-side detection of input format based on file name. 5.2 server is required.
        /// </summary>
        Auto
    }
}
