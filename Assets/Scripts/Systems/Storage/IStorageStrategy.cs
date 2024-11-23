using System.Collections.Generic;

namespace Systems.Storage
{
    public interface IStorageStrategy
    {
        public void Save(IEnumerable<IStorable> objectsToSave);
        public StorableData[] Load();
    }
}