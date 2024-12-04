using System.Collections.Generic;
using System.Linq;

namespace Systems.Pretty
{
    public class PrettyArray<T> : Systems.Pretty.Pretty
    {
        private readonly IEnumerable<string> _enumerable;

        public PrettyArray(T[] array)
        {
            _enumerable = array.Select(x => x.ToString()).ToList();
        }

        public PrettyArray(List<T> list)
        {
            _enumerable = list.Select(x => x.ToString()).ToList();
        }

        public PrettyArray(IEnumerable<T> enumerable)
        {
            _enumerable = enumerable.Select(x => x.ToString()).ToList();
        }
        

        public override string Prettify()
        {
            return "[" + string.Join(", ", _enumerable) + "]";
        }
    }
}