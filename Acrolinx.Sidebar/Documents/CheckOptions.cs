using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acrolinx.Sdk.Sidebar.Documents
{
    public class CheckOptionsProxy : ICheckOptions
    {
        private dynamic options = null;

        public CheckOptionsProxy()
        {
        }

        public CheckOptionsProxy(dynamic options)
        {
            this.options = options;
        }
        public bool Selection
        {
            get
            {
                try
                {
                    return options.selection;
                }
                catch
                {
                    return false;
                }
            }
        }
    }
}
