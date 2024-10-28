using System.Collections.Generic;
using UnityEngine;

namespace Pretty
{
    public class PrettyVector2IntArray : IPretty
    {
        private readonly IEnumerable<Vector2Int> _vector2IntArray;

        public PrettyVector2IntArray(Vector2Int[] vector2IntArray)
        {
            _vector2IntArray = vector2IntArray;
        }

        public PrettyVector2IntArray(IEnumerable<Vector2Int> vector2IntArray)
        {
            _vector2IntArray = vector2IntArray;
        }

        public string Prettify()
        {
            return "[" + string.Join(", ", _vector2IntArray) + "]";
        }
    }
}