using Systems.Storage;
using Templates.Singleton;
using UnityEngine;

namespace Board.Data
{
    public abstract class TetrisDataSingleton<T1, T2> : MonoBehaviourSingleton<T2>, ITetrisData<T1>, IStorableDataProvider
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
            Store(StorableTetrisData);
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

        protected abstract IStorable StorableTetrisData { get; }
        protected abstract void StartNewProgress();
        protected abstract void SubscribeProgressAction();
        protected abstract void UnsubscribeProgressAction();
    }
}