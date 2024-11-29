using System.Collections.Generic;
using System.Linq;
using Systems.Storage.POCO;
using Templates.Pretty;
using Templates.Singleton;
using Templates.TypeSelectors;
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
            List<StorableData> loadedData = _drive.Load();

            loadedData.Where(data => !_idMap.ContainsKey(data.Id)).ToList()
                .ForEach(data => Debug.LogWarning($"Can't restore data for object with id='{data.Id}'"));

            _idMap.Keys.ToList().ForEach(key => LoadFromDataList(loadedData, key));
        }

        private void LoadFromDataList(List<StorableData> loadedData, string key)
        {
            List<StorableData> data = loadedData.Where(data => data.Id == key).ToList();

            if (data.Count == 1)
            {
                _idMap[key].Load(data[0]);
            }
            else
            {
                if (data.Count > 1)
                {
                    Debug.LogWarning($"There is {data.Count} data with id='{key}' in the storage");
                }

                Debug.LogWarning($"Object with id='{key}' was loaded with initial data");
                _idMap[key].LoadInitial();
            }
        }
    }
}