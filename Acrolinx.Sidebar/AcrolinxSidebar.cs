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
using System.Globalization;

namespace Acrolinx.Sdk.Sidebar
{
    [ToolboxBitmap( typeof(AcrolinxSidebar) , "toolbox.bmp")]
    public partial class AcrolinxSidebar : UserControl, ISidebar
    {
        [Description("Called when the sidebar source was downloaded successfully."), Category("Sidebar")]
        public event SidebarLoadedEventHandler SidebarLoaded;
        [Description("Called when the sidebar has been loaded and is ready to check."), Category("Sidebar")]
        public event SidebarInitFinishedEventHandler InitFinished;
        [Description("Called when the sidebar was not able to download its source. Maybe the URL is wrong or the user is offline..."), Category("Sidebar")]
        public event SidebarSourceNotReachableEventHandler SidebarSourceNotReachable;
        [Description("Called when any kind of html was download. See Eventargs is the downloaded HTML was a valid Acrolinx Sidebar."), Category("Sidebar")]
        public event SidebarDocumentLoadedEventHandler DocumentLoaded;
        [Description("Called when the sidebar has finished a check."), Category("Sidebar")]
        public event SidebarCheckedEventHandler Checked;
        [Description("Called when sidebar.Check() should be called with extracted document."), Category("Sidebar")]
        public event SidebarCheckRequestedEventHandler RequestCheck;
        [Description(""), Category("Sidebar")]
        public event SidebarSelectRangesEventHandler SelectRanges;
        [Description(""), Category("Sidebar")]
        public event SidebarReplaceRangesEventHandler ReplaceRanges;

        private readonly string defaultSidebarUrl = "https://sidebar-classic.acrolinx-cloud.com/v14/prod/index.html?" + System.DateTime.Now.Ticks;
        private readonly string defaultSidebarServerLocation = "/sidebar/v14/index.html?" + System.DateTime.Now.Ticks;

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
            InitParameters.Add("clientLocale", "");
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
            RegisterClientComponent(typeof(WebBrowser).Assembly.GetName().Name + ".browser", "WebBrowser Control Browser", webBrowser.Version.Major + "." + webBrowser.Version.MajorRevision + "." + webBrowser.Version.Minor + "." + webBrowser.Version.MinorRevision,  AcrolinxSidebar.SoftwareComponentCategory.DETAIL);
        }

        public void Start()
        {
            this.Start(null);
        }

        public void Start(string serverAddress)
        {
            if (this.DesignMode)
            {
                return; //no start in design mode
            }

            System.Diagnostics.Trace.Assert(!string.IsNullOrWhiteSpace(ClientSignature), "You do not have specified a client signature. Please ask Acrolinx for a client signature and set the client signature via acrolinxSidebar.ClientSignature or use the .NET-UI-designer.");

            SetDefaults(serverAddress);

            RegisterComponents(Assembly.GetCallingAssembly());

            webBrowser.Navigate(this.SidebarSourceLocation);
        }

        private void SetDefaults(string serverAddress)
        {
            HideServerSelectorIfServerAddressParameterSet(serverAddress);
            SetDefaultForSidebarLocationAndShowServerSelectorIfLocationNotSet(serverAddress);
            SetDefaultClientLocaleIfNotSet();
        }

        private void SetDefaultClientLocaleIfNotSet()
        {
            if (string.IsNullOrWhiteSpace(ClientLocale))
            {
                this.ClientLocale = CultureInfo.CurrentUICulture.TwoLetterISOLanguageName;
            }
        }

        private void ShowServerSelectorIfServerAddressNotSet()
        {
            if (!string.IsNullOrWhiteSpace(this.ServerAddress))
            {
                this.ShowServerSelector = true;
            }
        }

        private void HideServerSelectorIfServerAddressParameterSet(string serverAddress)
        {
            if (!string.IsNullOrWhiteSpace(serverAddress))
            {
                this.ShowServerSelector = false;
            }
        }

        private void SetDefaultForSidebarLocationAndShowServerSelectorIfLocationNotSet(string serverAddress)
        {
            if (string.IsNullOrWhiteSpace(this.SidebarSourceLocation))
            {
                if (!string.IsNullOrWhiteSpace(serverAddress))
                {
                    this.SidebarSourceLocation = getServerAddress(serverAddress) + defaultSidebarServerLocation;
                }
                else
                {
                    this.SidebarSourceLocation = defaultSidebarUrl;
                    ShowServerSelectorIfServerAddressNotSet();
                }
            }
        }

        private string getServerAddress(string serverAddress)
        {
            if (serverAddress.Contains("/sidebar/v"))
            {
                serverAddress.Remove(serverAddress.IndexOf("/sidebar/v"));
            }
            serverAddress = serverAddress.Replace("http://", "");
            if (!serverAddress.Contains(":"))
            {
                serverAddress += ":8031";
            }
            if (!serverAddress.StartsWith("http"))
            {
                serverAddress = "http://" + serverAddress;
            }
            return serverAddress;
        }

