using UnityEngine;

namespace Board.PiecePosition
{
    public interface IPosition
    {
        Vector2Int GetNewPosition();
    }
}