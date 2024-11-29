namespace Systems.Storage.POCO
{
    public interface IStorable
    {
        public string Id { get; }
        public StorableData StorableData { get; }
        public void Load(StorableData loadData);
        public void LoadInitial();
    }
}