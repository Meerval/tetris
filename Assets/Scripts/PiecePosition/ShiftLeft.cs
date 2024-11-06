using UnityEngine;

namespace PiecePosition
{
    public class ShiftLeft : Shift
    {
        public ShiftLeft(Vector2Int currentPiecePosition) : base(currentPiecePosition)
        {
        }

        public override Vector2Int GetNewPosition()
        {
            return GetNewPositionOf(Vector2Int.left);
        }
    }
}