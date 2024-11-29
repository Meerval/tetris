using System;
using Templates.Pretty;

namespace Templates.POCO
{
    [Serializable]
    public abstract class Poco<T> : IPoco
    {
        protected string EmptyId = "@NONE";

        public override string ToString()
        {
            return new PrettyObject<Poco<T>>(this).Prettify();
        }
    }
}