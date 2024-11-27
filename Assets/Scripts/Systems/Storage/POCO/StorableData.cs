using System;

namespace Systems.Storage.POCO
{
    [Serializable]
    public class StorableData : Poco<StorableData>
    {
        public string Id { get; set; }
        public object[] Data { get; set; }

        public StorableData()
        {
            Id = "Empty";
            Data = new object[] { };
        }

        public StorableData(string id, object[] data)
        {
            Id = id;
            Data = data;
        }

        public bool IsEmpty()
        {
            return Id == "Empty";
        }

        public static StorableData[] Initial()
        {
            return new[] { new StorableData("RecordScore", new object[] { 0 }) };
        }
    }
}