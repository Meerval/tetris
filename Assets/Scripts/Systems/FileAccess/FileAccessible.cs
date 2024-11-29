using System;
using System.IO;
using System.Text;
using Systems.Storage.POCO;
using UnityEngine;

namespace Systems.FileAccess
{
    public abstract class FileAccessible : IFileAccessible
    {
        private readonly string _baseDirectory;
        private readonly string _filePath;

        private bool IsDirectoryExists => Directory.Exists(_baseDirectory);
        private bool IsFileExists => File.Exists(_filePath);

        public FileAccessible(string fileName, string baseDirectory = null)
        {
            if (string.IsNullOrEmpty(baseDirectory))
            {
                baseDirectory = Path.Combine(Application.persistentDataPath, "GameData");
                Debug.LogWarning($"The base directory path is not specified. Selected default path: {baseDirectory}");
            }

            _baseDirectory = baseDirectory;
            EnsureDirectoryExists();

            if (string.IsNullOrEmpty(fileName))
            {
                int i = 0;
                fileName = "GameData";
                while (File.Exists(Path.Combine(_baseDirectory, fileName)))
                {
                    i++;
                    fileName = $"GameData{i}";
                }

                Debug.LogWarning($"The file name is not specified. Selected default name: {fileName}");
            }

            _filePath = Path.Combine(_baseDirectory, fileName);
            EnsureFileExists();
            Debug.Log($"File {_filePath} is ready to work with");
        }

        private void EnsureDirectoryExists()
        {
            if (IsDirectoryExists) return;
            try
            {
                Directory.CreateDirectory(_baseDirectory);
                Debug.Log($"Directory created: {_baseDirectory}");
            }
            catch (Exception e)
            {
                Debug.LogError($"Directory {_baseDirectory} doesn't exists and was not created: {e.Message}");
            }
        }

        private void EnsureFileExists()
        {
            if (IsFileExists) return;
            try
            {
                File.Create(_filePath);
                Debug.Log($"File created: {_filePath}");
            }
            catch (Exception e)
            {
                Debug.LogError($"File {_filePath} doesn't exists and was not created: {e.Message}");
            }
        }

        public void Create()
        {
            if (IsFileExists)
            {
                Debug.LogWarning($"File {_filePath} cannot be created: file already exists");
                return;
            }

            EnsureDirectoryExists();
            EnsureFileExists();
        }

        public void Delete()
        {
            if (IsFileExists)
            {
                File.Delete(_filePath);
                Debug.Log($"File deleted: {_filePath}");

                DirectoryInfo di = new(_baseDirectory ?? string.Empty);

                while (di is { Exists: true } && di.GetFiles().Length == 0)
                {
                    Directory.Delete(di.FullName);
                    Debug.Log($"Empty directory deleted: {di.FullName}");
                    di = di.Parent;
                }

                return;
            }

            Debug.LogWarning($"File {_filePath} cannot be deleted: file not found");
        }

        public void Overwrite(StorableDataset data)
        {
            try
            {
                using (StreamWriter writer = new(_filePath, false, Encoding.UTF8))
                {
                    Overwrite(data, writer);
                }

                Debug.Log($"File {_filePath} updated with data");
            }
            catch (Exception e)
            {
                Debug.LogError($"Failed to update file {_filePath} with data, Error: {e.Message}");
            }
        }

        public StorableDataset Read()
        {
            try
            {
                using (StreamReader reader = new(_filePath, Encoding.UTF8))
                {
                    StorableDataset data = ReadToEnd(reader);
                    Debug.Log($"Data loaded from file: {_filePath}\nData: {data}");
                    return data ?? new StorableDataset();
                }
            }
            catch (Exception e)
            {
                Debug.LogError($"Failed to load file: {_filePath}, Error: {e.Message}");
                return new StorableDataset();
            }
        }

        protected abstract void Overwrite(StorableDataset data, StreamWriter writer);

        protected abstract StorableDataset ReadToEnd(StreamReader reader);
    }
}