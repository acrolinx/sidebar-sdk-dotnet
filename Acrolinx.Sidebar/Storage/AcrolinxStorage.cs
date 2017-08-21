using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acrolinx.Sdk.Sidebar.Storage
{
    public interface IAcrolinxStorage
    {
        string GetItem(string key);

        void RemoveItem(string key);

        void SetItem(string key, string data);

        void Clear();
    }
}
