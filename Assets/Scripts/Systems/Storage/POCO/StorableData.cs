using System;
using System.Collections.Generic;
using Templates.POCO;

namespace Systems.Storage.POCO
{
    [Serializable]
    public class StorableData : Poco
    {
        public string Id { get; set; }
        public Dictionary<string, object> Data { get; set; }

        public StorableData()
        {
            Id = EmptyId;
            Data = new Dictionary<string, object>();
        }

        public StorableData(string id, Dictionary<string, object> data)
        {
            Id = id;
            Data = data;
        }
    }
}