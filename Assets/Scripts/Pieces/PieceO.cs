using PositionCalculator;
using UnityEngine;

namespace Pieces
{
    public class PieceO : Piece
    {
        private readonly Vector2Int[] _shape = { new(0, 1), new(1, 1), new(0, 0), new(1, 0) };

        public override Vector2Int[] ShapeMap()
        {
            return _shape;
        }

        public override IPositionCalculator RotationCalculator(Vector2Int[] piecePosition, RotationDirection direction)
        {
            return new RotationPositionCalculatorIO(piecePosition, direction);
        }
    }
}