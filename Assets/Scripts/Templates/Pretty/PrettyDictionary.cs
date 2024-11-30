using System.Collections.Generic;
using Newtonsoft.Json;
namespace Templates.Pretty
{
    public class PrettyDictionary<T1, T2> : Pretty
    {
        private readonly Dictionary<T1, T2> _t;

        public PrettyDictionary(Dictionary<T1, T2> t)
        {
            _t = t;
        }

        public override string Prettify()
        {
            return JsonConvert.SerializeObject(_t, Formatting.Indented);
        }
    }
}