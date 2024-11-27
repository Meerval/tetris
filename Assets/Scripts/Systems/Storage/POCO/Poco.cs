using System;
using Templates.Pretty;

namespace Systems.Storage.POCO
{
    [Serializable]
    public abstract class Poco<T> : IPoco
    {
        public override string ToString()
        {
            return new PrettyObject<Poco<T>>(this).Prettify();
        }
    }
}