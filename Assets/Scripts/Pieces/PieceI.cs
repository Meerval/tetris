using PositionCalculator;
using UnityEngine;

namespace Pieces
{
    public class PieceI : Piece
    {
        private readonly Vector2Int[] _shape = { new(-1, 1), new(0, 1), new(1, 1), new(2, 1) };

        private readonly Vector2Int[,] _wallKicks =
        {
            { new(0, 0), new(-2, 0), new(1, 0), new(-2, -1), new(1, 2) },
            { new(0, 0), new(2, 0), new(-1, 0), new(2, 1), new(-1, -2) },
            { new(0, 0), new(-1, 0), new(2, 0), new(-1, 2), new(2, -1) },
            { new(0, 0), new(1, 0), new(-2, 0), new(1, -2), new(-2, 1) },
            { new(0, 0), new(2, 0), new(-1, 0), new(2, 1), new(-1, -2) },
            { new(0, 0), new(-2, 0), new(1, 0), new(-2, -1), new(1, 2) },
            { new(0, 0), new(1, 0), new(-2, 0), new(1, -2), new(-2, 1) },
            { new(0, 0), new(-1, 0), new(2, 0), new(-1, 2), new(2, -1) }
        };

        public override Vector2Int[] ShapeMap()
        {
            return _shape;
        }

        public override Vector2Int[,] WallKicksMap()
        {
            return _wallKicks;
        }

        public override IPositionCalculator RotationCalculator(Vector2Int[] piecePosition, RotationDirection direction)
        {
            return new RotationPositionCalculatorIO(piecePosition, direction);
        }
        
    }
}