using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace Acrolinx.Sdk.Sidebar.Util.About
{
    public partial class AboutBox : Form
    {
        public event EventHandler ComponentValueExtractor;
        private List<Componenets> clientComponents = new List<Componenets>();

        public class Componenets
        {
            public string name;
            public string version;
            public string path;

            public Componenets(string name, string version, string path)
            {
                this.name = name;
                this.version = version;
                this.path = path;
            }
        }

        public List<Componenets> ClientComponents
        {
            get
            {
                return clientComponents;
            }

            set
            {
                clientComponents = value;
            }
        }

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

            foreach (var item in ClientComponents)
            {
                dataGridView.Rows.Add(item.name, item.version, item.path);
            }
        }

        private void SetProductInfo()
        {
            this.osEdition.Text = Util.AssemblyUtil.OSName();

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
            application.Text = Properties.Resources.SDK_ABOUT_LABEL_APPLICATION;
            os.Text = Properties.Resources.SDK_ABOUT_LABEL_OS;
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
            //ClientComponents = new List<string[]>();
            ComponentValueExtractor?.Invoke(sender, e);
            AddAssemblyInfo();
        }
    }
}
