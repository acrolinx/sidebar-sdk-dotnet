/* Copyright (c) 2016 Acrolinx GmbH */

namespace Acrolinx.Sdk.Sidebar
{
    partial class AcrolinxSidebar
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AcrolinxSidebar));
            this.labelImage = new System.Windows.Forms.Label();
            this.webView2 = new Microsoft.Web.WebView2.WinForms.WebView2();
            ((System.ComponentModel.ISupportInitialize)(this.webView2)).BeginInit();
            this.SuspendLayout();
            // 
            // labelImage
            // 
            this.labelImage.BackColor = System.Drawing.Color.White;
            this.labelImage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelImage.Image = ((System.Drawing.Image)(resources.GetObject("labelImage.Image")));
            this.labelImage.Location = new System.Drawing.Point(0, 0);
            this.labelImage.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelImage.Name = "labelImage";
            this.labelImage.Size = new System.Drawing.Size(320, 443);
            this.labelImage.TabIndex = 1;
            // 
            // webView2
            // 
            this.webView2.AllowExternalDrop = true;
            this.webView2.CreationProperties = null;
            this.webView2.DefaultBackgroundColor = System.Drawing.Color.White;
            this.webView2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webView2.Location = new System.Drawing.Point(0, 0);
            this.webView2.Margin = new System.Windows.Forms.Padding(4);
            this.webView2.Name = "webView2";
            this.webView2.Size = new System.Drawing.Size(320, 443);
            this.webView2.TabIndex = 2;
            this.webView2.ZoomFactor = 1D;
            this.webView2.CoreWebView2InitializationCompleted += new System.EventHandler<Microsoft.Web.WebView2.Core.CoreWebView2InitializationCompletedEventArgs>(this.webView2_CoreWebView2InitializationCompleted);
            this.webView2.NavigationStarting += new System.EventHandler<Microsoft.Web.WebView2.Core.CoreWebView2NavigationStartingEventArgs>(this.webView2_NavigationStarting_1);
            this.webView2.NavigationCompleted += new System.EventHandler<Microsoft.Web.WebView2.Core.CoreWebView2NavigationCompletedEventArgs>(this.webView2_NavigationCompleted);
            // 
            // AcrolinxSidebar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.webView2);
            this.Controls.Add(this.labelImage);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "AcrolinxSidebar";
            this.Size = new System.Drawing.Size(320, 443);
            this.Resize += new System.EventHandler(this.AcrolinxSidebar_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.webView2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label labelImage;
        private Microsoft.Web.WebView2.WinForms.WebView2 webView2;
    }
}
