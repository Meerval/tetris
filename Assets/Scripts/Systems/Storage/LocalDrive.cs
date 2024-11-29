using System.Collections.Generic;
using System.IO;
using System.Linq;
using Systems.FileAccess;
using Systems.Storage.POCO;
using UnityEngine;

namespace Systems.Storage
{
    public class LocalDrive : IDrive
    {
        private const string SaveFolderName = "Saves";
        private const string SaveFileName = "Saves.json";

        private readonly IFileAccessible _file = new JsonFile(SaveFileName,
            Path.Combine(Application.persistentDataPath, SaveFolderName));

        public void Save(IEnumerable<IStorable> objectsToSave)
        {
            List<StorableData> serializedData = objectsToSave.Select(o => o.StorableData).ToList();
            StorableDataset storableDataset = new(serializedData);
            _file.Overwrite(storableDataset);
        }

        public List<StorableData> Load()
        {
            return _file.Read().Data;
        }
    }
}