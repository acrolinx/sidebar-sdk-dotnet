/* Copyright (c) 2016 Acrolinx GmbH */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using Acrolinx.Sdk.Sidebar;
using System.Diagnostics.Contracts;

namespace Acrolinx.Demo.Sidebar
{
    public partial class MultiSample : Form
    {
        private String[] topspin = Regex.Split(Properties.Settings.Default.topspin.Replace("\r\n","\n").Replace("\r", "\n"), "\\n\\n+", RegexOptions.Multiline);

        Integration integration;

        public MultiSample()
        {
            InitializeComponent();

            panelContent.VerticalScroll.Visible = true;

            integration = new Integration(acrolinxSidebar);


            integration.RegisterTitleTextBox(textBoxTitle); //Deal with plain text
            integration.RegisterXmlTextBox(textBoxPlainXML); //Deal with xml content

            for (int i = 0; i < 3; i++)
            {
                addTextSnippet();
            }


        }

         private void addTextSnippetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            addTextSnippet();
        }

        /// <summary>
        /// Demo how an integration could deal with a dynamic set of controls as source for a check request.
        /// </summary>
         private void addTextSnippet()
         {
             TextBox newTextBox = new TextBox();
             newTextBox.Multiline = true;

             newTextBox.ScrollBars = ScrollBars.Both;
             newTextBox.Height = 50;
             newTextBox.Dock = DockStyle.Fill;

             newTextBox.Text = topspin[this.panelContent.Controls.Count % topspin.Length].Replace("\n", System.Environment.NewLine);

             this.panelContent.Controls.Add(newTextBox);

             integration.RegisterTextBox(newTextBox);
         }
    }
}
