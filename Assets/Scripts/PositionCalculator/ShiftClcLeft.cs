using UnityEngine;

namespace PositionCalculator
{
    public class ShiftClcLeft : ShiftShapeMap
    {
        public ShiftClcLeft(Vector2Int currentPiecePosition) : base(currentPiecePosition)
        {
        }

        public override Vector2Int GetNewPosition()
        {
            return GetNewPositionOf(Vector2Int.left);
        }
    }
}