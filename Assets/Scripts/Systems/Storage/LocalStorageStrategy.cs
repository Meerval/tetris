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
        private const string SaveFolderName = "Resources/Saves";
        private const string SaveFileName = "Saves.json";
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
            try
            {
                if (!Directory.Exists(FolderFullName))
                {
                    Directory.CreateDirectory(FolderFullName);
                    if (Directory.Exists(FolderFullName))
                    {
                        Debug.Log($"Directory created: {FolderFullName}");
                    }

                    Debug.LogError($"Can't load directory. Path {FolderFullName} doesn't exist and can't be created");
                }

                if (!File.Exists(FileFullName))
                {
                    File.Create(FileFullName).Dispose();
                    if (File.Exists(FileFullName))
                    {
                        Debug.Log($"File created at: {FileFullName}");
                    }

                    Debug.LogError($"Can't load file. Path {FileFullName} doesn't exist and can't be created");
                }

                string serializedFile = File.ReadAllText(FileFullName);
                if (string.IsNullOrEmpty(serializedFile))
                {
                    return new[] { new StorableData("RecordScore", new object[] { 0 }) };
                }

                Debug.Log($"Save to {FileFullName}");
                return JsonConvert.DeserializeObject<SaveFile>(serializedFile).Data.ToArray();
            }
            catch (Exception e)
            {
                Debug.LogError($"Parsing is failed: {e.Message}");
                throw;
            }
        }
    }
}