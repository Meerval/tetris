using Systems.Chrono.Timer;
using UnityEngine;

namespace Board.Timers
{
    public class TimerOfSingleClick : TimerOfTetris<TimerOfSingleClick>
    {
        public override void ResetTimer()
        {
            UpdateTimeout();
            Debug.Log($"Timer of singleClick was resetted\n{this}");
        }

    }
}