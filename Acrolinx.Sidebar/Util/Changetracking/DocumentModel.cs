/* Copyright (c) 2016 Acrolinx GmbH */

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Acrolinx.Sdk.Sidebar.Documents;

namespace Acrolinx.Sdk.Sidebar.Util.Changetracking
{
    /// <summary>
    /// DocumentModel helps to keep track of the the changes the sidebar made to the text.
    /// </summary>
    public class DocumentModel
    {
        /// <summary>
        /// Change represents one change made to the document between two checks.
        /// </summary>
        private class Change
        {
            public Change(IRange range, string content)
            {
                Contract.Requires(range != null);
                Contract.Requires(content != null);
                this.Range = range;
                this.Content = content;
                this.Time = DateTime.Now;
            }
            public IRange Range { get; private set; }

            public string Content { get; private set; }

            public int LengthDifference
            {
                get { return Content.Length - Range.Length; }
            }

            public override string ToString()
            {
                return Content + Range + " @" + Time;
            }
            /// <summary>
            /// Just for debugging so that we know when the change was made.
            /// </summary>
            private DateTime Time { get; set; }
        }

        private readonly string originalContent;
        private readonly StringBuilder content;
        private readonly List<Change> changes = new List<Change>();
        public DocumentModel()
            : this("")
        {

        }
        public DocumentModel(string originalContent)
        {
            Contract.Requires(originalContent != null);

            this.originalContent = originalContent;
            this.content = new StringBuilder(originalContent);
        }
        public void Update(IRange originalRange, string content)
        {
            Contract.Requires(originalRange != null);
            Contract.Requires(content != null);
            Contract.Requires(originalRange.Start >= 0);
            Contract.Requires(originalRange.Length >= 0);
            Contract.Requires(originalRange.End <= OriginalContent.Length);

            System.Diagnostics.Trace.WriteLine("Old Content: " + Content);
            var range = OriginalToModifiedRange(originalRange);

            var change = new Change(range, content);
            System.Diagnostics.Trace.WriteLine("Change: " + change);
            this.content.Remove(range.Start, range.Length);
            this.content.Insert(range.Start, content);
            System.Diagnostics.Trace.WriteLine("New Content: " + Content);
            changes.Add(change);

            System.Diagnostics.Trace.WriteLine("All changes: " + String.Join(", " ,this.changes));
        }
        
        public string OriginalContent
        {
            get { return originalContent; }
        }

        public string Content
        {
            get {
                return content.ToString(); 
            }
        }

        public IRange OriginalToModifiedRange(IRange originalRange)
        {
            Contract.Requires(originalRange != null);
            Contract.Requires(originalRange.Start >= 0);
            Contract.Requires(originalRange.Start <= originalRange.End);
            Contract.Requires(originalRange.End <= OriginalContent.Length);

            int start = originalRange.Start;
            int end = originalRange.End;
            
            foreach(Change change in changes){
                int oldStart = start;
                if(change.Range.End <= start){
                    start += change.LengthDifference;
                }
                if(change.Range.End <= end){
                    end += change.LengthDifference;
                    System.Diagnostics.Trace.WriteLineIf(change.Range.End > oldStart, 
                        "Warning: text was changed overlapping with the range which had been looked up: " + originalRange + " change: " + change);
                }
                else if(change.Range.Start < end)
                {
                    end = Math.Min(end, change.Range.End);
                    System.Diagnostics.Trace.WriteLine(
                        "Warning: text was changed inside the range which had been looked up: " + originalRange + " change: " + change);
                }
            }

            return new Range(start, start + originalRange.Length);
        }
    }
}
