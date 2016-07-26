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

namespace Acrolinx.Sdk.Sidebar
{
    public partial class ToolBrowser : Form
    {
        private readonly Form parent;
        public ToolBrowser()
        {
            InitializeComponent();
        }

        public ToolBrowser(Form parent) : this(){
            this.parent = parent;
        }

        private void ToolBrowser_Deactivate(object sender, EventArgs e)
        {
            this.Close();
        }

        public void navigateAndShow(string url)
        {
            if (!this.Visible)
            {
                this.StartPosition = FormStartPosition.Manual;

                if(parent != null && parent.MdiParent != null)
                {
                    this.Location = CalculateCenterOfParent(parent.MdiParent);
                }
                else
                {
                    this.Location = CalculateCenterOfParent(parent);
                }

                this.Show(parent);
            } 
            webBrowser.Navigate(url);
        }

        private Point CalculateCenterOfParent(Form parent)
        {
            return new Point(Math.Max(0, parent.Location.X + (parent.Width - this.Width) / 2), Math.Max(0, parent.Location.Y + (parent.Height - this.Height) / 2));
        }

        private void webBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            Console.WriteLine("Tool browser navigated to: " + e.Url);
            this.Text = webBrowser.Document.Title;

            this.lblImage.Hide();
        }

        private void webBrowser_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            webBrowser.ScriptErrorsSuppressed = true;
            webBrowser.IsWebBrowserContextMenuEnabled = false;
        }
    }
}
