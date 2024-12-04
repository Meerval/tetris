using Newtonsoft.Json;
using Templates.POCO;

namespace Systems.Pretty
{
    public class PrettyObject : Pretty
    {
        private readonly IPoco _t;

        public PrettyObject(IPoco t)
        {
            _t = t;
        }

        public override string Prettify()
        {
            return JsonConvert.SerializeObject(_t, Formatting.Indented);
        }
    }
}