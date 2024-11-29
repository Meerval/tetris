using System.Collections.Generic;
using System.Linq;
using Systems.Storage.POCO;
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

            _idMap.Keys.ToList().ForEach
            (
                key =>
                {
                    if (loadedData.Select(data => data.Id).Contains(key))
                    {
                        _idMap[key].Load(loadedData.Find(data => data.Id.Equals(key)));
                    }
                    else
                    {
                        Debug.LogWarning($"Object with id='{key}' was loaded with initial data");
                        _idMap[key].LoadInitial();
                    }
                }
            );
        }
    }
}