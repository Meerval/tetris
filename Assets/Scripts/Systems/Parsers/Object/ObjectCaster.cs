using UnityEngine;

namespace Systems.Parsers.Object
{
    public abstract class ObjectCaster<T1, T2> where T1 : ObjectCaster<T1, T2>, new()
    {
        private object ValueToParse { get; set; }

        public static ObjectCaster<T1, T2> TryParse(object value)
        {
            T1 inst = new()
            {
                ValueToParse = value
            };
            return inst;
        }

        public T2 OrElse(T2 defaultValue)
        {
            if (Parse(ValueToParse, out T2 result)) return result;
            Debug.LogError($"Can't parse value='{ValueToParse}' to '{typeof(T2).Name}'\n" +
                           $"Default value='{defaultValue}' was selected");
            return defaultValue;
        }

        protected abstract bool Parse(object valueToParse, out T2 result);
    }
}