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
        private Timestamp _time;

        public string Time
        {
            get => _time.ToString();
            set => _time = new Timestamp(value);
        }

        public List<StorableData> Data { get; set; }

        public StorableDataset()
        {
            Data = new List<StorableData>();
            _time = Timestamp.Now;
        }

        public StorableDataset(List<StorableData> data) : this()
        {
            Data = data.ToList();
            _time = Timestamp.Now;
        }
    }
}