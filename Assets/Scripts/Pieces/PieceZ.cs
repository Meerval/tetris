using UnityEngine;

namespace Pieces
{
    public class PieceZ : Piece
    {
        private readonly Vector2Int[] _shape =
        {
            new Vector2Int(-1, 1), new Vector2Int(0, 1), new Vector2Int(0, 0), new Vector2Int(1, 0)
        };

        protected override Vector2Int[] ShapeMap()
        {
            return _shape;
        }
    }
}