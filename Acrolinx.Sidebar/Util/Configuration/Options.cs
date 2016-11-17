using Acrolinx.Sdk.Sidebar;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Acrolinx.Sdk.Sidebar.Util.Configuration
{
    public partial class Options : Form
    {
        public Options(string serverAddress, bool defaultSidebar = true)
        {
            InitializeComponent();

            LoggingGroupBox.Text = Properties.Resources.SDK_OPTION_GROUP_SERVERADDRESS;
            ServerAddrGroupBox.Text = Properties.Resources.SDK_OPTION_GROUP_LOGGING;
            ValidateButton.Text = Properties.Resources.SDK_OPTION_BUTTON_VALIDATE;
            DirectoryLabel.Text = Properties.Resources.SDK_OPTION_GROUP_LOGGING;
            selectInSidebar.Text = Properties.Resources.SDK_OPTION_LABEL_SELECTINSIDEBAR;

            selectInSidebar.Visible = defaultSidebar;

            LogDirectory.Text = Logging.Logger.DirPath;
            BrowsButton.Enabled = !(String.IsNullOrWhiteSpace(LogDirectory.Text));

            if (defaultSidebar)
            {
                selectInSidebar.Checked = serverAddress == null;
            }
            else
            {
                selectInSidebar.Checked = false;
            }
            if (serverAddress != null)
            {
                this.serverAddress.Text = serverAddress;
            }

            validationSidebar.SidebarSourceNotReachable += Validation_SidebarSourceNotReachable;
            validationSidebar.DocumentLoaded += Validation_SidebarLoaded;
            ValidateButton.Click += ValidateButton_Click;
            this.serverAddress.TextChanged += ServerAddressText_TextChanged;

            validateOptionsAndAdjustControlStates();
        }

        private void ServerAddressText_TextChanged(object sender, EventArgs e)
        {
            buttonOk.Enabled = false;
        }

        private void ValidateButton_Click(object sender, EventArgs e)
        {
            if (selectInSidebar.Checked)
            {
                ServerAddress = null;
                return;
            }
            else
            {
                if (lastCheckedServerAddress != serverAddress.Text)
                {
                    lastCheckedServerAddress = serverAddress.Text;
                    validate(serverAddress.Text);
                }
                else
                {
                    status = ValidationStatus.Success;
                }
            }

            validateOptionsAndAdjustControlStates();
        }

        public string ServerAddress { get; private set; }

        public string LogDirectoryPath { get; private set; }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            ServerAddress = serverAddress.Text;
            LogDirectoryPath = LogDirectory.Text;
        }

        private void selectInSidebar_CheckedChanged(object sender, EventArgs e)
        {
            validateOptionsAndAdjustControlStates();
            if(!selectInSidebar.Checked)
            {
                serverAddress.Focus();
            }
            else
            {
                this.serverAddress.Text = "";
            }
        }

        private void validateOptionsAndAdjustControlStates()
        {
            serverAddress.Enabled = !selectInSidebar.Checked;

            bool isValid = selectInSidebar.Checked || (!string.IsNullOrWhiteSpace(serverAddress.Text) && status == ValidationStatus.Success);
            buttonOk.Enabled = isValid;

            if (isValid || status != ValidationStatus.Failure)
            {
                errorProvider.Clear();
            }

            if (!isValid && status == ValidationStatus.Failure)
            {
                errorProvider.SetError(serverAddress, "The server doesn't seem to be responding. Is the address correct?");
            }
        }

        public enum ValidationStatus
        {
            NotStarted,
            Validating,
            Success,
            Failure
        }
        private ValidationStatus status = ValidationStatus.NotStarted;

        private void validate(string serverAddress)
        {
            validationSidebar.SidebarSourceLocation = null;
            status = ValidationStatus.Validating;
            validationSidebar.Start(serverAddress);
            validateOptionsAndAdjustControlStates();
        }

        private void Validation_SidebarLoaded(object sender, SidebarDocumentLoadedEvenArgs e)
        {
            status = ValidationStatus.Success;
            validateOptionsAndAdjustControlStates();
        }

        private void Validation_SidebarSourceNotReachable(object sender, EventArgs e)
        {
            status = ValidationStatus.Failure;
            validateOptionsAndAdjustControlStates();
        }

        private string lastCheckedServerAddress;
 
        private void BrowsButton_Click(object sender, EventArgs e)
        {
            try
            {
                System.IO.Directory.CreateDirectory(Logging.Logger.DirPath);
                Process proc = Process.Start(Logging.Logger.DirPath);
            }catch(Exception exce)
            {
                Trace.WriteLine(exce.Message);
            }
        }
    }
}
