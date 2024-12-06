using Systems.Events;
using Systems.Storage;
using Templates.Singleton;
using UnityEngine;

namespace Board.Meta
{
    public abstract class ProgressSingleton<T1, T2> : MonoBehaviourSingleton<T2>, IProgress<T1>, IStorableDataProvider
        where T2 : MonoBehaviour
    {
        protected T1 CurrentValue;

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

        protected override void AfterAwake()
        {
            Store(StorableTracker);
            StartNewProgress();
        }
        
        public T1 Value()
        {
            return CurrentValue;
        }

        public override string ToString()
        {
            return CurrentValue.ToString();
        }

        public void Store(IStorable storable)
        {
            if (storable == null)
            {
                Debug.LogWarning($"Object='{typeof(T2).FullName}' is not ready to store yet");
                return;
            }
            IStorable.Store(storable);
        }

        protected abstract IStorable StorableTracker { get; }
        protected abstract void StartNewProgress();
        protected abstract void SubscribeProgressAction();
        protected abstract void UnsubscribeProgressAction();
    }
}