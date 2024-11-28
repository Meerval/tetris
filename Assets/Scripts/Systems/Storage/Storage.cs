using System;
using System.Collections.Generic;
using Systems.Storage.POCO;
using Templates.Selectors;
using Templates.Singleton;
using UnityEngine;

namespace Systems.Storage
{
    public class Storage : SimpleSingleton<Storage>
    {
        private readonly Dictionary<string, IStorable> _idMap;
        private readonly IDrive _drive;

        public Storage()
        {
            _idMap = new Dictionary<string, IStorable>();
            _drive = new DriveSelector().Select();
        }

        public void Add(IStorable saveLoadObject) => _idMap[saveLoadObject.Id] = saveLoadObject;

        public void SaveGame()
        {
            _drive.Save(_idMap.Values);
        }

        public void LoadGame()
        {
            var loadedData = _drive.Load();

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