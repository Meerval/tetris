using UnityEngine;

namespace PositionCalculator
{
    public class ShiftPositionCalculatorRight : ShiftPositionCalculator
    {
        public ShiftPositionCalculatorRight(Vector2Int[] currentPiecePosition) : base(currentPiecePosition)
        {
        }

        public override Vector2Int[] GetNewPiecePosition()
        {
            return GetNewPositionOf(Vector2Int.right);
        }
    }
}