using Newtonsoft.Json;
using Templates.POCO;

namespace Templates.Pretty
{
    public class PrettyObject<T> : Pretty where T : IPoco
    {
        private readonly T _t;

        public PrettyObject(T t)
        {
            _t = t;
        }

        public override string Prettify()
        {
            return JsonConvert.SerializeObject(_t, Formatting.Indented);
        }
    }
}