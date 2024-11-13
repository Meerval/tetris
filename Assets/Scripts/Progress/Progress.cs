using Event;
using UnityEngine;

namespace Progress
{
    public abstract class Progress<T> : MonoBehaviour, IProgress<T>
    {

        protected T CurrentValue;
        
        public void OnEnable()
        {
            EventsHub.OnGameStart.AddSubscriber(StartNewProgress);
            SubscribeProgressAction();
        }

        public void OnDisable()
        {
            EventsHub.OnGameStart.RemoveSubscriber(StartNewProgress);
            UnsubscribeProgressAction();
        }

        public void Start()
        {
            StartNewProgress();
        }

        public T Value()
        {
            return CurrentValue;
        }
        
        protected abstract void StartNewProgress();
        protected abstract void SubscribeProgressAction();
        protected abstract void UnsubscribeProgressAction();

        public override string ToString()
        {
            return CurrentValue.ToString();
        }
    }
}