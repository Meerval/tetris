using UnityEngine;

namespace Pieces
{
    public interface IPredictedPiece : IPiece
    {
        Vector2Int MonitorOffset();
    }
}