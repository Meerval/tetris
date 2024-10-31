using System.Collections.Generic;
using System.Linq;

namespace Pretty
{
    public class PrettyArray<T> : IPretty
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
        

        public string Prettify()
        {
            return "[" + string.Join(", ", _enumerable) + "]";
        }

        public override string ToString()
        {
            return Prettify();
        }
    }
}