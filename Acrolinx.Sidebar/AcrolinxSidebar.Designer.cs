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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AcrolinxSidebar));
            this.webBrowser = new System.Windows.Forms.WebBrowser();
            this.labelImage = new System.Windows.Forms.Label();
            this.FixFocusTimer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // webBrowser
            // 
            this.webBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowser.Location = new System.Drawing.Point(0, 0);
            this.webBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser.Name = "webBrowser";
            this.webBrowser.Size = new System.Drawing.Size(300, 450);
            this.webBrowser.TabIndex = 0;
            this.webBrowser.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.webBrowser_DocumentCompleted);
            this.webBrowser.Navigated += new System.Windows.Forms.WebBrowserNavigatedEventHandler(this.webBrowser_Navigated);
            // 
            // labelImage
            // 
            this.labelImage.BackColor = System.Drawing.Color.White;
            this.labelImage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelImage.Image = ((System.Drawing.Image)(resources.GetObject("labelImage.Image")));
            this.labelImage.Location = new System.Drawing.Point(0, 0);
            this.labelImage.Name = "labelImage";
            this.labelImage.Size = new System.Drawing.Size(300, 450);
            this.labelImage.TabIndex = 1;
            // 
            // FixFocusTimer
            // 
            this.FixFocusTimer.Tick += new System.EventHandler(this.OnFixFocusTimerTick);
            // 
            // AcrolinxSidebar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.labelImage);
            this.Controls.Add(this.webBrowser);
            this.Name = "AcrolinxSidebar";
            this.Size = new System.Drawing.Size(300, 450);
            this.Resize += new System.EventHandler(this.AcrolinxSidebar_Resize);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.WebBrowser webBrowser;
        private System.Windows.Forms.Label labelImage;
        private System.Windows.Forms.Timer FixFocusTimer;
    }
}
