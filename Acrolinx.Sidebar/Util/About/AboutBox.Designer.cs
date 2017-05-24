namespace Acrolinx.Sdk.Sidebar.Util.About
{
    partial class AboutBox
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutBox));
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.AssemblyName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Version = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Path = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.copyClipBoard = new System.Windows.Forms.Button();
            this.productName = new System.Windows.Forms.Label();
            this.osEdition = new System.Windows.Forms.Label();
            this.editor = new System.Windows.Forms.Label();
            this.application = new System.Windows.Forms.Label();
            this.os = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView
            // 
            this.dataGridView.AllowUserToAddRows = false;
            this.dataGridView.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            this.dataGridView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dataGridView.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dataGridView.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
            this.dataGridView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.AssemblyName,
            this.Version,
            this.Path});
            this.dataGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dataGridView.Location = new System.Drawing.Point(12, 63);
            this.dataGridView.MultiSelect = false;
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.ReadOnly = true;
            this.dataGridView.RowHeadersVisible = false;
            this.dataGridView.ShowCellErrors = false;
            this.dataGridView.ShowCellToolTips = false;
            this.dataGridView.ShowEditingIcon = false;
            this.dataGridView.ShowRowErrors = false;
            this.dataGridView.Size = new System.Drawing.Size(572, 148);
            this.dataGridView.TabIndex = 1;
            this.dataGridView.TabStop = false;
            // 
            // AssemblyName
            // 
            this.AssemblyName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.AssemblyName.HeaderText = "<Name>";
            this.AssemblyName.Name = "AssemblyName";
            this.AssemblyName.ReadOnly = true;
            // 
            // Version
            // 
            this.Version.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Version.HeaderText = "<Version>";
            this.Version.Name = "Version";
            this.Version.ReadOnly = true;
            // 
            // Path
            // 
            this.Path.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Path.HeaderText = "<Path>";
            this.Path.Name = "Path";
            this.Path.ReadOnly = true;
            // 
            // copyClipBoard
            // 
            this.copyClipBoard.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.copyClipBoard.Location = new System.Drawing.Point(470, 233);
            this.copyClipBoard.Name = "copyClipBoard";
            this.copyClipBoard.Size = new System.Drawing.Size(114, 29);
            this.copyClipBoard.TabIndex = 2;
            this.copyClipBoard.Text = "<COPYTOCLIPBOARD>";
            this.copyClipBoard.UseVisualStyleBackColor = true;
            this.copyClipBoard.Click += new System.EventHandler(this.copyClipBoard_Click);
            // 
            // productName
            // 
            this.productName.AutoSize = true;
            this.productName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.productName.Location = new System.Drawing.Point(12, 9);
            this.productName.Name = "productName";
            this.productName.Size = new System.Drawing.Size(84, 13);
            this.productName.TabIndex = 3;
            this.productName.Text = "<ProductName>";
            // 
            // osEdition
            // 
            this.osEdition.AutoSize = true;
            this.osEdition.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.osEdition.Location = new System.Drawing.Point(371, 39);
            this.osEdition.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.osEdition.Name = "osEdition";
            this.osEdition.Size = new System.Drawing.Size(66, 13);
            this.osEdition.TabIndex = 4;
            this.osEdition.Text = "<OSEdition>";
            // 
            // editor
            // 
            this.editor.AutoSize = true;
            this.editor.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.editor.Location = new System.Drawing.Point(83, 39);
            this.editor.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.editor.Name = "editor";
            this.editor.Size = new System.Drawing.Size(46, 13);
            this.editor.TabIndex = 5;
            this.editor.Text = "<Editor>";
            // 
            // application
            // 
            this.application.AutoSize = true;
            this.application.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.application.Location = new System.Drawing.Point(12, 39);
            this.application.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.application.Name = "application";
            this.application.Size = new System.Drawing.Size(71, 13);
            this.application.TabIndex = 6;
            this.application.Text = "<Application>";
            // 
            // os
            // 
            this.os.AutoSize = true;
            this.os.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.os.Location = new System.Drawing.Point(337, 39);
            this.os.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.os.Name = "os";
            this.os.Size = new System.Drawing.Size(34, 13);
            this.os.TabIndex = 7;
            this.os.Text = "<OS>";
            // 
            // AboutBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(596, 285);
            this.Controls.Add(this.os);
            this.Controls.Add(this.application);
            this.Controls.Add(this.editor);
            this.Controls.Add(this.osEdition);
            this.Controls.Add(this.productName);
            this.Controls.Add(this.copyClipBoard);
            this.Controls.Add(this.dataGridView);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AboutBox";
            this.Padding = new System.Windows.Forms.Padding(9);
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Load += new System.EventHandler(this.AboutBox_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn AssemblyName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Version;
        private System.Windows.Forms.DataGridViewTextBoxColumn Path;
        private System.Windows.Forms.Button copyClipBoard;
        private System.Windows.Forms.Label productName;
        private System.Windows.Forms.Label osEdition;
        private System.Windows.Forms.Label editor;
        private System.Windows.Forms.Label application;
        private System.Windows.Forms.Label os;
    }
}
