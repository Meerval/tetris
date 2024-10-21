using UnityEngine;

namespace PositionCalculator
{
    public interface IPositionCalculator
    {
        public Vector2Int[] GetNewPiecePosition();
    }
}