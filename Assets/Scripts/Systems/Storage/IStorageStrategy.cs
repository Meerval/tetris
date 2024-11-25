using System.Collections.Generic;
using Systems.Storage.POCO;

namespace Systems.Storage
{
    public interface IStorageStrategy
    {
        public void Save(IEnumerable<IStorable> objectsToSave);
        public StorableData[] Load();
    }
}