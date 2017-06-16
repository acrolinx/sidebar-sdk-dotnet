using Acrolinx.Sdk.Sidebar;
using Acrolinx.Sdk.Sidebar.Properties;
using Acrolinx.Sdk.Sidebar.Util.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Acrolinx.Sdk.Sidebar.Util.Configuration
{
    public partial class Options : Form
    {
        public Options(List<string> serverAddress, bool showSelectInSidebarOption = false)
        {
            InitializeComponent();

            SetLocalizations();

            checkSelectInSidebar.Visible = showSelectInSidebarOption;
            checkSelectInSidebar.Checked = showSelectInSidebarOption && serverAddress == null;

            textLogDirectory.Text = Logging.Logger.Directory;
            buttonBrowse.Enabled = !(String.IsNullOrWhiteSpace(textLogDirectory.Text));

            if (serverAddress != null && serverAddress.Count > 0)
            {
                foreach (var item in serverAddress)
                {
                    serverSelector.Items.Add(item);
                }

                serverSelector.Text = serverAddress[0];
            }
            else
            {
                serverSelector.Text = "";
            }

            validationSidebar.SidebarSourceNotReachable += Validation_SidebarSourceNotReachable;
            validationSidebar.DocumentLoaded += Validation_DocumentLoaded;
            validationSidebar.SidebarLoaded += Validation_SidebarLoaded;
            this.serverSelector.TextChanged += ServerAddressText_TextChanged;

            validateOptionsAndAdjustControlStates();
        }

        private void SetLocalizations()
        {
            groupLogging.Text = Properties.Resources.SDK_OPTION_GROUP_LOGGING;
            labelServerAddress.Text = Properties.Resources.SDK_OPTION_LABEL_SERVERADDRESS;
            buttonConnect.Text = Properties.Resources.SDK_OPTION_BUTTON_VALIDATE;
            labelDirectory.Text = Properties.Resources.SDK_OPTION_LABEL_DIRECTORY;
            checkSelectInSidebar.Text = Properties.Resources.SDK_OPTION_LABEL_SELECTINSIDEBAR;
            labelStatus.Text = Properties.Resources.SDK_OPTION_LABEL_STATUS;
            buttonCancel.Text = Properties.Resources.SDK_OPTION_LABEL_BUTTON_CANCEL;
            buttonOk.Text = Properties.Resources.SDK_OPTION_LABEL_BUTTON_OK;
            this.Text = Properties.Resources.SDK_OPTION_DIALOG_TITLE;
        }

        private void ServerAddressText_TextChanged(object sender, EventArgs e)
        {
            buttonOk.Enabled = false;
        }

        private void buttonConnect_Click(object sender, EventArgs e)
        {
            if (checkSelectInSidebar.Checked)
            {
                ServerAddress = null;
                return;
            }
            else
            {
                lastCheckedServerAddress = serverSelector.Text;
                validate(serverSelector.Text);
            }

            validateOptionsAndAdjustControlStates();
        }

        public string ServerAddress { get; private set; }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            ServerAddress = serverSelector.Text;
        }

        private void selectInSidebar_CheckedChanged(object sender, EventArgs e)
        {
            validateOptionsAndAdjustControlStates();
            if (!checkSelectInSidebar.Checked)
            {
                serverSelector.Focus();
            }
            else
            {
                this.serverSelector.Text = "";
            }
        }

        private void validateOptionsAndAdjustControlStates()
        {
            serverSelector.Enabled = !checkSelectInSidebar.Checked && status != ValidationStatus.Validating;

            buttonConnect.Enabled = !string.IsNullOrWhiteSpace(serverSelector.Text) && status != ValidationStatus.Validating;
            bool isValid = checkSelectInSidebar.Checked || (!string.IsNullOrWhiteSpace(serverSelector.Text) && lastCheckedServerAddress == serverSelector.Text && status == ValidationStatus.Success);
            buttonOk.Enabled = isValid;

            if (isValid)
            {
                textStatus.Text = Properties.Resources.SDK_OPTION_LABEL_STATUS_SUCCESS;
                pictureStatus.Image = Resources.iconConnected;
                this.AcceptButton = buttonOk;
            }
            else
            {
                if (status == ValidationStatus.NotStarted)
                {
                    textStatus.Text = Properties.Resources.SDK_OPTION_LABEL_STATUS_NOT_CONNECTED;
                    pictureStatus.Image = null;
                    this.AcceptButton = buttonConnect;
                }
                if (status == ValidationStatus.Validating)
                {
                    textStatus.Text = Properties.Resources.SDK_OPTION_LABEL_STATUS_CONNECTING;
                    pictureStatus.Image = Resources.iconConnecting;
                    this.AcceptButton = buttonCancel;
                }
                else if (status == ValidationStatus.SidebarFailure)
                {
                    textStatus.Text = Properties.Resources.SDK_OPTION_LABEL_STATUS_SIDEBAR_FAILURE;
                    pictureStatus.Image = Resources.iconDisconnected;
                    this.AcceptButton = buttonConnect;
                }
                else if (status == ValidationStatus.Failure)
                {
                    textStatus.Text = Properties.Resources.SDK_OPTION_LABEL_STATUS_FAILURE;
                    pictureStatus.Image = Resources.iconDisconnected;
                    this.AcceptButton = buttonConnect;
                }
            }
        }

        public enum ValidationStatus
        {
            NotStarted,
            Validating,
            Success,
            SidebarFailure,
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

        private void Validation_DocumentLoaded(object sender, SidebarDocumentLoadedEvenArgs e)
        {
            if (!e.ValidSidebar)
            {
                status = ValidationStatus.SidebarFailure;
                Logger.AcroLog.Error("Loaded page seems to be an IE error page. May be sidebar is not present on server. URL: " + e.Url );
                validateOptionsAndAdjustControlStates();
                serverSelector.Focus();
            }
        }

        private void Validation_SidebarLoaded(object sender, SidebarUrlEvenArgs e)
        {
            status = ValidationStatus.Success;
            validateOptionsAndAdjustControlStates();
            buttonOk.Focus();
        }

        private void Validation_SidebarSourceNotReachable(object sender, EventArgs e)
        {
            status = ValidationStatus.Failure;
            validateOptionsAndAdjustControlStates();
            serverSelector.Focus();
        }

        private string lastCheckedServerAddress;
 
        private void BrowsButton_Click(object sender, EventArgs e)
        {
            try
            {
                System.IO.Directory.CreateDirectory(Logging.Logger.Directory);
                Process proc = Process.Start(Logging.Logger.Directory);
            }catch(Exception exce)
            {
                Logger.AcroLog.Error(exce.Message);
            }
        }

        private void serverSelector_TextChanged(object sender, EventArgs e)
        {
            validateOptionsAndAdjustControlStates();
        }

        private void serverSelector_Enter(object sender, EventArgs e)
        {
            serverSelector.SelectAll();
        }
    }
}
