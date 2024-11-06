using UnityEngine;

namespace PiecePosition
{
    public class ShiftDown : Shift
    {
        public ShiftDown(Vector2Int currentPiecePosition) : base(currentPiecePosition)
        {
        }

        public override Vector2Int GetNewPosition()
        {
            return GetNewPositionOf(Vector2Int.down);
        }
    }
}