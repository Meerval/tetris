using System;
using Templates.Pretty;

namespace Templates.POCO
{
    [Serializable]
    public class Poco<T> : IPoco
    {
        protected string EmptyId = "@NONE_ID";

        public override string ToString()
        {
            return new PrettyObject<Poco<T>>(this).Prettify();
        }
    }
}