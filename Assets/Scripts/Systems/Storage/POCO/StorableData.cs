using System;
using Board.Meta;
using Templates.POCO;

namespace Systems.Storage.POCO
{
    [Serializable]
    public class StorableData : Poco<StorableData>
    {
        public string Id { get; set; }
        public Poco<object> Data { get; set; }

        public StorableData()
        {
            Id = EmptyId;
        }

        public StorableData(string id, Poco<object> data)
        {
            Id = id;
            Data = data;
        }
    }
}