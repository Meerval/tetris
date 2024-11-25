using System.Collections.Generic;
using System.IO;
using System.Linq;
using Systems.FileAccess;
using Systems.Storage.POCO;
using UnityEngine;

namespace Systems.Storage
{
    public class FileStorageStrategy : IStorageStrategy
    {
        private const string SaveFolderName = "Saves";
        private const string SaveFileName = "Saves.json";

        private readonly IFileAccessible _file = new JsonFile(SaveFileName,
            Path.Combine(Application.persistentDataPath, SaveFolderName));

        public void Save(IEnumerable<IStorable> objectsToSave)
        {
            List<StorableData> serializedData = objectsToSave.Select(@object => @object.DataToStore()).ToList();
            SaveData saveData = new(serializedData);
            _file.Overwrite(saveData);
        }

        public StorableData[] Load()
        {
            SaveData data = _file.Read();
            return data.IsEmpty() ? StorableData.Initial() : data.Data.ToArray();
        }
    }
}