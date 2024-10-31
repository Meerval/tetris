using UnityEngine;

namespace PositionCalculator
{
    public class ShiftClcRight : ShiftShapeMap
    {
        public ShiftClcRight(Vector2Int currentPiecePosition) : base(currentPiecePosition)
        {
        }

        public override Vector2Int GetNewPosition()
        {
            return GetNewPositionOf(Vector2Int.right);
        }
    }
}