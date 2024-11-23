using Systems.Timer;
using UnityEngine;

namespace Board.Timers
{
    public class TimerOfPieceLock : TimerOfTetris<TimerOfPieceLock>
    {
        public override void ResetTimer()
        {
            UpdateTimeout();
            Debug.Log($"Timer of piece lock resetted\n{this}");
        }

    }
}