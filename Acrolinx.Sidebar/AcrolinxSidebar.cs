/* Copyright (c) 2016 Acrolinx GmbH */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.Diagnostics.Contracts;
using Acrolinx.Sdk.Sidebar.Documents;
using Acrolinx.Sdk.Sidebar.Util.Logging;
using System.Reflection;
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
        [Description("Called when the sidebar has embed check information"), Category("Sidebar")]
        public event SidebarProcessEmbedCheckDataEventHandler ProcessEmbedCheckData;
        [Description("Called when the sidebar to open a web browser (sign in / Scorecard)"), Category("Sidebar")]
        public event SidebarOpenBrowserEventHandler OpenBrowser;
        public IAcrolinxStorage Storage
        {
            get;
            private set;
        }

        [Description("Turns check selection support on. In case request check has selections set to true, you have to specify the selected offsets in the check document."), Category("Sidebar")]
        public bool SupportCheckSelection
        {
            get
            {
                var checkSelection = Supported["checkSelection"];
                if (checkSelection == null)
                {
                    return false;
                }
                return checkSelection.Value<bool>();
            }
            set
            {
                Supported["checkSelection"] = value;
            }
        }

        public JObject Supported
        {
            get
            {
                return InitParameters["supported"] as JObject;
            }
            set
            {
                InitParameters.Remove("supported");
                InitParameters.Add("supported", value);
            }
        }

        public JObject InitParameters
        {
            get;
            private set;
        }

        public AcrolinxSidebar() : this(null)
        {
        }

        public AcrolinxSidebar(IAcrolinxStorage acroStorage)
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
            InitParameters.Add("supported", new JObject());

            InitializeComponent();

            webBrowser.IsWebBrowserContextMenuEnabled = false;
        }

        private void RegisterComponents(Assembly callingAssembly)
        {
            Logger.AcroLog.Info("Registering Sidebar Components");
            GuessMainComponentAndHostApplication(callingAssembly);

            RegisterClientComponent(typeof(AcrolinxSidebar).Assembly, "Acrolinx Sidebar .NET SDK", SoftwareComponentCategory.DEFAULT);
            RegisterClientComponent(typeof(String).Assembly, ".NET Framework", AcrolinxSidebar.SoftwareComponentCategory.DETAIL);
            RegisterClientComponent(typeof(JObject).Assembly, "Json.NET", AcrolinxSidebar.SoftwareComponentCategory.DETAIL);
            RegisterClientComponent(typeof(log4net.Core.ILogger).Assembly, "log4net", AcrolinxSidebar.SoftwareComponentCategory.DETAIL);
            RegisterClientComponent(typeof(WebBrowser).Assembly, "WebBrowser Control", AcrolinxSidebar.SoftwareComponentCategory.DETAIL);
            RegisterClientComponent(typeof(WebBrowser).Assembly.GetName().Name + ".browser", "WebBrowser Control Browser", webBrowser.Version.Major + "." + webBrowser.Version.MajorRevision + "." + webBrowser.Version.Minor + "." + webBrowser.Version.MinorRevision, AcrolinxSidebar.SoftwareComponentCategory.DETAIL);
            var osInfo = Util.AssemblyUtil.OSInfo();
            var appInfo = Util.AssemblyUtil.AppInfo();
            RegisterClientComponent(osInfo["osId"], osInfo["osName"], osInfo["version"], AcrolinxSidebar.SoftwareComponentCategory.DEFAULT);
            RegisterClientComponent(appInfo["appId"], appInfo["productName"], appInfo["version"], AcrolinxSidebar.SoftwareComponentCategory.DEFAULT);
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

            var startpageUrl = GetStartPageURL();
            if (!string.IsNullOrEmpty(startpageUrl))
            {
                webBrowser.Navigate(startpageUrl);
            }
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
            var assemblyLocation = "";
            if (string.IsNullOrEmpty(StartPageSourceLocation))
            {
                Logger.AcroLog.Debug("Default start page source location is used");
                assemblyLocation = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\Acrolinx.Startpage.dll";
            }
            else
            {
                if (!Path.IsPathRooted(StartPageSourceLocation))
                {
                    StartPageSourceLocation = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), StartPageSourceLocation);
                }
                assemblyLocation = ((Path.GetFileName(StartPageSourceLocation) != "Acrolinx.Startpage.dll"))
                    ? StartPageSourceLocation + @"\Acrolinx.Startpage.dll" : StartPageSourceLocation;
            }

            var resUrl = BuildResUrl(assemblyLocation);
            if (string.IsNullOrEmpty(resUrl))
            {
                Logger.AcroLog.Error("Failed to locate " + assemblyLocation);
                SetUiError();
            }
            return resUrl;
        }

        private string BuildResUrl(string assemblyLocation)
        {
            Contract.Requires(!string.IsNullOrEmpty(assemblyLocation));
            var version = Util.FileUtil.GetFileVersion(assemblyLocation);
            if (!string.IsNullOrEmpty(version))
            {
                if (!assemblyLocation.StartsWith(@"\\"))
                {
                    return @"res://" + assemblyLocation + "//index.html";
                }

                var userTempPath = Path.GetTempPath()
                            + "Acrolinx.Startpage_" + version + ".dll";


                return (Util.FileUtil.CopyFileWithRetries(assemblyLocation, userTempPath, 5))
                    ? @"res://" + userTempPath + "//index.html"
                    : null;
            }
            return null;
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
        private string startPageLocation = "";

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

        [Description("Sets startpage location."), Category("Startpage")]
        [DefaultValue("")]
        public string StartPageSourceLocation
        {
            get
            {
                return startPageLocation;
            }

            set
            {
                startPageLocation = value;
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
            if (assembly != null)
            {
                var name = assembly.GetName();
                RegisterClientComponent(name.Name, humanReadableName, name.Version.ToString(), category);
            }
        }

        private bool mainComponentSet = false;
        public void RegisterClientComponent(string id, string name, string version, SoftwareComponentCategory category)
        {
            Contract.Requires(id != null);

            mainComponentSet |= category == SoftwareComponentCategory.MAIN;

            var clientComponents = InitParameters["clientComponents"].Value<JArray>();

            var component = new JObject();
            component.Add("id", id.ToLower());
            component.Add("name", name);
            component.Add("version", version);
            component.Add("category", category.ToString().ToUpper());
            if (category != SoftwareComponentCategory.DETAIL)
            {
                clientComponents.Add(component);
            }
            Logger.AcroLog.Info(name + "\t" + version);
        }

        public String Check(IDocument document)
        {
            acrolinxPlugin.Document = document;
            Logger.AcroLog.Debug("Content: " + document.Content);

            var code = "new function(){var c = window.external.getContent(); "
                + "return acrolinxSidebar.checkGlobal(c, {inputFormat:'" + document.Format.ToString().ToUpper() + "', requestDescription:{documentReference: '"
                + document.Reference.Replace("\\", "\\\\").Replace("'", "\\'").Replace("\n", "").Replace("\r", "") + "'}, selection:{ranges:" + SerializeSelection(document.Selections) + "}})}();";

            dynamic check = Eval(code);
            string checkId = check.checkId;
            return checkId;
        }
        public void CancelCheck()
        {
            var code = "acrolinxSidebar.onGlobalCheckRejected();";
            Eval(code);
            Logger.AcroLog.Info("Check canceled by Acrolinx Integration.");
        }

        /// <summary>
        /// Show a message in the Sidebar.
        /// Supported since Acrolinx Platform 2021.2 (Sidebar version 14.28).
        /// </summary>
        public void ShowMessage(Util.Message.Message message)
        {
            var title = GetJavaScriptFriendlyParameterString(message.Title);
            var text = GetJavaScriptFriendlyParameterString(message.Text);
            var code = $"acrolinxSidebar.showMessage({{type: '{message.Type.ToString().ToLowerInvariant()}', title: '{title}', text: '{text}'}});";
            Logger.AcroLog.Info($"Show message on sidebar: {message.Type}: {message.Title} - {message.Text}");
            Eval(code);
        }

        private string GetJavaScriptFriendlyParameterString(string input)
        {
            return input.Replace("\\", "\\\\").Replace("'", "\\'").Replace("\n", "").Replace("\r", "");
        }

        private string SerializeSelection(IReadOnlyList<IRange> selections)
        {
            if (selections == null)
            {
                return "[]";
            }

            return "[" + String.Join(",", selections.Select(s => "[" + s.Start + ", " + s.End + "]")) + "]";
        }

        internal dynamic Eval(string code)
        {
            Logger.AcroLog.Debug("eval(" + code + ")");
            dynamic result = webBrowser.Document.InvokeScript("eval", new object[] { code });
            return result;
        }

        internal void FireRequestCheck(ICheckOptions options)
        {
            Contract.Requires(options != null);

            RequestCheck?.Invoke(this, new CheckRequestedEventArgs(options));
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

        internal void FireProcessEmbedCheckData(JArray embedCheckInformation, string inputFormat)
        {
            Contract.Requires(embedCheckInformation != null);

            IDictionary<string, string> embedCheckInfo = null;
            if (embedCheckInformation != null)
            {
                embedCheckInfo = embedCheckInformation.ToDictionary(k => k["key"].ToString(), v => v["value"].ToString());
            }
            ProcessEmbedCheckData?.Invoke(this, new ProcessEmbedCheckDataEventArgs(embedCheckInfo, acrolinxPlugin.Document.StringToFormat(inputFormat)));
        }

        internal bool FireOpenBrowser(string url)
        {
            Contract.Requires(url != null);

            OpenBrowserEventArgs args = new OpenBrowserEventArgs(new Uri(url));

            OpenBrowser?.Invoke(this, args);

            return !args.Cancel;
        }

        private void webBrowser_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            webBrowser.ScriptErrorsSuppressed = true;
            webBrowser.IsWebBrowserContextMenuEnabled = false;
        }

        private void webBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            Logger.AcroLog.Debug("Sidebar navigated to: " + String.Format("{0}{1}{2}{3}", e.Url.Scheme, Uri.SchemeDelimiter, e.Url.Authority, e.Url.AbsolutePath));

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

                foreach (HtmlWindow frame in webBrowser.Document.Window.Frames)
                {
                    frame.Document.LosingFocus += LosingFocusInSidebar;
                }
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

            AdjustSidebarZoomLevelByWidth();
        }

        private void LosingFocusInSidebar(object sender, EventArgs e)
        {
            foreach (HtmlWindow frame in webBrowser.Document.Window.Frames)
            {
                frame.Document.Body.MouseUp += MouseUpInSidebar;
                frame.Document.LosingFocus -= LosingFocusInSidebar;
            }
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
            if (webBrowser.Document.Window.Frames.Count == 0 && webBrowser.Document?.ActiveElement != null)
            {
                webBrowser.Focus();
                webBrowser.Document.ActiveElement.Focus();
            }
            else
            {
                foreach (HtmlWindow frame in webBrowser.Document.Window.Frames)
                {
                    if (frame.Document?.ActiveElement != null)
                    {
                        webBrowser.Focus();
                        frame.Document.ActiveElement.Focus();
                        break;
                    }
                }
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
            else if (string.IsNullOrWhiteSpace(this.ServerAddress))
            {
                this.ShowServerSelector = true;
            }
        }

        private void SetUiError()
        {
            labelImage.Text = "\n\n\nOops, something went wrong with loading the Sidebar. Check the log file for any errors.";
            labelImage.TextAlign = ContentAlignment.TopLeft;
        }

        private void AcrolinxSidebar_Resize(object sender, EventArgs e)
        {
            AdjustSidebarZoomLevelByWidth();
        }

        private void AdjustSidebarZoomLevelByWidth()
        {
            try
            {
                if (webBrowser.ReadyState != WebBrowserReadyState.Complete)
                {
                    return;
                }

                object ax = webBrowser.ActiveXInstance;
                if (ax == null)
                {
                    return;
                }

                dynamic activeX = ax;
                const int OLECMDID_OPTICAL_ZOOM = 63;
                const int OLECMDEXECOPT_DONTPROMPTUSER = 2;
                int zoomFactor = (int)(this.Width * 100 * Util.GraphicUtil.GetScaling() / 300);

                activeX.ExecWB(OLECMDID_OPTICAL_ZOOM, OLECMDEXECOPT_DONTPROMPTUSER, zoomFactor, IntPtr.Zero);
            }
            catch (Exception e)
            {
                Logger.AcroLog.Warn(e.Message);
            }
        }
    }
}
