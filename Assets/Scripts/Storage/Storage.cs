﻿using System;
using System.Collections.Generic;
using Settings.Storage;
using Structure;
using UnityEngine;

namespace Storage
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

        public void Add(IStorable saveLoadObject) => _idMap[saveLoadObject.ComponentSaveId] = saveLoadObject;

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
                if (!_idMap.ContainsKey(objectId))
                {
                    Debug.LogError($"Can't restore data for object with id {objectId}");
                    continue;
                }

                _idMap[objectId].RestoreValues(data);
            }
        }
    }
}