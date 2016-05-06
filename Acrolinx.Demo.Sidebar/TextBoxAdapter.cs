/* Copyright (c) 2016 Acrolinx GmbH */

using Acrolinx.Sdk.Sidebar;
using Acrolinx.Sdk.Sidebar.Documents;
using Acrolinx.Sdk.Sidebar.Util.Adapter;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Acrolinx.Demo.Sidebar
{
    /// <summary>
    /// TextBoxAdapter is a sample implementation of <see cref="IAdapter"/> . The TextBoxAdapter uses <see cref="UniversalAdapter"/> as base class, which simplifies the implementation since it already does some offset magic to adjust the guessed offsets to the actual offsets of the source.
    /// </summary>
    class TextBoxAdapter : UniversalAdapter
    {
        public TextBoxAdapter(string tagName, TextBox textBox, bool encodeContent)
            : base(tagName, encodeContent)
        {
            Contract.Requires(tagName != null);
            Contract.Requires(textBox != null);

            this.TextBox = textBox;
        }
        protected override string ExtractRaw()
        {
            return TextBox.Text;
        }

        public TextBox TextBox { get; private set; }

        protected override void SelectRawRange(IRange range, Format format)
        {
            TextBox.Select(range.Start, range.Length);
            TextBox.Focus();
        }

        protected override void ReplaceRawRange(IRange range, string replacement, Format format)
        {
            System.Diagnostics.Trace.Assert(range.Start >= 0);
            System.Diagnostics.Trace.Assert(range.End <= TextBox.TextLength);

            SelectRawRange(range, format);
            TextBox.SelectedText = replacement;
        }
    }
}
