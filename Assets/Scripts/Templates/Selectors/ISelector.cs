using System;
using System.Collections.Generic;
using System.Linq;
using Templates.Pretty;

namespace Templates.Selectors
{
    public interface ISelector<out T> where T : class
    {
        public T Select();
    }

    public abstract class Selector<T1, T2> : ISelector<T1> where T1 : class
    {
        public T1 Select()
        {
            EnsureKeyExists();
            return Activator.CreateInstance(TypesMap[Key]) as T1;
        }

        private void EnsureKeyExists()
        {
            if (TypesMap.ContainsKey(Key))
                throw new Exception
                (
                    $"There is no type of '{typeof(T1).Name}' with key '{Key}' to create.\n" +
                    $"Available types are: {new PrettyArray<T2>(TypesMap.Keys.ToList()).Prettify()}"
                );
        }

        protected abstract Dictionary<T2, Type> TypesMap { get; }
        protected abstract T2 Key { get; set; }
    }
}