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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Options));
            this.buttonOk = new System.Windows.Forms.Button();
            this.textServerAddress = new System.Windows.Forms.TextBox();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.checkSelectInSidebar = new System.Windows.Forms.CheckBox();
            this.groupLogging = new System.Windows.Forms.GroupBox();
            this.buttonBrowse = new System.Windows.Forms.Button();
            this.textLogDirectory = new System.Windows.Forms.TextBox();
            this.labelDirectory = new System.Windows.Forms.Label();
            this.buttonConnect = new System.Windows.Forms.Button();
            this.labelServerAddress = new System.Windows.Forms.Label();
            this.labelStatus = new System.Windows.Forms.Label();
            this.textStatus = new System.Windows.Forms.TextBox();
            this.pictureStatus = new System.Windows.Forms.PictureBox();
            this.validationSidebar = new Acrolinx.Sdk.Sidebar.AcrolinxSidebar();
            this.groupLogging.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureStatus)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonOk
            // 
            this.buttonOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOk.Location = new System.Drawing.Point(356, 173);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(75, 23);
            this.buttonOk.TabIndex = 10;
            this.buttonOk.Text = "<OK>";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // textServerAddress
            // 
            this.textServerAddress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textServerAddress.Enabled = false;
            this.textServerAddress.Location = new System.Drawing.Point(12, 25);
            this.textServerAddress.Name = "textServerAddress";
            this.textServerAddress.Size = new System.Drawing.Size(416, 20);
            this.textServerAddress.TabIndex = 1;
            this.textServerAddress.TextChanged += new System.EventHandler(this.textServerAddress_TextChanged);
            this.textServerAddress.Enter += new System.EventHandler(this.textServerAddress_Enter);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(435, 173);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 11;
            this.buttonCancel.Text = "<Cancel>";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // checkSelectInSidebar
            // 
            this.checkSelectInSidebar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkSelectInSidebar.AutoSize = true;
            this.checkSelectInSidebar.Checked = true;
            this.checkSelectInSidebar.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkSelectInSidebar.Location = new System.Drawing.Point(310, 51);
            this.checkSelectInSidebar.Name = "checkSelectInSidebar";
            this.checkSelectInSidebar.Size = new System.Drawing.Size(118, 17);
            this.checkSelectInSidebar.TabIndex = 2;
            this.checkSelectInSidebar.Text = "<Select in Sidebar>";
            this.checkSelectInSidebar.UseVisualStyleBackColor = true;
            this.checkSelectInSidebar.CheckedChanged += new System.EventHandler(this.selectInSidebar_CheckedChanged);
            // 
            // groupLogging
            // 
            this.groupLogging.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupLogging.Controls.Add(this.buttonBrowse);
            this.groupLogging.Controls.Add(this.textLogDirectory);
            this.groupLogging.Controls.Add(this.labelDirectory);
            this.groupLogging.Location = new System.Drawing.Point(12, 100);
            this.groupLogging.Name = "groupLogging";
            this.groupLogging.Size = new System.Drawing.Size(498, 67);
            this.groupLogging.TabIndex = 7;
            this.groupLogging.TabStop = false;
            this.groupLogging.Text = "<Logging>";
            // 
            // buttonBrowse
            // 
            this.buttonBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonBrowse.Location = new System.Drawing.Point(459, 32);
            this.buttonBrowse.Name = "buttonBrowse";
            this.buttonBrowse.Size = new System.Drawing.Size(33, 20);
            this.buttonBrowse.TabIndex = 9;
            this.buttonBrowse.Text = "...";
            this.buttonBrowse.UseVisualStyleBackColor = true;
            this.buttonBrowse.Click += new System.EventHandler(this.BrowsButton_Click);
            // 
            // textLogDirectory
            // 
            this.textLogDirectory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textLogDirectory.Location = new System.Drawing.Point(6, 32);
            this.textLogDirectory.Name = "textLogDirectory";
            this.textLogDirectory.ReadOnly = true;
            this.textLogDirectory.Size = new System.Drawing.Size(447, 20);
            this.textLogDirectory.TabIndex = 9;
            // 
            // labelDirectory
            // 
            this.labelDirectory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelDirectory.AutoSize = true;
            this.labelDirectory.Location = new System.Drawing.Point(6, 17);
            this.labelDirectory.Name = "labelDirectory";
            this.labelDirectory.Size = new System.Drawing.Size(61, 13);
            this.labelDirectory.TabIndex = 8;
            this.labelDirectory.Text = "<Directory>";
            // 
            // buttonConnect
            // 
            this.buttonConnect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonConnect.Location = new System.Drawing.Point(435, 25);
            this.buttonConnect.Name = "buttonConnect";
            this.buttonConnect.Size = new System.Drawing.Size(75, 23);
            this.buttonConnect.TabIndex = 3;
            this.buttonConnect.Text = "<Connect>";
            this.buttonConnect.UseVisualStyleBackColor = true;
            this.buttonConnect.Click += new System.EventHandler(this.buttonConnect_Click);
            // 
            // labelServerAddress
            // 
            this.labelServerAddress.AutoSize = true;
            this.labelServerAddress.Location = new System.Drawing.Point(12, 9);
            this.labelServerAddress.Name = "labelServerAddress";
            this.labelServerAddress.Size = new System.Drawing.Size(88, 13);
            this.labelServerAddress.TabIndex = 0;
            this.labelServerAddress.Text = "<ServerAddress>";
            // 
            // labelStatus
            // 
            this.labelStatus.AutoSize = true;
            this.labelStatus.Location = new System.Drawing.Point(12, 58);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(49, 13);
            this.labelStatus.TabIndex = 4;
            this.labelStatus.Text = "<Status>";
            // 
            // textStatus
            // 
            this.textStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textStatus.Location = new System.Drawing.Point(12, 74);
            this.textStatus.Name = "textStatus";
            this.textStatus.ReadOnly = true;
            this.textStatus.Size = new System.Drawing.Size(416, 20);
            this.textStatus.TabIndex = 5;
            // 
            // pictureStatus
            // 
            this.pictureStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureStatus.Location = new System.Drawing.Point(435, 74);
            this.pictureStatus.Name = "pictureStatus";
            this.pictureStatus.Size = new System.Drawing.Size(20, 20);
            this.pictureStatus.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureStatus.TabIndex = 11;
            this.pictureStatus.TabStop = false;
            // 
            // validationSidebar
            // 
            this.validationSidebar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.validationSidebar.ClientLocale = "";
            this.validationSidebar.ClientSignature = "123";
            this.validationSidebar.Location = new System.Drawing.Point(12, 183);
            this.validationSidebar.MaximumSize = new System.Drawing.Size(300, 0);
            this.validationSidebar.MinimumSize = new System.Drawing.Size(300, 450);
            this.validationSidebar.Name = "validationSidebar";
            this.validationSidebar.ShowServerSelector = false;
            this.validationSidebar.SidebarSourceLocation = null;
            this.validationSidebar.Size = new System.Drawing.Size(300, 450);
            this.validationSidebar.TabIndex = 12;
            this.validationSidebar.Visible = false;
            // 
            // Options
            // 
            this.AcceptButton = this.buttonOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(522, 206);
            this.Controls.Add(this.pictureStatus);
            this.Controls.Add(this.textStatus);
            this.Controls.Add(this.labelStatus);
            this.Controls.Add(this.buttonConnect);
            this.Controls.Add(this.textServerAddress);
            this.Controls.Add(this.labelServerAddress);
            this.Controls.Add(this.checkSelectInSidebar);
            this.Controls.Add(this.groupLogging);
            this.Controls.Add(this.validationSidebar);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOk);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(1200, 240);
            this.MinimumSize = new System.Drawing.Size(538, 240);
            this.Name = "Options";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "<Options>";
            this.groupLogging.ResumeLayout(false);
            this.groupLogging.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureStatus)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.TextBox textServerAddress;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.CheckBox checkSelectInSidebar;
        private Sdk.Sidebar.AcrolinxSidebar validationSidebar;
        private System.Windows.Forms.GroupBox groupLogging;
        private System.Windows.Forms.Button buttonBrowse;
        private System.Windows.Forms.TextBox textLogDirectory;
        private System.Windows.Forms.Label labelDirectory;
        private System.Windows.Forms.Button buttonConnect;
        private System.Windows.Forms.Label labelServerAddress;
        private System.Windows.Forms.TextBox textStatus;
        private System.Windows.Forms.Label labelStatus;
        private System.Windows.Forms.PictureBox pictureStatus;
    }
}