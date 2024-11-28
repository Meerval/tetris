namespace Systems.Storage.POCO
{
    public interface IStorable
    {
        public string Id { get; }
        public StorableData DataToStore();
        public void RestoreValues(StorableData storableDataData);
    }
}