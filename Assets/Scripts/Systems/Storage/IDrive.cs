using System.Collections.Generic;
using Systems.Storage.POCO;

namespace Systems.Storage
{
    public interface IDrive
    {
        public void Save(IEnumerable<IStorable> objectsToSave);
        public List<StorableData> Load();
    }
}