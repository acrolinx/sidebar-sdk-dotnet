/* Copyright (c) 2016 Acrolinx GmbH */

using Acrolinx.Sdk.Sidebar;
using Acrolinx.Sdk.Sidebar.Documents;
using Acrolinx.Sdk.Sidebar.Exceptions;
using Acrolinx.Sdk.Sidebar.Util.Adapter;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Acrolinx.Demo.Sidebar
{
    class Integration
    {
        private readonly List<UniversalAdapter> adapterList = new List<UniversalAdapter>();
        private AcrolinxSidebar sidebar;

        public Integration(AcrolinxSidebar sidebar)
        {
            this.sidebar = sidebar;

            //By default, a publicly available Acrolinx Sidebar is loaded.
            //This requires that you configure CORS on your Acrolinx Server.
            //Add cors.allowedOrigins=* to your coreserver.properties.
            //See: https://en.wikipedia.org/wiki/Cross-origin_resource_sharing
            //If the sidebar is loaded from a host using https it is only allowed to talk to Acrolinx Servers which run with SSL.

            //Each Acrolinx Server installation >= 4.7 ships its own version of the sidebar.
            //This allows you to talk to the server without configuring CORS.
            //Ensure the SidebarSourceLocation uses the same host as ServerAddress.
            //Ensure the external name of the Acrolinx Server equals the host name you are talking to.
            //sidebar.SidebarSourceLocation = "http://yourlocalserver:8031/sidebar/v14/index.html";
            //sidebar.ServerAddress = "http://yourlocalserver:8031";
            //sidebar.ShowServerSelector = false;

            //sidebar.ClientSignature = "ASK_ACROLINX_FOR_A_CLIENT_SIGNATURE";

            //Register the events you are interested in.
            sidebar.RequestCheck += RequestCheck;
            sidebar.Checked += Checked;
            sidebar.SelectRanges += SelectRanges;
            sidebar.ReplaceRanges += ReplaceRanges;
            sidebar.SidebarSourceNotReachable += SidebarSourceNotReachable;

            //Set version information. This is used for support and Acrolinx Analytics.
            //sidebar.RegisterClientComponent(typeof(Integration).Assembly, "Acrolinx for " + Application.ProductName, AcrolinxSidebar.SoftwareComponentCategory.MAIN);
            //sidebar.RegisterClientComponent(Assembly.GetEntryAssembly(), Application.ProductName, AcrolinxSidebar.SoftwareComponentCategory.DEFAULT);

            //Start the sidebar, which connects to an Acrolinx Server.
            sidebar.Start();
        }

        void SidebarSourceNotReachable(object sender, EventArgs e)
        {
            if (MessageBox.Show(null, "Sidebar could not be loaded from '" + sidebar.SidebarSourceLocation + "' due to connection problems. Retry?",
                "Acrolinx", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error) == DialogResult.Retry)
            {
                sidebar.Start();
            }
        }

        public void RegisterTextBox(TextBox textBox)
        {
            Contract.Requires(textBox != null);

            adapterList.Add(new TextBoxAdapter("paragraph", textBox, true));
        }
        public void RegisterTitleTextBox(TextBox textBox)
        {
            Contract.Requires(textBox != null);

            adapterList.Add(new TextBoxAdapter("title", textBox, true));
        }
        public void RegisterXmlTextBox(TextBox textBox)
        {
            Contract.Requires(textBox != null);

            adapterList.Add(new TextBoxAdapter("xml", textBox, false));
        }

        private MultiAdapter current = new MultiAdapter("", new UniversalAdapter[0], Format.Text);
        private MultiAdapter request = new MultiAdapter("", new UniversalAdapter[0], Format.Text);

        private void RequestCheck(object sender, EventArgs e)
        {
            System.Diagnostics.Trace.WriteLine("RequestCheck");

            request = new MultiAdapter("DotNetSampleXML", adapterList, Format.XML);
            request.DocumentReference = "dotnetSample/topspin.xml"; //Document reference should be set to path, uri or unique id of the current document

            var checkId = sidebar.Check(request.Document);

            System.Diagnostics.Trace.WriteLine("Check submitted with id: " + checkId);
        }

        private void Checked(object sender, CheckedEventArgs e)
        {
            System.Diagnostics.Trace.WriteLine("Checked: " + e.CheckId + " from " + e.Range.Start + " to " + e.Range.End);

            current = request;
        }


        private void SelectRanges(object sender, MatchesEventArgs e)
        {
            Contract.Requires(e.Matches.All(m => (0 <= m.Range.Start && m.Range.End <= current.Document.Content.Length)));

            System.Diagnostics.Trace.WriteLine("SelectRanges: " + e.Matches);

            try
            {
                current.SelectRanges(e.Matches);
            }
            catch (Exception err)
            {
                System.Diagnostics.Trace.WriteLine(err);
                sidebar.InvalidateRanges(e.CheckId, e.Matches);
            }
        }

        private void ReplaceRanges(object sender, MatchesWithReplacementEventArgs e)
        {
            Contract.Requires(e.Matches.All(m => m.Range.Start >= 0 && m.Range.End <= current.Document.Content.Length));
            System.Diagnostics.Trace.WriteLine("ReplaceRanges: " + e.Matches);
            try
            {
                current.ReplaceRanges(e.Matches);
            }
            catch (Exception err)
            {
                System.Diagnostics.Trace.WriteLine(err);
                sidebar.InvalidateRanges(e.CheckId, e.Matches);
            }
        }
    }
}
