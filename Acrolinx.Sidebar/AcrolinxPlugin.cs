/* Copyright (c) 2016 Acrolinx GmbH */

using Acrolinx.Sdk.Sidebar.Documents;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using Acrolinx.Sdk.Sidebar.Util.Logging;
using System.IO;
using System.Diagnostics;
using Newtonsoft.Json;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace Acrolinx.Sdk.Sidebar
{
    [ClassInterface(ClassInterfaceType.AutoDual)]
    [ComVisible(true)]
    public class AcrolinxPlugin
    {
        private readonly Microsoft.Web.WebView2.WinForms.WebView2 webView2;
        private readonly AcrolinxSidebar sidebar;

        internal AcrolinxPlugin(Microsoft.Web.WebView2.WinForms.WebView2 webView2, AcrolinxSidebar sidebar)
        {
            Contract.Requires(webView2 != null);
            Contract.Requires(sidebar != null);

            this.Document = new Document();
            this.webView2 = webView2;
            this.sidebar = sidebar;
        }


        public async Task OnAfterObjectSet()
        {
            Logger.AcroLog.Debug("OnAfterObjectSet");

            await sidebar.Eval("window.bridge = chrome.webview.hostObjects.bridge; if (!window.console) { window.console = {} }; window.console.logOld = window.console.log; window.console.log = function(msg) { window.bridge.Log(msg); }");
            await sidebar.Eval("window.bridge = chrome.webview.hostObjects.bridge; window.onerror = function(msg, url, line, col, error) { window.bridge.OnError(msg, url, line, col, error); }");
            //The next line would have the effect that the sidebar exchanges objects instead of strings. This seem to fail in case of .NET internet explorer web control...

            await sidebar.Eval("{window.bridge = chrome.webview.hostObjects.bridge; window.acrolinxStorage = { getItem: function(key) { return await window.bridge.getItem(key); }, removeItem: function(key) { window.bridge.removeItem(key); }, setItem: function(key, data) { window.bridge.setItem(key, data); } } }");
            await sidebar.Eval("{window.bridge = chrome.webview.hostObjects.bridge; window.acrolinxPlugin =   {requestInit: function(){ window.bridge.requestInit()}, onInitFinished: function(finishResult) {window.bridge.onInitFinished(JSON.stringify(finishResult))}, configure: function(configuration) { window.bridge.configure(JSON.stringify(configuration)) }, requestGlobalCheck: function(options) { window.bridge.requestGlobalCheck(options) }, onCheckResult: function(checkResult) {window.bridge.onCheckResult(JSON.stringify(checkResult)) }, selectRanges: function(checkId, matches) { window.bridge.selectRanges(checkId, JSON.stringify(matches))}, replaceRanges: function(checkId, matchesWithReplacements) { window.bridge.replaceRanges(checkId, JSON.stringify(matchesWithReplacements)) }, download: function(downloadInfo) { window.bridge.download(JSON.stringify(downloadInfo))}, openWindow: function(openWindowParameters) { window.bridge.openWindow(JSON.stringify(openWindowParameters)) }, openLogFile: function() {window.bridge.openLogFile()}}; }");
        }

        public void Log(params dynamic[] o)
        {
            Contract.Requires(o != null);

            Logger.AcroLog.Info("JavaScript Log: " + string.Join(", ", o));
        }


        public void OnError(params dynamic[] o)
        {
            Contract.Requires(o != null);
            Contract.Requires(o.Length >= 5);

            Logger.AcroLog.Debug("onError");
            var l = string.Join(", ", o);
            try
            {
                var message = o[4].message as string;
                var stack = o[4].stack as string;

                Logger.AcroLog.Error("JavaScript Error: " + message + " " + stack);
            }
            catch
            {
                Logger.AcroLog.Error("JavaScript Error: " + l);
            }
        }

        public string getItem(params dynamic[] o)
        {
            Contract.Requires(o != null);
            Contract.Requires(o.Length > 0);

            Logger.AcroLog.Debug("getItem");

            string key = o[0];
            return sidebar.Storage.GetItem(key);
        }
        public void setItem(params dynamic[] o)
        {
            Contract.Requires(o != null);
            Contract.Requires(o.Length == 2);

            Logger.AcroLog.Debug("setItem");

            sidebar.Storage.SetItem(o[0], o[1]);
        }
        public void removeItem(params dynamic[] o)
        {
            Contract.Requires(o != null);
            Contract.Requires(o.Length > 0);

            Logger.AcroLog.Debug("removeItem");

            sidebar.Storage.RemoveItem(o[0]);
        }
        public bool openWindow(params dynamic[] o)
        {
            Contract.Requires(o != null);
            Contract.Requires(o.Length > 0);

            Logger.AcroLog.Debug("openWindow");

            dynamic ow = JObject.Parse(o[0]);

            string url = ow.url.Value;

            if (!(url.ToLower().StartsWith("http://") || url.ToLower().StartsWith("https://") || url.ToLower().StartsWith("mailto:") || url.ToLower().StartsWith("www.")))
            {
                Logger.AcroLog.Warn("Ignoring URL: '" + url + "'. It seems not to be a valid URL.");
                return false;
            }

            if (!sidebar.FireOpenBrowser(url))
            {
                return false;
            }

            System.Diagnostics.Process.Start(url);

            return true;
        }

        public async void requestInit(params object[] o)
        {
            Logger.AcroLog.Info("request init");

            var initParams = sidebar.InitParameters.ToString();

            await sidebar.Eval("acrolinxSidebar.init(" + initParams + ")");
        }

        public void onInitFinished(params object[] o)
        {
            sidebar.FireInitFinished();
        }

        public IDocument Document { get; set; }
        public String getContent()
        {
            Logger.AcroLog.Info("getContent");
            return Document.Content;
        }

        public String getDocumentReference()
        {
            Logger.AcroLog.Info("getDocumentReference");
            return Document.Reference;
        }

        public void requestGlobalCheck(params dynamic[] o)
        {
            Contract.Requires(o != null);
            Logger.AcroLog.Info("requestGlobalCheck");

            ICheckOptions options = ConvertOptions(o);

            sidebar.FireRequestCheck(options);
        }

        private ICheckOptions ConvertOptions(dynamic[] o)
        {
            if (o.Length == 0)
            {
                return new CheckOptionsProxy();
            }
            return new CheckOptionsProxy(o[0]);
        }

        public void selectRanges(params dynamic[] o)
        {
            Contract.Requires(o != null);
            Contract.Requires(o.Length >= 2);

            string checkId = o[0];
            string jsonString = o[1];
            Logger.AcroLog.Info("selectRanges(\"" + checkId + "\", \"" + jsonString + "\"");

            var matches = ConvertMatches(jsonString);
            sidebar.FireSelectRanges(checkId, matches);
        }

        private IEnumerable<MatchWithReplacement> ConvertMatches(string jsonString)
        {
            Contract.Requires(!string.IsNullOrWhiteSpace(jsonString));

            JArray matches = JArray.Parse(jsonString);

            Range lastRange = new Range(0, 0);
            foreach (dynamic match in matches)
            {
                string content = "" + match.content;
                string replacement = "" + match.replacement;
                Range range = CreateRangeSafe(lastRange, (int)match.range[0], (int)match.range[1]);
                lastRange = range;
                yield return new MatchWithReplacement(new Match(content, range), replacement);
            }
        }

        private static Range CreateRangeSafe(Range lastRange, int start, int end)
        {
            Contract.Requires(lastRange.End <= start);
            Contract.Requires(start <= end);
            return new Range(start, end);
        }

        public void replaceRanges(params dynamic[] o)
        {
            Contract.Requires(o != null);
            Contract.Requires(o.Length >= 2);
            Logger.AcroLog.Debug("replace ranges: " + o);

            string checkId = "" + o[0];
            string jsonString = "" + o[1];
            Logger.AcroLog.Info("replaceRanges(\"" + checkId + "\", \"" + jsonString + "\"");

            var matches = ConvertMatches(jsonString);
            sidebar.FireReplaceRanges(checkId, matches);
        }

        public void onCheckResult(params dynamic[] o)
        {
            Contract.Requires(o != null);
            Contract.Requires(o.Length > 0);
            string jsonStr = o[0];
            Logger.AcroLog.Debug("onCheckResult: " + jsonStr);

            using (var jsonReader = new JsonTextReader(new StringReader(jsonStr)) { DateParseHandling = DateParseHandling.None })
            {
                dynamic json = JObject.Load(jsonReader);
                sidebar.FireChecked(json.checkedPart.checkId.Value, new Range((int)json.checkedPart.range[0].Value, (int)json.checkedPart.range[1].Value));
                if (json.embedCheckInformation != null)
                {
                    sidebar.FireProcessEmbedCheckData(json.embedCheckInformation, json.inputFormat != null ? json.inputFormat.Value : "");
                }
            }
        }


        public void configure(params dynamic[] o)
        {
            Contract.Requires(o != null);
            Contract.Requires(o.Length > 0);
            string json = o[0];
            Logger.AcroLog.Info("configure: " + json);
        }

        public void openLogFile()
        {
            try
            {
                Process proc = Process.Start(Path.GetDirectoryName(Logger.Directory));
            }
            catch (Exception exce)
            {
                Logger.AcroLog.Error(exce.Message);
            }
        }
    }
}