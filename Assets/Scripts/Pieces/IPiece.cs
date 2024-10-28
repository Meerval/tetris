using PositionCalculator;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Pieces
{
    public interface IPiece
    {
        public TileBase Tile();
        public Vector2Int[] ShapeMap();
        public Vector2Int[,] WallKicksMap();
        public int GetWallKickIndex(RotationDirection rotationDirection);
        public IPositionCalculator RotationCalculator(Vector2Int[] piecePosition, RotationDirection direction);
    }
}