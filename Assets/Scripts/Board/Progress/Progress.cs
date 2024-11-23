using Systems.Events;
using Templates;
using Templates.Singleton;
using UnityEngine;

namespace Board.Progress
{
    public abstract class Progress<T,V> : MonoBehaviourSingleton<V>, IProgress<T> where V : MonoBehaviour
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