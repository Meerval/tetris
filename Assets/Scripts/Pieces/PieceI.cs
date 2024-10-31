using PositionCalculator;
using UnityEngine;

namespace Pieces
{
    public class PieceI : Piece
    {
        private readonly Vector2Int[] _shapeMap = { new(-1, 1), new(0, 1), new(1, 1), new(2, 1) };

        private readonly Vector2Int[,] _wallKicks =
        {
            { new(0, 0), new(-2, 0), new(1, 0), new(-2, -1), new(1, 2) },
            { new(0, 0), new(2, 0), new(-1, 0), new(2, 1), new(-1, -2) },
            { new(0, 0), new(-1, 0), new(2, 0), new(-1, 2), new(2, -1) },
            { new(0, 0), new(1, 0), new(-2, 0), new(1, -2), new(-2, 1) },
            { new(0, 0), new(2, 0), new(-1, 0), new(2, 1), new(-1, -2) },
            { new(0, 0), new(-2, 0), new(1, 0), new(-2, -1), new(1, 2) },
            { new(0, 0), new(1, 0), new(-2, 0), new(1, -2), new(-2, 1) },
            { new(0, 0), new(-1, 0), new(2, 0), new(-1, 2), new(2, -1) },
        };

        protected override Vector2Int[] ShapeMap()
        {
            return _shapeMap;
        }

        protected override Vector2Int[,] WallKicks()
        {
            return _wallKicks;
        }

        protected override IShapeMapClc RotationCalculator()
        {
            return new RotationClcIO(ShapeMap(), RotationAngle);
        }
    }
}