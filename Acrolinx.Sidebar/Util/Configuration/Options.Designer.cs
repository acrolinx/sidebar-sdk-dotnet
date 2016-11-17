namespace Acrolinx.Sdk.Sidebar.Util.Configuration
{
    partial class Options
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Options));
            this.buttonOk = new System.Windows.Forms.Button();
            this.serverAddress = new System.Windows.Forms.TextBox();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.buttonCancel = new System.Windows.Forms.Button();
            this.selectInSidebar = new System.Windows.Forms.CheckBox();
            this.LoggingGroupBox = new System.Windows.Forms.GroupBox();
            this.BrowsButton = new System.Windows.Forms.Button();
            this.LogDirectory = new System.Windows.Forms.TextBox();
            this.DirectoryLabel = new System.Windows.Forms.Label();
            this.ServerAddrGroupBox = new System.Windows.Forms.GroupBox();
            this.ValidateButton = new System.Windows.Forms.Button();
            this.validationSidebar = new Acrolinx.Sdk.Sidebar.AcrolinxSidebar();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.LoggingGroupBox.SuspendLayout();
            this.ServerAddrGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonOk
            // 
            this.buttonOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOk.Location = new System.Drawing.Point(353, 267);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(75, 23);
            this.buttonOk.TabIndex = 6;
            this.buttonOk.Text = "&OK";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // serverAddress
            // 
            this.serverAddress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.serverAddress.Enabled = false;
            this.serverAddress.Location = new System.Drawing.Point(25, 32);
            this.serverAddress.Name = "serverAddress";
            this.serverAddress.Size = new System.Drawing.Size(442, 20);
            this.serverAddress.TabIndex = 1;
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(435, 267);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 7;
            this.buttonCancel.Text = "&Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // selectInSidebar
            // 
            this.selectInSidebar.AutoSize = true;
            this.selectInSidebar.Checked = true;
            this.selectInSidebar.CheckState = System.Windows.Forms.CheckState.Checked;
            this.selectInSidebar.Location = new System.Drawing.Point(25, 68);
            this.selectInSidebar.Name = "selectInSidebar";
            this.selectInSidebar.Size = new System.Drawing.Size(106, 17);
            this.selectInSidebar.TabIndex = 2;
            this.selectInSidebar.Text = global::Acrolinx.Sdk.Sidebar.Properties.Resources.SDK_OPTION_LABEL_SELECTINSIDEBAR;
            this.selectInSidebar.UseVisualStyleBackColor = true;
            this.selectInSidebar.CheckedChanged += new System.EventHandler(this.selectInSidebar_CheckedChanged);
            // 
            // LoggingGroupBox
            // 
            this.LoggingGroupBox.Controls.Add(this.BrowsButton);
            this.LoggingGroupBox.Controls.Add(this.LogDirectory);
            this.LoggingGroupBox.Controls.Add(this.DirectoryLabel);
            this.LoggingGroupBox.Location = new System.Drawing.Point(19, 139);
            this.LoggingGroupBox.Name = "LoggingGroupBox";
            this.LoggingGroupBox.Size = new System.Drawing.Size(491, 117);
            this.LoggingGroupBox.TabIndex = 0;
            this.LoggingGroupBox.TabStop = false;
            // 
            // BrowsButton
            // 
            this.BrowsButton.Location = new System.Drawing.Point(434, 66);
            this.BrowsButton.Name = "BrowsButton";
            this.BrowsButton.Size = new System.Drawing.Size(33, 20);
            this.BrowsButton.TabIndex = 5;
            this.BrowsButton.Text = "...";
            this.BrowsButton.UseVisualStyleBackColor = true;
            this.BrowsButton.Click += new System.EventHandler(this.BrowsButton_Click);
            // 
            // LogDirectory
            // 
            this.LogDirectory.Location = new System.Drawing.Point(25, 66);
            this.LogDirectory.Name = "LogDirectory";
            this.LogDirectory.ReadOnly = true;
            this.LogDirectory.Size = new System.Drawing.Size(387, 20);
            this.LogDirectory.TabIndex = 4;
            // 
            // DirectoryLabel
            // 
            this.DirectoryLabel.AutoSize = true;
            this.DirectoryLabel.Location = new System.Drawing.Point(25, 36);
            this.DirectoryLabel.Name = "DirectoryLabel";
            this.DirectoryLabel.Size = new System.Drawing.Size(0, 13);
            this.DirectoryLabel.TabIndex = 0;
            // 
            // ServerAddrGroupBox
            // 
            this.ServerAddrGroupBox.Controls.Add(this.ValidateButton);
            this.ServerAddrGroupBox.Controls.Add(this.serverAddress);
            this.ServerAddrGroupBox.Controls.Add(this.selectInSidebar);
            this.ServerAddrGroupBox.Location = new System.Drawing.Point(19, 17);
            this.ServerAddrGroupBox.Name = "ServerAddrGroupBox";
            this.ServerAddrGroupBox.Size = new System.Drawing.Size(491, 117);
            this.ServerAddrGroupBox.TabIndex = 0;
            this.ServerAddrGroupBox.TabStop = false;
            // 
            // ValidateButton
            // 
            this.ValidateButton.Location = new System.Drawing.Point(392, 64);
            this.ValidateButton.Name = "ValidateButton";
            this.ValidateButton.Size = new System.Drawing.Size(75, 23);
            this.ValidateButton.TabIndex = 3;
            this.ValidateButton.Text = "Validate";
            this.ValidateButton.UseVisualStyleBackColor = true;
            // 
            // validationSidebar
            // 
            this.validationSidebar.ClientLocale = "";
            this.validationSidebar.ClientSignature = "123";
            this.validationSidebar.Location = new System.Drawing.Point(21, 277);
            this.validationSidebar.MaximumSize = new System.Drawing.Size(300, 0);
            this.validationSidebar.MinimumSize = new System.Drawing.Size(300, 450);
            this.validationSidebar.Name = "validationSidebar";
            this.validationSidebar.ShowServerSelector = false;
            this.validationSidebar.SidebarSourceLocation = null;
            this.validationSidebar.Size = new System.Drawing.Size(300, 450);
            this.validationSidebar.TabIndex = 5;
            this.validationSidebar.Visible = false;
            // 
            // Options
            // 
            this.AcceptButton = this.buttonOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(522, 298);
            this.Controls.Add(this.ServerAddrGroupBox);
            this.Controls.Add(this.LoggingGroupBox);
            this.Controls.Add(this.validationSidebar);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOk);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Options";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Options";
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.LoggingGroupBox.ResumeLayout(false);
            this.LoggingGroupBox.PerformLayout();
            this.ServerAddrGroupBox.ResumeLayout(false);
            this.ServerAddrGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.TextBox serverAddress;
        private System.Windows.Forms.ErrorProvider errorProvider;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.CheckBox selectInSidebar;
        private Sdk.Sidebar.AcrolinxSidebar validationSidebar;
        private System.Windows.Forms.GroupBox LoggingGroupBox;
        private System.Windows.Forms.Button BrowsButton;
        private System.Windows.Forms.TextBox LogDirectory;
        private System.Windows.Forms.Label DirectoryLabel;
        private System.Windows.Forms.GroupBox ServerAddrGroupBox;
        private System.Windows.Forms.Button ValidateButton;
    }
}