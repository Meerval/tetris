using System;
using System.Collections.Generic;
using System.Linq;

namespace Systems.Storage.POCO
{
    [Serializable]
    public class SaveData : Poco<SaveData>
    {
        public DateTime SaveTime { get; set; }
        public List<StorableData> Data { get; set; }

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