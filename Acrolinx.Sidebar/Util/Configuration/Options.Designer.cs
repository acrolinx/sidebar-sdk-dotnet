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
            this.serverAddressValidationTimer = new System.Windows.Forms.Timer(this.components);
            this.validationSidebar = new Acrolinx.Sdk.Sidebar.AcrolinxSidebar();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonOk
            // 
            this.buttonOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOk.Location = new System.Drawing.Point(353, 45);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(75, 23);
            this.buttonOk.TabIndex = 0;
            this.buttonOk.Text = "&OK";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // serverAddress
            // 
            this.serverAddress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.serverAddress.Enabled = false;
            this.serverAddress.Location = new System.Drawing.Point(12, 12);
            this.serverAddress.Name = "serverAddress";
            this.serverAddress.Size = new System.Drawing.Size(473, 20);
            this.serverAddress.TabIndex = 2;
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(435, 45);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 3;
            this.buttonCancel.Text = "&Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // selectInSidebar
            // 
            this.selectInSidebar.AutoSize = true;
            this.selectInSidebar.Checked = true;
            this.selectInSidebar.CheckState = System.Windows.Forms.CheckState.Checked;
            this.selectInSidebar.Location = new System.Drawing.Point(12, 45);
            this.selectInSidebar.Name = "selectInSidebar";
            this.selectInSidebar.Size = new System.Drawing.Size(106, 17);
            this.selectInSidebar.TabIndex = 4;
            this.selectInSidebar.Text = "Select in Sidebar";
            this.selectInSidebar.UseVisualStyleBackColor = true;
            this.selectInSidebar.CheckedChanged += new System.EventHandler(this.selectInSidebar_CheckedChanged);
            // 
            // serverAddressValidationTimer
            // 
            this.serverAddressValidationTimer.Enabled = true;
            this.serverAddressValidationTimer.Interval = 1000;
            this.serverAddressValidationTimer.Tick += new System.EventHandler(this.serverAddressValidationTimer_Tick);
            // 
            // validationSidebar
            // 
            this.validationSidebar.ClientLocale = "";
            this.validationSidebar.ClientSignature = "123";
            this.validationSidebar.Location = new System.Drawing.Point(12, 58);
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
            this.ClientSize = new System.Drawing.Size(522, 76);
            this.Controls.Add(this.validationSidebar);
            this.Controls.Add(this.selectInSidebar);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.serverAddress);
            this.Controls.Add(this.buttonOk);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Options";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Options";
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.TextBox serverAddress;
        private System.Windows.Forms.ErrorProvider errorProvider;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.CheckBox selectInSidebar;
        private System.Windows.Forms.Timer serverAddressValidationTimer;
        private Sdk.Sidebar.AcrolinxSidebar validationSidebar;
    }
}