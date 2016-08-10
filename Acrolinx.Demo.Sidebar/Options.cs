using Acrolinx.Sdk.Sidebar;
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

            validationSidebar.SidebarSourceNotReachable += Validation_SidebarSourceNotReachable;
            validationSidebar.SidebarLoaded += Validation_SidebarLoaded;

            validateOptionsAndAdjustControlStates();
        }

        public string ServerAddress { get; private set; }

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
            validateOptionsAndAdjustControlStates();
            if(!selectInSidebar.Checked)
            {
                serverAddress.Focus();
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

        private void Validation_SidebarLoaded(object sender, EventArgs e)
        {
            status = ValidationStatus.Success;
            validateOptionsAndAdjustControlStates();
        }

        private void Validation_SidebarSourceNotReachable(object sender, EventArgs e)
        {
            status = ValidationStatus.Failure;
            validateOptionsAndAdjustControlStates();
        }

        private string lastEnteredServerAddress;
        private string lastCheckedServerAddress;
        private void serverAddressValidationTimer_Tick(object sender, EventArgs e)
        {
            if (!selectInSidebar.Checked)
            {
                if (lastEnteredServerAddress != serverAddress.Text)
                {
                    lastEnteredServerAddress = serverAddress.Text;
                    return; //Wait for changes
                }
                if (lastCheckedServerAddress != serverAddress.Text)
                {
                    lastCheckedServerAddress = serverAddress.Text;
                    validate(serverAddress.Text);
                }
            }
        }
    }
}
