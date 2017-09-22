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
using Acrolinx.Sdk.Sidebar.Util.Logging;
using System.Dynamic;
using System.Reflection;
using Microsoft.CSharp.RuntimeBinder;
using System.Linq.Expressions;
using Newtonsoft.Json.Linq;
using System.Globalization;
using Acrolinx.Sdk.Sidebar.Storage;
using System.IO;

namespace Acrolinx.Sdk.Sidebar
{
    [ToolboxBitmap(typeof(AcrolinxSidebar), "toolbox.bmp")]
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

        public IAcrolinxStorage Storage
        {
            get;
            private set;
        }

        public JObject InitParameters
        {
            get;
            private set;
        }

        public AcrolinxSidebar(IAcrolinxStorage acroStorage = null)
        {
            Storage = (acroStorage != null) ? acroStorage : RegistryAcrolinxStorage.Instance;

            InitParameters = new JObject();
            InitParameters.Add("showServerSelector", true);
            InitParameters.Add("serverAddress", "");
            InitParameters.Add("clientSignature", "");
            InitParameters.Add("readOnlySuggestions", false);
            InitParameters.Add("clientLocale", "");
            InitParameters.Add("logFileLocation", Logger.Directory);
            InitParameters.Add("clientComponents", new JArray());
            InitParameters.Add("minimumSidebarVersion", "14.5.0");

            InitializeComponent();

            webBrowser.IsWebBrowserContextMenuEnabled = false;
        }

        private void RegisterComponents(Assembly callingAssembly)
        {
            GuessMainComponentAndHostApplication(callingAssembly);

            RegisterClientComponent(typeof(AcrolinxSidebar).Assembly, "Acrolinx Sidebar .NET SDK", SoftwareComponentCategory.DEFAULT);
            RegisterClientComponent(typeof(String).Assembly, ".NET Framework", AcrolinxSidebar.SoftwareComponentCategory.DETAIL);
            RegisterClientComponent(typeof(JObject).Assembly, "Json.NET", AcrolinxSidebar.SoftwareComponentCategory.DETAIL);
            RegisterClientComponent(typeof(log4net.Core.ILogger).Assembly, "log4net", AcrolinxSidebar.SoftwareComponentCategory.DETAIL);
            RegisterClientComponent(typeof(WebBrowser).Assembly, "WebBrowser Control", AcrolinxSidebar.SoftwareComponentCategory.DETAIL);
            RegisterClientComponent(typeof(WebBrowser).Assembly.GetName().Name + ".browser", "WebBrowser Control Browser", webBrowser.Version.Major + "." + webBrowser.Version.MajorRevision + "." + webBrowser.Version.Minor + "." + webBrowser.Version.MinorRevision, AcrolinxSidebar.SoftwareComponentCategory.DETAIL);
            RegisterClientComponent("osInfo", Util.AssemblyUtil.OSInfo()["osName"], Util.AssemblyUtil.OSInfo()["version"], AcrolinxSidebar.SoftwareComponentCategory.DEFAULT);
            RegisterClientComponent("editor", Util.AssemblyUtil.AppInfo()["productName"], Util.AssemblyUtil.AppInfo()["version"], AcrolinxSidebar.SoftwareComponentCategory.DEFAULT);
        }

        public void Start()
        {
            RegisterComponents(Assembly.GetCallingAssembly());
            this.Start(null);
        }

        /// <summary>
        /// Prefered way to start sidebar is Start(), this will enable server selector feature by default
        /// </summary>
        public void Start(string serverAddress)
        {
            if (this.DesignMode)
            {
                return; //no start in design mode
            }

            System.Diagnostics.Trace.Assert(!string.IsNullOrWhiteSpace(ClientSignature), "You do not have specified a client signature. Please ask Acrolinx for a client signature and set the client signature via acrolinxSidebar.ClientSignature or use the .NET-UI-designer.");

            SetDefaults(serverAddress);


            AutoScaleDimensions = new SizeF(96F, 96F);

            webBrowser.Navigate(GetStartPageURL());
        }

        private void SetDefaults(string serverAddress)
        {
            ShowHideServerSelectorIfServerAddressParameterSet(serverAddress);
            SetDefaultClientLocaleIfNotSet();
        }

        private void SetDefaultClientLocaleIfNotSet()
        {
            if (string.IsNullOrWhiteSpace(ClientLocale))
            {
                this.ClientLocale = CultureInfo.CurrentUICulture.TwoLetterISOLanguageName;
            }
        }

