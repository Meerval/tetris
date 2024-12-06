using Systems.Events;
using Systems.Storage;

namespace Board.Meta.Objects
{
    public class UpdateLock : ProgressSingleton<bool, UpdateLock>
    {
        protected override IStorable StorableTracker => null;

        protected override void StartNewProgress()
        {
            CurrentValue = false;
        }

        protected override void SubscribeProgressAction()
        {
            EventsHub.OnWaitCoroutineStart.AddSubscriber(Lock);
            EventsHub.OnWaitCoroutineEnd.AddSubscriber(Unlock);
        }

        protected override void UnsubscribeProgressAction()
        {
            EventsHub.OnWaitCoroutineStart.RemoveSubscriber(Lock);
            EventsHub.OnWaitCoroutineEnd.RemoveSubscriber(Unlock);
        }

        private void Lock()
        {
            CurrentValue = true;
        }

        private void Unlock()
        {
            CurrentValue = false;
        }
    }
}