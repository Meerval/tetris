using UnityEngine;

namespace PositionCalculator
{
    public class ShiftPositionCalculatorLeft : ShiftPositionCalculator
    {
        public ShiftPositionCalculatorLeft(Vector2Int[] currentPiecePosition) : base(currentPiecePosition)
        {
        }

        public override Vector2Int[] GetNewPiecePosition()
        {
            return GetNewPositionOf(Vector2Int.left);
        }
    }
}