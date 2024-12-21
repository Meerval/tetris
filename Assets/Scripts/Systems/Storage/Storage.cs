using System.Collections.Generic;
using System.Linq;
using Systems.Pretty;
using Systems.Storage.POCO;
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

        public void Add(IStorable saveLoadObject)
        {
            _idMap[saveLoadObject.Id] = saveLoadObject;
            Debug.Log($"Storable object with id='{saveLoadObject.Id}' was added to Storage");
        }

        public void SaveGame()
        {
            _drive.Save(_idMap.Values);
        }

        public void LoadGame()
        {
            if (_idMap.Count == 0)
            {
                Debug.LogWarning("There is no storable objects to load");
                return;
            }
            
            List<StorableData> loadedData = _drive.Load();

            loadedData.Where(data => !_idMap.ContainsKey(data.Id)).ToList()
                .ForEach
                (
                    data => Debug.LogWarning
                    (
                        $"Can't restore data for object with id='{data.Id}':\n" +
                        $"there is no id='{data.Id}' within id-type map keys ='{new PrettyArray<string>(_idMap.Keys.ToList())}'"
                    )
                );

            _idMap.Keys.ToList().ForEach(key => LoadFromDataList(loadedData, key));
        }

        private void LoadFromDataList(List<StorableData> loadedData, string key)
        {
            List<StorableData> data = loadedData.Where(data => data.Id == key).ToList();

            if (data.Count == 1)
            {
                _idMap[key].Load(data[0]);
                Debug.Log($"Object with id='{key}' was loaded from File");
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