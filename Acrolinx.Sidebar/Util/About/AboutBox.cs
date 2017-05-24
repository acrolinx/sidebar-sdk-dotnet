using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Reflection;
using System.Windows.Forms;

namespace Acrolinx.Sdk.Sidebar.Util.About
{
    public partial class AboutBox : Form
    {
        public EventHandler<EventArgs> CallBack;
        private List<string[]> clientComponents;
        public AboutBox()
        {
            InitializeComponent();
            SetLocalizations();
            SetProductInfo();
            
        }

        private void AddAssemblyInfo()
        {
            this.dataGridView.Rows.Add(Application.ProductName, Application.ProductVersion, Application.StartupPath);
            AssemblyUtil abtSDK = new AssemblyUtil((typeof(Acrolinx.Sdk.Sidebar.AcrolinxSidebar).Assembly));
            this.dataGridView.Rows.Add(abtSDK.AssemblyProduct, abtSDK.AssemblyVersion, abtSDK.AssemblyPath);
            AssemblyUtil abtNewtonsoft = new AssemblyUtil(Assembly.Load("Newtonsoft.Json"));
            this.dataGridView.Rows.Add(abtNewtonsoft.AssemblyProduct, abtNewtonsoft.AssemblyVersion, abtNewtonsoft.AssemblyPath);

            foreach (var item in clientComponents)
            {
                this.dataGridView.Rows.Add(item[0], item[1], item[2]);
            }
        }

        private void SetProductInfo()
        {
            var name = (from t in new ManagementObjectSearcher("SELECT Caption FROM Win32_OperatingSystem").Get().Cast<ManagementObject>() select t.GetPropertyValue("Caption")).FirstOrDefault();
            this.os.Text = name != null ? name.ToString() : "Unknown";

            var str = Assembly.GetExecutingAssembly().GetName().ProcessorArchitecture;
            FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);

            var architecture = (IntPtr.Size * 8).ToString() + " bit";
            this.editor.Text = fvi.FileDescription + " " + architecture;

            this.productName.Text = Properties.Resources.SDK_ABOUT_LABEL_PRODUCTNAME + fvi.FileDescription.Split(' ').Last();
        }

        private void SetLocalizations()
        {
            copyClipBoard.Text = Properties.Resources.SDK_ABOUT_LABEL_COPYTOCLIPBOARD;
            this.Text = Properties.Resources.SDK_ABOUT_DIALOG_TITLE;
            AssemblyName.HeaderText = Properties.Resources.SDK_ABOUT_LABEL_ASSEMBLYNAME;
            Path.HeaderText = Properties.Resources.SDK_ABOUT_LABEL_PATH;
            Version.HeaderText = Properties.Resources.SDK_ABOUT_LABEL_VERSION;            
        }

        private void copyClipBoard_Click(object sender, EventArgs e)
        {
            string clipboardContent = "";
            foreach (DataGridViewRow dataGridRow in dataGridView.Rows)
            {
                foreach (DataGridViewCell cell in  dataGridRow.Cells)
                {
                    clipboardContent += cell.Value + "\t";
                }
                clipboardContent += "\r\n";
            }
            Clipboard.SetText(clipboardContent);
        }

        private void AboutBox_Load(object sender, EventArgs e)
        {
            clientComponents = new List<string[]>();
            CallBack?.Invoke(this.clientComponents, new EventArgs());
            AddAssemblyInfo();
        }
    }
}
