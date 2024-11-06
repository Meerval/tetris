using System;
using PiecePosition;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Pieces
{
    public interface IPiece
    {
        TileBase Tile();
        Vector2Int[] CurrentShapeMap();
        bool HasNoWallKick(Func<Vector2Int, bool> checkFreePlace);
        void AddRotation(Direction direction);
        void SubRotation(Direction direction);
        void Destroy();
    }
}