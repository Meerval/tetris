using UnityEngine;

namespace Timer
{
    public class TimerOfPieceHardDrop : TimerOfTetris<TimerOfPieceHardDrop>
    {
        public override void ResetTimer()
        {
            UpdateTimeout();
            Debug.Log($"Timer of piece hard drop timer resetted\n{this}");
        }
    }
}