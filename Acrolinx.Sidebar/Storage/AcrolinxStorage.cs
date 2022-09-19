using Newtonsoft.Json.Linq;

namespace Acrolinx.Sdk.Sidebar.Storage
{
    public interface IAcrolinxStorage
    {
        string GetItem(string key);

        void RemoveItem(string key);

        void SetItem(string key, string data);

        JObject GetAllItems();
    }
}
