using UnityEngine;

namespace PiecePosition
{
    public class ShiftRight : Shift
    {
        public ShiftRight(Vector2Int currentPiecePosition) : base(currentPiecePosition)
        {
        }

        public override Vector2Int GetNewPosition()
        {
            return GetNewPositionOf(Vector2Int.right);
        }
    }
}