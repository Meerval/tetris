using UnityEngine;

namespace Board.PiecePosition
{
    public interface IShapeMap
    {
        Vector2Int[] GetRotatedShapeMap();
    }
}