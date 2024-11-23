using UnityEngine;

namespace Board.Pieces
{
    public interface IPredictedPiece : IPiece
    {
        Vector2Int MonitorOffset();
    }
}