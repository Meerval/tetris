using System;
using System.Collections.Generic;
using System.Linq;
using Templates.POCO;

namespace Systems.Storage.POCO
{
    [Serializable]
    public class StorableDataset : Poco
    {
        public DateTime SaveTime { get; set; }
        public List<StorableData> Data { get; set; }

        public StorableDataset()
        {
            Data = new List<StorableData>();
            SaveTime = DateTime.Now;
        }

        public StorableDataset(List<StorableData> data) : this()
        {
            Data = data.ToList();
            SaveTime = DateTime.Now;
        }
    }
}