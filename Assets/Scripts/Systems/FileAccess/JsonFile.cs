using System.IO;
using Newtonsoft.Json;
using Templates.POCO;
using UnityEngine;

namespace Systems.FileAccess
{
    class JsonFile<T> : FileAccessible<T> where T : Poco, new()
    {
        public JsonFile(string fileName, string baseDirectory = null) : base(fileName, baseDirectory)
        {
        }

        protected override void Overwrite(T data, StreamWriter writer)
        {
            writer.Write(data.ToString());
        }

        protected override T ReadToEnd(StreamReader reader)
        {
            string r = reader.ReadToEnd();
            Debug.Log($"row = {r}");
            return JsonConvert.DeserializeObject<T>(r);
        }
    }
}