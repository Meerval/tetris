using UnityEngine;

namespace Timer
{
    public class PieceMoveTimer : TetrisTimer
    {
        public override void ResetTimer()
        {
            UpdateTimeout();
            Debug.Log($"Piece move timer vas reset\n{this}");
        }

    }
}