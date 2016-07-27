using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Acrolinx.Demo.Sidebar
{
    public partial class Options : Form
    {
        public Options(string serverAddress)
        {
            InitializeComponent();
            selectInSidebar.Checked = serverAddress == null;

            if (serverAddress != null)
            {
                this.serverAddress.Text = serverAddress;
            }
        }

        public string ServerAddress { get; internal set; }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            if (selectInSidebar.Checked)
            {
                ServerAddress = null;
                return;
            }
            ServerAddress = serverAddress.Text;
        }

        private void selectInSidebar_CheckedChanged(object sender, EventArgs e)
        {
            serverAddress.Enabled = !selectInSidebar.Checked;
        }
    }
}
