using PositionCalculator;
using UnityEngine;

namespace Pieces
{
    public class PieceO : Piece
    {
        private readonly Vector2Int[] _shape = { new(0, 1), new(1, 1), new(0, 0), new(1, 0) };
        private readonly RotationType _rotationType = PositionCalculator.RotationType.B;

        public override Vector2Int[] ShapeMap()
        {
            return _shape;
        }

        public override RotationType RotationType()
        {
            return _rotationType;
        }
    }
}