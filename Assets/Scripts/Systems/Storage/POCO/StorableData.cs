using System;
using System.Collections.Generic;
using Systems.Chrono;
using Templates.POCO;
using Templates.Pretty;
using UnityEngine;

namespace Systems.Storage.POCO
{
    [Serializable]
    public class StorableData : Poco
    {
        public string Id { get; set; }
        public Dictionary<string, object> Data { get; set; }

        public StorableData()
        {
            Id = EmptyId;
            Data = new Dictionary<string, object>();
        }

        public StorableData(string id, Dictionary<string, object> data)
        {
            Id = id;
            Data = data;
        }

        public bool TryParse(string key, out long result)
        {
            if (long.TryParse(Data[key].ToString(), out result))
            {
                return true;
            }

            TrowParseErrorLog(key, "long");
            result = -1L;
            return false;
        }

        public bool TryParse(string key, out string result)
        {
            if (Data[key] != null)
            {
                result = Data[key].ToString();
                return true;
            }

            TrowParseErrorLog(key, "string");
            result = string.Empty;
            return false;
        }

        public bool TryParse(string key, out Timestamp timestamp)
        {
            if (TryParse(key, out string strTimestamp))
            {
                timestamp = new Timestamp(strTimestamp);
                return true;
            }

            TrowParseErrorLog(key, "string");
            timestamp = new Timestamp("0");
            return false;
        }

        private void TrowParseErrorLog(string key, string type)
        {
            Debug.LogError(
                $"Can't parse value with key='{key}' to '{type}'" +
                $"from Data='{new PrettyDictionary<string, object>(Data).Prettify()}'");
        }
    }
}