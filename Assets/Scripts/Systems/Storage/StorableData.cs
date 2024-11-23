namespace Systems.Storage
{
    [System.Serializable]
    public class StorableData
    {
        public string Id { get; private set; }
        public object[] Data { get; private set; }
        
        public StorableData(string id, object[] data)
        {
            Id = id;
            Data = data;
        }
    }
}