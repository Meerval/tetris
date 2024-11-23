using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using UnityEngine;

namespace Systems.Storage
{
    public class FileStorageStrategy : IStorageStrategy
    {
        private const string SaveFolderName = "Resources/Saved";
        private const string SaveFileName = "GameSaveFile.json";
        private static string FolderFullName => Path.Combine(Application.persistentDataPath, SaveFolderName);
        private static string FileFullName => Path.Combine(FolderFullName, SaveFileName);

        public void Save(IEnumerable<IStorable> objectsToSave)
        {
            try
            {
                var serializedData = objectsToSave.Select(@object => @object.DataToStore()).ToList();

                if (!Directory.Exists(FolderFullName)) Directory.CreateDirectory(FolderFullName);

                SaveFile saveFile = new(serializedData);
                string serializedSaveFile = JsonConvert.SerializeObject(saveFile);
                File.WriteAllText(FileFullName, serializedSaveFile);
            }
            catch (Exception e)
            {
                Debug.LogException(e);
                throw;
            }
        }

        public StorableData[] Load()
        {
            if (!File.Exists(FileFullName))
            {
                Debug.LogError($"Can't load save file. File {FileFullName} is doesn't exist.");
                return null;
            }

            try
            {
                string serializedFile = File.ReadAllText(FileFullName);
                if (string.IsNullOrEmpty(serializedFile))
                {
                    Debug.LogError($"Loaded file {FileFullName} is empty.");
                    return null;
                }

                Debug.Log($"Save to {FileFullName}");
                return JsonConvert.DeserializeObject<SaveFile>(serializedFile).Data.ToArray();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}