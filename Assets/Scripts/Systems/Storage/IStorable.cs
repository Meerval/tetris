namespace Systems.Storage
{
    public interface IStorable
    {
        public string ComponentSaveId { get; }
        public StorableData DataToStore();
        public void RestoreValues(StorableData storableDataData);
    }
}