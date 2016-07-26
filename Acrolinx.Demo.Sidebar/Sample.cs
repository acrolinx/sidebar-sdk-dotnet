/* Copyright (c) 2016 Acrolinx GmbH */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Acrolinx.Sdk.Sidebar.Documents;

namespace Acrolinx.Demo.Sidebar
{
    public partial class Sample : Form
    {
        private int childFormNumber = 0;

        public Sample()
        {
            InitializeComponent();
        }

        private void ShowNewForm(object sender, EventArgs e)
        {
            Form childForm = new Form();
            childForm.MdiParent = this;
            childForm.Text = "Window " + childFormNumber++;
            childForm.Show();
        }

        private void OpenFile(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            openFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = openFileDialog.FileName;
            }
        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            saveFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = saveFileDialog.FileName;
            }
        }

        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CutToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void CascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void TileVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void TileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void ArrangeIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.ArrangeIcons);
        }

        private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
        }

        private void simpleSampleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var simple = new SimpleSample();
            simple.MdiParent = this;
            simple.Show();
        }

        private void multiSampleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var multi = new MultiSample();
            multi.MdiParent = this;
            multi.Show();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var result = openFileDialog.ShowDialog(this);

            if (result == DialogResult.OK)
            {
                foreach (var fileName in openFileDialog.FileNames)
                {
                    try
                    {
                        var simple = new SimpleSample(getFormat(fileName), fileName, File.ReadAllText(fileName));
                        simple.MdiParent = this;
                        simple.Show();
                    }
                    catch (Exception err)
                    {
                        MessageBox.Show(this, "Failed to load '" + fileName + "':" + Environment.NewLine + err.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
            }
        }

        private Format getFormat(string fileName)
        {
            if (fileName.ToLower().EndsWith(".htm") || fileName.ToLower().EndsWith(".html"))
            {
                return Format.HTML;
            }
            if (fileName.ToLower().EndsWith(".xml") || fileName.ToLower().EndsWith(".xhtml"))
            {
                return Format.XML;
            }
            return Format.Text;
        }
    }
}
