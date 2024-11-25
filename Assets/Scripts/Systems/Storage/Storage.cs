using System;
using System.Collections.Generic;
using Settings.Systems.Storage;
using Systems.Storage.POCO;
using Templates.Singleton;
using UnityEngine;

namespace Systems.Storage
{
    public class Storage : SimpleSingleton<Storage>
    {
        private readonly Dictionary<string, IStorable> _idMap;
        private readonly IStorageStrategy _strategy;

        public Storage()
        {
            _idMap = new Dictionary<string, IStorable>();
            _strategy = StorageSettings.Strategy switch
            {
                EStorageStrategy.Local => new FileStorageStrategy(),
                _ => throw new Exception("There is no other classes of IStorageStrategy to create")
            };
        }

        public void Add(IStorable saveLoadObject) => _idMap[saveLoadObject.Id] = saveLoadObject;

        public void SaveGame()
        {
            _strategy.Save(_idMap.Values);
        }

        public void LoadGame()
        {
            var loadedData = _strategy.Load();

            foreach (StorableData data in loadedData)
            {
                string objectId = data.Id;
                if (_idMap.ContainsKey(objectId))
                {
                    _idMap[objectId].RestoreValues(data);
                }
                else
                {
                    Debug.LogError($"Can't restore data for object with id {objectId}");
                }
            }
        }
    }
}