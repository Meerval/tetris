using UnityEngine;

namespace PositionCalculator
{
    public class ShiftClcDown : ShiftShapeMap
    {
        public ShiftClcDown(Vector2Int currentPiecePosition) : base(currentPiecePosition)
        {
        }

        public override Vector2Int GetNewPosition()
        {
            return GetNewPositionOf(Vector2Int.down);
        }
    }
}