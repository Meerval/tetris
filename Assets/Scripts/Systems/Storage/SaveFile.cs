using System;
using System.Collections.Generic;

namespace Systems.Storage
{
    [Serializable]
    public struct SaveFile
    {
        public DateTime SaveTime { get; }
        public List<StorableData> Data { get; }

        public SaveFile(List<StorableData> data) : this()
        {
            Data = data;
            SaveTime = DateTime.Now;
        }
    }
}