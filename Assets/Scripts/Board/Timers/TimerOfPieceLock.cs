using Systems.Chrono.Timer;
using UnityEngine;

namespace Board.Timers
{
    public class TimerOfPieceLock : TimerSingleton<TimerOfPieceLock>
    {
        public override void ResetTimer()
        {
            UpdateTimeout();
            Debug.Log($"Timer of piece lock resetted\n{this}");
        }

    }
}