using UnityEngine;

namespace Timer
{
    public class TimerOfPieceMove : TimerOfTetris<TimerOfPieceMove>
    {
        public override void ResetTimer()
        {
            UpdateTimeout();
            Debug.Log($"Timer of piece move resetted\n{this}");
        }

    }
}