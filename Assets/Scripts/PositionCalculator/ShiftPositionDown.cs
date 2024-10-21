using UnityEngine;

namespace PositionCalculator
{
    public class ShiftPositionCalculatorDown : ShiftPositionCalculator
    {
        public ShiftPositionCalculatorDown(Vector2Int[] currentPiecePosition) : base(currentPiecePosition)
        {
        }

        public override Vector2Int[] GetNewPiecePosition()
        {
            return GetNewPositionOf(Vector2Int.down);
        }
    }
}