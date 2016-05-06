/* Copyright (c) 2016 Acrolinx GmbH */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics.Contracts;
using Acrolinx.Sdk.Sidebar.Documents;
using System.Dynamic;
using System.Reflection;
using Microsoft.CSharp.RuntimeBinder;
using System.Linq.Expressions;
using Newtonsoft.Json.Linq;
namespace Acrolinx.Sdk.Sidebar
{
    [ToolboxBitmap( typeof(AcrolinxSidebar) , "toolbox.bmp")]
    public partial class AcrolinxSidebar : UserControl, ISidebar
    {
        [Description("Called when the sidebar has been loaded and is ready to check."), Category("Sidebar")]
        public event SidebarInitFinishedEventHandler InitFinished;
        [Description("Called when the sidebar was not able to download its source. Maybe the URL is wrong or the user is offline..."), Category("Sidebar")]
        public event SidebarSourceNotReachableEventHandler SidebarSourceNotReachable;
        [Description("Called when the sidebar has finished a check."), Category("Sidebar")]
        public event SidebarCheckedEventHandler Checked;
        [Description("Called when sidebar.Check() should be called with extracted document."), Category("Sidebar")]
        public event SidebarCheckRequestedEventHandler RequestCheck;
        [Description(""), Category("Sidebar")]
        public event SidebarSelectRangesEventHandler SelectRanges;
        [Description(""), Category("Sidebar")]
        public event SidebarReplaceRangesEventHandler ReplaceRanges;

        private readonly string defaultSidebarUrl = "https://acrolinx-sidebar-classic.s3.amazonaws.com/v14/prod/index.html?" + System.DateTime.Now.Ticks;

        public JObject InitParameters
        {
            get;
            private set;
        }

        public AcrolinxSidebar()
        {
            InitParameters = new JObject();
            InitParameters.Add("showServerSelector", true);
            InitParameters.Add("serverAddress", "");
            InitParameters.Add("clientSignature", "");
            InitParameters.Add("clientComponents", new JArray());

            InitializeComponent();

            webBrowser.IsWebBrowserContextMenuEnabled = false;
        }

        private void RegisterComponents(Assembly callingAssembly)
        {           
            GuessMainComponentAndHostApplication(callingAssembly);

            RegisterClientComponent(typeof(AcrolinxSidebar).Assembly, "Acrolinx.NET", SoftwareComponentCategory.DEFAULT);
            RegisterClientComponent(typeof(String).Assembly, ".NET Framework", AcrolinxSidebar.SoftwareComponentCategory.DETAIL);
            RegisterClientComponent(typeof(JObject).Assembly, "Json.NET", AcrolinxSidebar.SoftwareComponentCategory.DETAIL);
            RegisterClientComponent(typeof(WebBrowser).Assembly, "WebBrowser Control", AcrolinxSidebar.SoftwareComponentCategory.DETAIL);
        }

        public void Start()
        {
            if (this.DesignMode)
            {
                return; //no start in design mode
            }

            System.Diagnostics.Trace.Assert(!string.IsNullOrWhiteSpace(ClientSignature), "You do not have specified a client signature. Please ask Acrolinx for a client signature and set the client signature via acrolinxSidebar.ClientSignature or use the .NET-UI-designer.");

            if(string.IsNullOrWhiteSpace(this.SidebarSourceLocation))
            {
                this.SidebarSourceLocation = defaultSidebarUrl;
                this.ShowServerSelector = true;
            }
            
            RegisterComponents(Assembly.GetCallingAssembly());

            webBrowser.Navigate(this.SidebarSourceLocation);
        }

        private void GuessMainComponentAndHostApplication(Assembly callingAssembly)
        {
            if (mainComponentSet)
            {
                return;
            }
            System.Diagnostics.Trace.WriteLine("Please call sidebar.RegisterClientComponent(..., SoftwareComponentCategory.MAIN). Sidebar tries to guess the integration name...");

            RegisterClientComponent(callingAssembly, "Acrolinx for " + Application.ProductName, SoftwareComponentCategory.MAIN);

            if (callingAssembly != Assembly.GetEntryAssembly())
            {
                RegisterClientComponent(Assembly.GetEntryAssembly(), Application.ProductName, SoftwareComponentCategory.DEFAULT);
            }
        }

        private AcrolinxPlugin acrolinxPlugin = null;

        [Description("The integration specific clientSignature. To get one, ask your Acrolinx contact."), Category("Sidebar")]
        [DefaultValue("")]
        public string ClientSignature
        {
            get
            {
                return InitParameters["clientSignature"].Value<string>();
            }
            set
            {
                InitParameters["clientSignature"] = value;
            }
        } 
        
        [Description("The address of the server the sidebar talks to. default value is '' which means the base URL of the host that it runs from"), Category("Sidebar")]
        [DefaultValue("")]
        public string ServerAddress
        {
            get
            {
                return InitParameters["serverAddress"].Value<string>();
            }
            set
            {
                InitParameters["serverAddress"] = value;
            }
        }
        [Description("Enables user to manually change the serverAddress on the sign in screen"), Category("Sidebar")]
        [DefaultValue(true)]
        public bool ShowServerSelector
        {
            get
            {
                return InitParameters["showServerSelector"].Value<bool>();
            }
            set
            {
                InitParameters["showServerSelector"] = value;
            }
        }
        [Description("The URL where the sidebar loads its HTML from."), Category("Sidebar")]
        [DefaultValue("")]
        public string SidebarSourceLocation
        {
            get;
            set;
        }

