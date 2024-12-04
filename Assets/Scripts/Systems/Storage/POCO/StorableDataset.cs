using System;
using System.Collections.Generic;
using System.Linq;
using Systems.Chrono;
using Templates.POCO;

namespace Systems.Storage.POCO
{
    [Serializable]
    public class StorableDataset : Poco
    {
        public string Time { get; set; }
        public List<StorableData> Data { get; set; }

        public StorableDataset()
        {
            Data = new List<StorableData>();
            Time = Timestamp.Now.ToString();
        }

        public StorableDataset(List<StorableData> data) : this()
        {
            Data = data.ToList();
            Time = Timestamp.Now.ToString();
        }
    }
}