        private string GetStartPageURL()
        {
            var assemblyLocation = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\Acrolinx.Startpage.dll";

            if (File.Exists(assemblyLocation))
            {
                return @"res://" + assemblyLocation + "//index.html";
            }
            Logger.AcroLog.Error("Failed to locate " + assemblyLocation);
            return null;
        }

        private string getServerAddress(string serverAddress)
        {
            serverAddress = serverAddress.ToLower().Trim();
            serverAddress = serverAddress.Replace("http://", "");
            if (!serverAddress.Contains(":"))
            {
                serverAddress += ":8031";
            }
            if (!serverAddress.StartsWith("http"))
            {
                if (serverAddress.Contains(":443"))
                {
                    serverAddress = "https://" + serverAddress;
                }
                else
                {
                    serverAddress = "http://" + serverAddress;
                }
            }
            return serverAddress;
        }

        private void GuessMainComponentAndHostApplication(Assembly callingAssembly)
        {
            if (mainComponentSet)
            {
                return;
            }
            Logger.AcroLog.Info("Please call sidebar.RegisterClientComponent(..., SoftwareComponentCategory.MAIN). Sidebar tries to guess the integration name...");

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

        [Description("The integration specifies the minimum Acrolinx Sidebar version required."), Category("Sidebar")]
        [DefaultValue("")]
        public string MinimumSidebarVersion
        {
            get
            {
                return InitParameters["minimumSidebarVersion"].Value<string>();
            }
            set
            {
                InitParameters["minimumSidebarVersion"] = value;
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

        [Description("Experimental: In case you face focus problems, turn this on."), Category("Sidebar")]
        [DefaultValue(false)]
        public bool FixFocusWorkaround
        {
            get;
            set;
        }

        [Description("Sets sidebar to show suggestions in read-only mode."), Category("Sidebar")]
        [DefaultValue(false)]
        public bool ReadOnlySuggestions
        {
            get
            {
                return InitParameters["readOnlySuggestions"].Value<bool>();
            }

            set
            {
                InitParameters["readOnlySuggestions"] = value;
            }
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
            Logger.AcroLog.Debug("Content: " + document.Content);

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
            Logger.AcroLog.Debug("eval(" + code + ")");
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
            ReplaceRanges?.Invoke(this, new MatchesWithReplacementEventArgs(checkId, matches));
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
            Logger.AcroLog.Info("Sidebar navigated to: " + e.Url);

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
                Logger.AcroLog.Warn("Could not find sidebar at URL: " + e.Url);

                string internalUrl = GetInternalUrl();

                if (internalUrl.StartsWith("res://ieframe.dll/"))
                {
                    Logger.AcroLog.Error("Loaded page seems to be an IE error page. URL: " + e.Url + " / " + internalUrl);

                    SidebarSourceNotReachable?.Invoke(this, new SidebarUrlEvenArgs(e.Url));
                    return;
                }
                Logger.AcroLog.Error("The server doesn't seem to be responding. Is the address (" + internalUrl + ") correct?");
                Logger.AcroLog.Error("A communication error occurred may be connection refused due to network problem.");
            }

            labelImage.Visible = false;

            if (FixFocusWorkaround && webBrowser.Document?.Body != null)
            {
                webBrowser.Document.Body.MouseEnter += MouseEnteredSidebar;
                webBrowser.Document.Body.MouseLeave += MouseLeftSidebar;
                webBrowser.Document.Body.MouseUp += MouseUpInSidebar;
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

        private void MouseUpInSidebar(object sender, HtmlElementEventArgs e)
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

            Logger.AcroLog.Debug("InvalidateRanges: " + string.Join(", ", matches));

            JArray invalidRanges = new JArray();

            foreach (Match match in matches)
            {
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
            if (webBrowser.Document?.ActiveElement != null)
            {
                webBrowser.Focus();
                webBrowser.Document.ActiveElement.Focus();
            }
            FixFocusTimer.Enabled = false;
        }

        private void ShowHideServerSelectorIfServerAddressParameterSet(string serverAddress)
        {
            if (!string.IsNullOrWhiteSpace(serverAddress))
            {
                this.ServerAddress = serverAddress;
                this.ShowServerSelector = false;
            }
            else
            {
                this.ShowServerSelector = true;
            }
        }
    }
}
