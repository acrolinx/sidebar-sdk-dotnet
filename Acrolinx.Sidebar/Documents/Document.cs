/* Copyright (c) 2016 Acrolinx GmbH */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Acrolinx.Sdk.Sidebar.Documents
{
    public class Document : IDocument
    {
        public Document() : this("",Format.Text, "")
        {
        }

        public Document(string content, Format format, string reference) : this(content,format, reference, new List<IRange>())
        {

        }
        public Document(string content, Format format, string reference, IReadOnlyList<IRange> selections)
        {
            Content = content;
            Format = format;
            Reference = reference;
            Selections = selections;
        }

        public string Content
        {
            get;
            set;
        }

        public Format Format
        {
            get;
            set;
        }

        public string Reference
        {
            get;
            set;
        }

        public IReadOnlyList<IRange> Selections
        {
            get;
            set;
        }

        public override string ToString()
        {
            return Format.ToString() + ": " + Content;
        }

        public Format StringToFormat(String inputFormat)
        {
            if ("XML".Equals(inputFormat, StringComparison.OrdinalIgnoreCase))
                return Format.XML;
            if ("HTML".Equals(inputFormat, StringComparison.OrdinalIgnoreCase))
                return Format.HTML;
            if ("MARKDOWN".Equals(inputFormat, StringComparison.OrdinalIgnoreCase))
                return Format.Markdown;
            if ("TEXT".Equals(inputFormat, StringComparison.OrdinalIgnoreCase))
                return Format.Text;
            if ("WORD_XML".Equals(inputFormat, StringComparison.OrdinalIgnoreCase))
                return Format.Word_XML;
            return Format;
        }
    }
}
