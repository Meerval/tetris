namespace Systems.Chrono.Timer
{
    public interface ITimer
    {
        bool IsInProgress();
        bool IsTimedOut();
        void UpdateTimeout();
        void ResetTimer();
    }
}