        public enum SoftwareComponentCategory
        {

            /// <summary>
            /// There should be exactly one MAIN component.
            /// This information is used to identify your client on the server.
            /// Version information about this components might be displayed more prominently.
            /// </summary>
            MAIN,

            /// <summary>
            /// Version information about such components are displayed in the about dialog.
            /// </summary>
        
            DEFAULT,
            /// <summary>
            /// Version information about such components are displayed in the detail section of the about dialog or not at all.
            /// </summary>
            DETAIL
        }

        public void RegisterClientComponent(Assembly assembly, string humanReadableName, SoftwareComponentCategory category){
            Contract.Requires(assembly != null);
            Contract.Requires(humanReadableName != null);

            var name = assembly.GetName();
            RegisterClientComponent(name.Name, humanReadableName, name.Version.ToString(), category);
        }

        private bool mainComponentSet = false;
        public void RegisterClientComponent(string id, string name, string version, SoftwareComponentCategory category)
        {
            Contract.Requires(id != null);

            mainComponentSet |= category == SoftwareComponentCategory.MAIN;

            var clientComponents = InitParameters["clientComponents"].Value<JArray>();

            var component = new JObject();
            component.Add("id", "dotnet." + id.ToLower());
            component.Add("name", name);
            component.Add("version", version);
            component.Add("category", category.ToString().ToUpper());
            clientComponents.Add(component);
        }

        public String Check(IDocument document)
        {
            acrolinxPlugin.Document = document;
            System.Diagnostics.Trace.WriteLine("Content: " + document.Content);

            var code = "new function(){var c = window.external.getContent(); "
                + "console.log('Content: ' + c); "
                + "return acrolinxSidebar.checkGlobal(c, {inputFormat:'" + document.Format.ToString().ToUpper() + "', requestDescription:{documentReference: '" 
                + document.Reference.Replace("\\", "\\\\").Replace("'", "\\'").Replace("\n", "").Replace("\r", "") + "'}})}();";

            dynamic check = Eval(code);
            string checkId = check.checkId;
            return checkId;
        }

        internal dynamic Eval(string code)
        {
            System.Diagnostics.Trace.WriteLine("eval(" + code + ")");
            dynamic result = webBrowser.Document.InvokeScript("eval", new object[] { code });
            return result;
        }

        internal void FireRequestCheck()
        {
            RequestCheck?.Invoke(this, new EventArgs());
        }

        internal void FireInitFinished()
        {
            InitFinished?.Invoke(this, new EventArgs());
        }


        internal void FireSelectRanges(string checkId, IEnumerable<Match> matches)
        {
            Contract.Requires(checkId != null);
            Contract.Requires(matches != null);
            SelectRanges?.Invoke(this, new MatchesEventArgs(checkId, matches));
        }

        internal void FireReplaceRanges(string checkId, IEnumerable<MatchWithReplacement> matches)
        {
            Contract.Requires(checkId != null);
            Contract.Requires(matches != null);
            ReplaceRanges?.Invoke(this, new MatchesWithReplacementEventArgs(checkId , matches));
        }

        internal void FireChecked(string checkId, Range range)
        {
            Contract.Requires(checkId != null);
            Contract.Requires(range != null);
            Checked?.Invoke(this, new CheckedEventArgs(checkId, range));
        }

        private void webBrowser_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            webBrowser.ScriptErrorsSuppressed = true;
            webBrowser.IsWebBrowserContextMenuEnabled = false;
        }

        private void webBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            System.Diagnostics.Trace.WriteLine("Sidebar navigated to: " + e.Url);

            bool sidebarRevisionFound = false;
            foreach(HtmlElement element in webBrowser.Document.GetElementsByTagName("meta")){
                if ("sidebar-revision".Equals(("" + element.GetAttribute("name")).ToLower()))
                {
                    sidebarRevisionFound = true;
                    break;
                }
            }
            if (!sidebarRevisionFound)
            {
                System.Diagnostics.Trace.WriteLine("Connection problem: could not load the sidebar: " + e.Url);
                SidebarSourceNotReachable?.Invoke(this, new EventArgs());
                return;
            }

            if (webBrowser.ObjectForScripting == null)
            {
                acrolinxPlugin = new AcrolinxPlugin(webBrowser, this);
                webBrowser.ObjectForScripting = acrolinxPlugin;
                acrolinxPlugin.OnAfterObjectSet();
            }

            labelImage.Visible = false;
        }

        public void InvalidateRanges(String checkId, IReadOnlyList<Match> matches)
        {
            Contract.Requires(matches != null);
            Contract.Requires(!string.IsNullOrWhiteSpace(checkId));

            System.Diagnostics.Trace.WriteLine("InvalidateRanges: " + string.Join(", ", matches));

            JArray invalidRanges = new JArray();

            foreach(Match match in matches){
                JObject invalidDocumentPart = new JObject();
                invalidDocumentPart.Add("checkId", checkId);
                invalidDocumentPart.Add("range", new JArray(match.Range.Start, match.Range.End));
                invalidRanges.Add(invalidDocumentPart);
            }

            var code = "new function(){ return acrolinxSidebar.invalidateRanges(" + invalidRanges.ToString() + ")}();";
            Eval(code);

        }

    }
}
