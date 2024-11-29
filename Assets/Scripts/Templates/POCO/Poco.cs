using System;
using Templates.Pretty;

namespace Templates.POCO
{
    [Serializable]
    public abstract class Poco<T> : IPoco
    {
        public string Empty { get; } = "@NONE";

        public override string ToString()
        {
            return new PrettyObject<Poco<T>>(this).Prettify();
        }
    }
}