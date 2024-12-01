using System;
using Templates.Pretty;

namespace Templates.POCO
{
    [Serializable]
    public class Poco : IPoco
    {
        protected string EmptyId = "@NONE_ID";

        public override string ToString()
        {
            return new PrettyObject(this).Prettify();
        }
    }
}