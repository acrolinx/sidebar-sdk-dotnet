using Newtonsoft.Json.Linq;

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
            this.options = JObject.Parse(options);
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
