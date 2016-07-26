/* Copyright (c) 2016 Acrolinx GmbH */

using Acrolinx.Sdk.Sidebar.Documents;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Acrolinx.Demo.Sidebar
{
    /// <summary>
    /// This example shows the minimal effort for a POC. The whole select / replace will fail if the text was changed.
    /// Please have a look at the properties, events and functions of the <see cref="AcrolinxSidebar"/> control.
    /// See: <see cref="MultiSample"/> for examples how to deal with changes and multiple input fields.
    /// </summary>
    public partial class SimpleSample : Form
    {
        private string documentReference;

        public SimpleSample() : this(Format.HTML, "your_file_name.txt", "<body>" +Environment.NewLine + "   <h1> This is a header without dot </h1>" + Environment.NewLine + "    <div>This is an simple sample tesst</div>" + Environment.NewLine + "</body>")
        {
            
        }

        public SimpleSample(Format format, string documentReference, string text)
        {
            InitializeComponent();

            FillFormatList();

            formatComboBox.SelectedItem = format;
            this.documentReference = documentReference;
            this.textBox.Text = text;
            this.Text += " - " + documentReference;

            acrolinxSidebar.Start();
        }

        private void FillFormatList()
        {
            foreach (Format f in Enum.GetValues(typeof(Format)))
            {
                formatComboBox.Items.Add(f);
            }
        }

        private void acrolinxSidebar_RequestCheck(object sender, EventArgs e)
        {
            System.Diagnostics.Trace.WriteLine("acrolinxSidebar_RequestCheck");

            var document = new Document();
            document.Content = textBox.Text;
            document.Format = (Format)formatComboBox.SelectedItem;
            document.Reference = documentReference;
            acrolinxSidebar.Check(document);
        }

        private void acrolinxSidebar_InitFinished(object sender, EventArgs e)
        {
            System.Diagnostics.Trace.WriteLine("acrolinxSidebar_InitFinished");
        }

        private void acrolinxSidebar_Checked(object sender, Sdk.Sidebar.CheckedEventArgs e)
        {
            System.Diagnostics.Trace.WriteLine("acrolinxSidebar_Checked");
        }

        private void acrolinxSidebar_SidebarSourceNotReachable(object sender, EventArgs e)
        {
            System.Diagnostics.Trace.WriteLine("acrolinxSidebar_SidebarSourceNotReachable");

            acrolinxSidebar.Start(); //retry
        }

        private void acrolinxSidebar_SelectRanges(object sender, Sdk.Sidebar.MatchesEventArgs e)
        {
            System.Diagnostics.Trace.WriteLine("acrolinxSidebar_SelectRanges");

            SelectWholeRange(e.CheckId, e.Matches);
        }

        private void SelectWholeRange(string checkId, IReadOnlyList<Match> matches)
        {
            try
            {
                var range = new Range(matches[0].Range.Start, matches[matches.Count - 1].Range.End);
                textBox.Select(range.Start, range.Length);
                textBox.ScrollToCaret();
                textBox.Focus();
                if (!(textBox.SelectedText.StartsWith(matches[0].Content) && textBox.SelectedText.EndsWith(matches[matches.Count - 1].Content)))
                {
                    acrolinxSidebar.InvalidateRanges(checkId, matches);
                    textBox.Select(textBox.SelectionStart, 0);
                }
            }
            catch (Exception)
            {
                acrolinxSidebar.InvalidateRanges(checkId, matches);
            }
        }

        private void acrolinxSidebar_ReplaceRanges(object sender, Sdk.Sidebar.MatchesWithReplacementEventArgs e)
        {
            System.Diagnostics.Trace.WriteLine("acrolinxSidebar_ReplaceRanges");

            SelectWholeRange(e.CheckId, e.Matches);
            if (textBox.SelectionLength > 0)
            {
                textBox.SelectedText = string.Join("", e.Matches.Select(m => m.Replacement));
            }
        }
    }
}
