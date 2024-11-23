using Systems.Timer;
using UnityEngine;

namespace Board.Timers
{
    public class TimerOfClearLine : TimerOfTetris<TimerOfClearLine>
    {
        public override void ResetTimer()
        {
            UpdateTimeout();
            Debug.Log($"Timer of clear line was resetted\n{this}");
        }

    }
}