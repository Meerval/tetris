using Newtonsoft.Json;
using Systems.Storage.POCO;

namespace Templates.Pretty
{
    public class PrettyObject<T> : IPretty where T : IPoco
    {
        private readonly T _t;

        public PrettyObject(T t)
        {
            _t = t;
        }

        public string Prettify()
        {
            return JsonConvert.SerializeObject(_t, Formatting.Indented);
        }
    }
}