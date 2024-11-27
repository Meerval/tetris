using System.IO;
using Newtonsoft.Json;
using Systems.Storage.POCO;
using UnityEngine;

namespace Systems.FileAccess
{
    class JsonFile : FileAccessible
    {
        public JsonFile(string fileName, string baseDirectory = null) : base(fileName, baseDirectory)
        {
        }

        protected override void Overwrite(SaveData data, StreamWriter writer)
        {
            writer.Write(JsonConvert.SerializeObject(data));
        }

        protected override SaveData ReadToEnd(StreamReader reader)
        {
            string r = reader.ReadToEnd();
            Debug.Log($"row = {r}");
            return JsonConvert.DeserializeObject<SaveData>(r);
        }
    }
}