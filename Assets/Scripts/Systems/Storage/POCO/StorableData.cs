using System;
using System.Collections.Generic;
using Settings;
using Templates.POCO;
using Templates.TypeSelectors;

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
    
    public class DataSelector : SettingsDependantSelector<IPoco, string>
    {
        protected override Dictionary<string, Type> TypesMap { get; } = new();
        
        protected override string Key { get; set; } = TetrisSettings.Drive;
    }
}