        private void GuessMainComponentAndHostApplication(Assembly callingAssembly)
        {
            if (mainComponentSet)
            {
                return;
            }
            System.Diagnostics.Trace.WriteLine("Please call sidebar.RegisterClientComponent(..., SoftwareComponentCategory.MAIN). Sidebar tries to guess the integration name...");

            RegisterClientComponent(callingAssembly, "Acrolinx for " + Application.ProductName, SoftwareComponentCategory.MAIN);

            if (Assembly.GetEntryAssembly() != null && callingAssembly != Assembly.GetEntryAssembly())
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

        [Description("Sets the localization language of the sidebar. Use two letter language codes like 'en' or 'de'. If nothing is set CultureInfo.CurrentUICulture.TwoLetterISOLanguageName will be used."), Category("Sidebar")]
        [DefaultValue(true)]
        public string ClientLocale
        {
            get
            {
                return InitParameters["clientLocale"].Value<string>();
            }
            set
            {
                if (value == null)
                {
                    InitParameters["clientLocale"] = "";
                    return;
                }
                InitParameters["clientLocale"] = value.Trim().ToLower();
            }
        }
        

        [Description("The URL where the sidebar loads its HTML from."), Category("Sidebar")]
        [DefaultValue("")]
        public string SidebarSourceLocation
        {
            get;
            set;
        }

        [Description("Experimental: In case you face focus problems, turn this on."), Category("Sidebar")]
        [DefaultValue(false)]
        public bool FixFocusWorkaround
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

        public void RegisterClientComponent(Assembly assembly, string humanReadableName, SoftwareComponentCategory category)
        {
            Contract.Requires(assembly != null);
            Contract.Requires(humanReadableName != null);
            if (assembly == null)
            {
                return;
            }

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
            foreach (HtmlElement element in webBrowser.Document.GetElementsByTagName("meta"))
            {
                if ("sidebar-revision".Equals(("" + element.GetAttribute("name")).ToLower()))
                {
                    sidebarRevisionFound = true;
                    break;
                }
            }

            if (!sidebarRevisionFound)
            {
                System.Diagnostics.Trace.WriteLine("Could not find sidebar at URL: " + e.Url);

                string internalUrl = GetInternalUrl();
                
                if (internalUrl.StartsWith("res://ieframe.dll/"))
                {
                    System.Diagnostics.Trace.WriteLine("Loaded page seems to be an IE error page. URL: " + e.Url + " / " + internalUrl);

                    SidebarSourceNotReachable?.Invoke(this, new SidebarUrlEvenArgs(e.Url));
                    return;
                }
            }

            labelImage.Visible = false;

            if (FixFocusWorkaround && webBrowser.Document?.Body != null)
            {
                webBrowser.Document.Body.MouseEnter += MouseEnteredSidebar;
                webBrowser.Document.Body.MouseLeave += MouseLeftSidebar;
                webBrowser.Document.Body.MouseDown += MouseDownInSidebar;
            }

            DocumentLoaded?.Invoke(this, new SidebarDocumentLoadedEvenArgs(sidebarRevisionFound, e.Url));
            if (!sidebarRevisionFound)
            {
                return;
            }

            if (webBrowser.ObjectForScripting == null)
            {
                acrolinxPlugin = new AcrolinxPlugin(webBrowser, this);
                webBrowser.ObjectForScripting = acrolinxPlugin;
                acrolinxPlugin.OnAfterObjectSet();
            }

            SidebarLoaded?.Invoke(this, new SidebarUrlEvenArgs(e.Url));
        }

        private void MouseLeftSidebar(object sender, HtmlElementEventArgs e)
        {
            mouseEnteredSidebar = false;
        }

        private void MouseDownInSidebar(object sender, HtmlElementEventArgs e)
        {
            if (!mouseEnteredSidebar)
            {
                return;
            }
            mouseEnteredSidebar = false;

            FixFocusTimer.Enabled = true;
        }

        private bool mouseEnteredSidebar = false;
        private void MouseEnteredSidebar(object sender, HtmlElementEventArgs e)
        {
            mouseEnteredSidebar = true;
        }

        private string GetInternalUrl()
        {
            return "" + webBrowser?.Document?.Window?.Url?.AbsoluteUri;
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

        private void OnFixFocusTimerTick(object sender, EventArgs e)
        {
            if(webBrowser.Document?.ActiveElement != null)
            {
                webBrowser.Focus();
                webBrowser.Document.ActiveElement.Focus();
            }
            FixFocusTimer.Enabled = false;
        }
    }
}
