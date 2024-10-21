using Math;
using PositionCalculator;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Pieces
{
    public abstract class Piece : IPiece
    {
        private readonly TileBase _tile;
        private readonly Vector2Int[,] _wallKicks =
        {
            { new(0, 0), new(-1, 0), new(-1, 1), new(0, -2), new(-1, -2) },
            { new(0, 0), new(1, 0), new(1, -1), new(0, 2), new(1, 2) },
            { new(0, 0), new(1, 0), new(1, -1), new(0, 2), new(1, 2) },
            { new(0, 0), new(-1, 0), new(-1, 1), new(0, -2), new(-1, -2) },
            { new(0, 0), new(1, 0), new(1, 1), new(0, -2), new(1, -2) },
            { new(0, 0), new(-1, 0), new(-1, -1), new(0, 2), new(-1, 2) },
            { new(0, 0), new(-1, 0), new(-1, -1), new(0, 2), new(-1, 2) },
            { new(0, 0), new(1, 0), new(1, 1), new(0, -2), new(1, -2) }
        };

        private readonly RotationType _rotationType = PositionCalculator.RotationType.A;

        public TileBase Tile()
        {
            return _tile;
        }

        public virtual Vector2Int[,] WallKicksMap()
        {
            return _wallKicks;
        }
        
        public int GetWallKickIndex(RotationDirection rotationDirection)
        {
            int wallKickIndex = rotationDirection.Value() * 2;
            if (wallKickIndex < 0) wallKickIndex--;
            return Wrapper.Wrap(wallKickIndex, 0, WallKicksMap().GetLength(0));
        }

        public virtual RotationType RotationType()
        {
            return _rotationType;
        }

        public abstract Vector2Int[] ShapeMap();
    }
}