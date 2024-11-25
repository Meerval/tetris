using System;
using System.Collections.Generic;
using System.Linq;

namespace Systems.Storage.POCO
{
    [Serializable]
    public class SaveData
    {
        public DateTime SaveTime { get; }
        public List<StorableData> Data { get; }

        public SaveData()
        {
            Data = new List<StorableData>();
            SaveTime = DateTime.Now;
        }

        public SaveData(List<StorableData> data) : this()
        {
            Data = data;
            SaveTime = DateTime.Now;
        }

        public bool IsEmpty()
        {
            return Data.Where(storableData => !storableData.IsEmpty()).ToList().Count == 0;
        }
    }
}