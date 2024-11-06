using UnityEngine;

namespace PiecePosition
{
    public interface IPosition
    {
        Vector2Int GetNewPosition();
    }
}