using Newtonsoft.Json.Linq;
using Systems.Storage.POCO;

namespace Systems.Storage
{
    public interface IStorable
    {
        public static void Store(IStorable storable)
        {
            Storage.Instance.Add(storable);
        }

        public string Id { get; }
        public StorableData StorableData { get; }
        public void Load(StorableData loadData);
        public void LoadInitial();

        public string ToString()
        {
            JObject jsonObj = JObject.Parse(StorableData.ToString());
            jsonObj["id"] = Id;
            return jsonObj.ToString();
        }
    }
}