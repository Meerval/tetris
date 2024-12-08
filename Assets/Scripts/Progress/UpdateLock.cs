﻿using Event;

namespace Progress
{
    public class UpdateLock : Progress<bool, UpdateLock>
    {
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