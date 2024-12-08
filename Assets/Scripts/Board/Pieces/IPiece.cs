using System;
using Board.PiecePosition.RotationMath;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Board.Pieces
{
    public interface IPiece
    {
        public EPiece PieceType { get; }
        public TileBase Tile();
        public Vector2Int[] CurrentShapeMap();
        public bool HasNoWallKick(Func<Vector2Int, bool> checkFreePlace);
        public void AddRotation(Direction direction);
        public void SubRotation(Direction direction);
        public void Destroy();
    }
}