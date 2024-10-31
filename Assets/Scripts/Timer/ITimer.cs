using System;

namespace Timer
{
    public interface ITimer
    {
        bool IsInProgress();
        bool IsTimedOut();
        void UpdateTimeout();
        void UpdateDelay(Func<float,float> delayFunc);
    }
}