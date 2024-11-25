using Systems.Storage.POCO;

namespace Systems.Storage
{
    public interface IStorable
    {
        public string Id { get; }
        public StorableData DataToStore();
        public void RestoreValues(StorableData storableDataData);
    }
}