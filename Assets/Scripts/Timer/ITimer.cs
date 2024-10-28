﻿namespace Timer
{
    public interface ITimer
    {
        bool IsInProgress();
        bool IsTimedOut();
        void UpdateTimeout();
        void MultiplyDelay(float coef);
    }
}