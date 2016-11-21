/* Copyright (c) 2016 Acrolinx GmbH */

using Acrolinx.Sdk.Sidebar.Documents;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace Acrolinx.Sdk.Sidebar
{
    [System.Runtime.InteropServices.ComVisibleAttribute(true)]
    public class AcrolinxPlugin
    {
        private readonly System.Windows.Forms.WebBrowser webBrowser;
        private readonly AcrolinxSidebar sidebar;

        internal AcrolinxPlugin(System.Windows.Forms.WebBrowser webBrowser, AcrolinxSidebar sidebar)
        {
            Contract.Requires(webBrowser != null);
            Contract.Requires(sidebar != null);

            this.Document = new Document();
            this.webBrowser = webBrowser;
            this.sidebar = sidebar;
        }


        public void OnAfterObjectSet()
        {
            System.Diagnostics.Trace.WriteLine("OnAfterObjectSet");

            sidebar.Eval("if (!window.console) { window.console = {} }; window.console.logOld = window.console.log; window.console.log = function(msg) { window.external.Log(msg); }" );
            sidebar.Eval( "window.onerror = function(msg, url, line, col, error) { window.external.OnError(msg, url, line, col, error); }" );
            //The next line would have the effect that the sidebar exchanges objects instead of strings. This seem to fail in case of .NET internet explorer web control...
            webBrowser.Document.InvokeScript("eval", new object[] { "window.acrolinxPlugin =   {requestInit: function(){ window.external.requestInit()}, onInitFinished: function(finishResult) {window.external.onInitFinished(JSON.stringify(finishResult))}, configure: function(configuration) { window.external.configure(JSON.stringify(configuration)) }, requestGlobalCheck: function() { window.external.requestGlobalCheck() }, onCheckResult: function(checkResult) {window.external.onCheckResult(JSON.stringify(checkResult)) }, selectRanges: function(checkId, matches) { window.external.selectRanges(checkId, JSON.stringify(matches))}, replaceRanges: function(checkId, matchesWithReplacements) { window.external.replaceRanges(checkId, JSON.stringify(matchesWithReplacements)) }, download: function(downloadInfo) { window.external.download(JSON.stringify(downloadInfo))}, openWindow: function(openWindowParameters) { window.external.openWindow(JSON.stringify(openWindowParameters)) }}; "});
        }

        public void Log(params dynamic[] o)
        {
            Contract.Requires(o != null);

            System.Diagnostics.Trace.WriteLine("JavaScript Log: " + string.Join(", ", o));
        }
        
     
        public void OnError(params dynamic[] o)
        {
            Contract.Requires(o != null);
            Contract.Requires(o.Length >= 5);

            System.Diagnostics.Trace.WriteLine("onError");
            var l = string.Join(", ", o);
            try
            {
                var message = o[4].message as string;
                var stack = o[4].stack as string;

                System.Diagnostics.Trace.TraceError("JavaScript Error: " + message + " " + stack);
            }
            catch
            {

            System.Diagnostics.Trace.TraceError("JavaScript Error: " + l);
            }
        }


        public bool openWindow(params dynamic[] o)
        {
            Contract.Requires(o != null);
            Contract.Requires(o.Length > 0);

            System.Diagnostics.Trace.WriteLine("openWindow");

            dynamic ow = JObject.Parse(o[0]);

            string url = ow.url.Value;

            if(!(url.ToLower().StartsWith("http") || url.ToLower().StartsWith("mailto:") || url.ToLower().StartsWith("www")))
            {
                System.Diagnostics.Trace.TraceWarning("Ignoring URL: '" + url + "'. It seems not to be a valid URL.");
                return false;
            }
            System.Diagnostics.Process.Start(url);

            return true;
        }

        public void requestInit(params object[] o)
        {
            System.Diagnostics.Trace.WriteLine("request init");

            var initParams = sidebar.InitParameters.ToString();

            sidebar.Eval("acrolinxSidebar.init(" + initParams + ")");
        }

        public void onInitFinished(params object[] o)
        {
            sidebar.FireInitFinished();
        }

        public IDocument Document { get; set; }
        public String getContent()
        {
            System.Diagnostics.Trace.WriteLine("getContent");
            return Document.Content;
        }

        public String getDocumentReference()
        {
            System.Diagnostics.Trace.WriteLine("getDocumentReference");
            return Document.Reference;
        }

        public void requestGlobalCheck(params dynamic[] o)
        {
            System.Diagnostics.Trace.WriteLine("requestGlobalCheck");

            sidebar.FireRequestCheck();
        }
        public void selectRanges(params dynamic[] o)
        {
            Contract.Requires(o != null);
            Contract.Requires(o.Length >= 2);

            string checkId = o[0];
            string jsonString = o[1];
            System.Diagnostics.Trace.WriteLine("selectRanges(\"" + checkId + "\", \"" + jsonString + "\"");

            var matches = ConvertMatches(jsonString);
            sidebar.FireSelectRanges(checkId, matches);
        }

        private IEnumerable<MatchWithReplacement> ConvertMatches(string jsonString)
        {
            Contract.Requires(!string.IsNullOrWhiteSpace(jsonString));

            JArray matches = JArray.Parse(jsonString);

            Range lastRange = new Range(0,0);
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
            System.Diagnostics.Trace.WriteLine("replace ranges: " + o);

            string checkId = "" + o[0];
            string jsonString = "" + o[1];
            System.Diagnostics.Trace.WriteLine("replaceRanges(\"" + checkId + "\", \"" + jsonString + "\"");

            var matches = ConvertMatches(jsonString);
            sidebar.FireReplaceRanges(checkId, matches);
        }

        public string onCheckResult(params dynamic[] o)
        {
            Contract.Requires(o != null);
            Contract.Requires(o.Length > 0);
            string jsonStr = o[0];
            System.Diagnostics.Trace.WriteLine("onCheckResult: " + jsonStr);

            dynamic json = JObject.Parse(jsonStr);

            sidebar.FireChecked(json.checkedPart.checkId.Value, new Range((int)json.checkedPart.range[0].Value, (int)json.checkedPart.range[1].Value));
            return "{}";
        }


        public void configure(params dynamic[] o)
        {
            Contract.Requires(o != null);
            Contract.Requires(o.Length > 0);
            string json = o[0];
            System.Diagnostics.Trace.WriteLine("configure: " + json);
        }
    }
}
