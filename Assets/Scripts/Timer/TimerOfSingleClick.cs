using UnityEngine;

namespace Timer
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