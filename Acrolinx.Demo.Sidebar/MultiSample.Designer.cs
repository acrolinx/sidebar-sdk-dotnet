/* Copyright (c) 2016 Acrolinx GmbH */
namespace Acrolinx.Demo.Sidebar
{
    partial class MultiSample
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MultiSample));
            this.panel1 = new System.Windows.Forms.Panel();
            this.panelContent = new System.Windows.Forms.TableLayoutPanel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxPlainXML = new System.Windows.Forms.TextBox();
            this.textBoxTitle = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.addTextSnippetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.acrolinxSidebar = new Acrolinx.Sdk.Sidebar.AcrolinxSidebar();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panelContent);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.menuStrip1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(816, 513);
            this.panel1.TabIndex = 6;
            // 
            // panelContent
            // 
            this.panelContent.AutoScroll = true;
            this.panelContent.ColumnCount = 1;
            this.panelContent.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.panelContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContent.Location = new System.Drawing.Point(0, 169);
            this.panelContent.Name = "panelContent";
            this.panelContent.RowCount = 1;
            this.panelContent.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.panelContent.Size = new System.Drawing.Size(816, 344);
            this.panelContent.TabIndex = 7;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.textBoxPlainXML);
            this.panel2.Controls.Add(this.textBoxTitle);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 24);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(816, 145);
            this.panel2.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "XML:";
            // 
            // textBoxPlainXML
            // 
            this.textBoxPlainXML.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxPlainXML.Location = new System.Drawing.Point(48, 48);
            this.textBoxPlainXML.Multiline = true;
            this.textBoxPlainXML.Name = "textBoxPlainXML";
            this.textBoxPlainXML.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxPlainXML.Size = new System.Drawing.Size(751, 88);
            this.textBoxPlainXML.TabIndex = 2;
            this.textBoxPlainXML.Text = "<x>\r\n    <some>Test</some>\r\n    <structured>data</structured>\r\n</x>";
            // 
            // textBoxTitle
            // 
            this.textBoxTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxTitle.Location = new System.Drawing.Point(48, 22);
            this.textBoxTitle.Name = "textBoxTitle";
            this.textBoxTitle.Size = new System.Drawing.Size(751, 20);
            this.textBoxTitle.TabIndex = 1;
            this.textBoxTitle.Text = "Topspin Document";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Title:";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addTextSnippetToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(816, 24);
            this.menuStrip1.TabIndex = 6;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // addTextSnippetToolStripMenuItem
            // 
            this.addTextSnippetToolStripMenuItem.Name = "addTextSnippetToolStripMenuItem";
            this.addTextSnippetToolStripMenuItem.Size = new System.Drawing.Size(109, 20);
            this.addTextSnippetToolStripMenuItem.Text = "Add Text Snippet";
            this.addTextSnippetToolStripMenuItem.Click += new System.EventHandler(this.addTextSnippetToolStripMenuItem_Click);
            // 
            // acrolinxSidebar
            // 
            this.acrolinxSidebar.Dock = System.Windows.Forms.DockStyle.Right;
            this.acrolinxSidebar.Location = new System.Drawing.Point(816, 0);
            this.acrolinxSidebar.MaximumSize = new System.Drawing.Size(300, 0);
            this.acrolinxSidebar.MinimumSize = new System.Drawing.Size(300, 0);
            this.acrolinxSidebar.Name = "acrolinxSidebar";
            this.acrolinxSidebar.SidebarSourceLocation = null;
            this.acrolinxSidebar.Size = new System.Drawing.Size(300, 513);
            this.acrolinxSidebar.TabIndex = 2;
            // 
            // MultiSample
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1116, 513);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.acrolinxSidebar);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MultiSample";
            this.Text = "Acrolinx Sidebar .NET Multi";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Acrolinx.Sdk.Sidebar.AcrolinxSidebar acrolinxSidebar;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TableLayoutPanel panelContent;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem addTextSnippetToolStripMenuItem;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox textBoxTitle;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxPlainXML;



    }
}

