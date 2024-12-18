﻿using System.IO;
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

        protected override void Overwrite(StorableDataset data, StreamWriter writer)
        {
            writer.Write(data.ToString());
        }

        protected override StorableDataset ReadToEnd(StreamReader reader)
        {
            string r = reader.ReadToEnd();
            Debug.Log($"row = {r}");
            return JsonConvert.DeserializeObject<StorableDataset>(r);
        }
    }
}