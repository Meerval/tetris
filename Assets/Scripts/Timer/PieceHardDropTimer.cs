using Event;
using Progress;
using UnityEngine;

namespace Timer
{
    public class PieceHardDropTimer : TetrisTimer
    {
        public override void ResetTimer()
        {
            UpdateTimeout();
            Debug.Log($"Piece hard drop timer reset\n{this}");
        }
    }
}