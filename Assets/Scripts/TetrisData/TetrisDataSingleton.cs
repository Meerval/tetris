using Systems.Storage;
using Templates.Singleton;
using UnityEngine;

namespace TetrisData
{
    public abstract class TetrisDataSingleton<T1, T2> : MonoBehaviourSingleton<T2>, ITetrisData<T1>,
        IStorableDataProvider
        where T2 : MonoBehaviour
    {
        protected virtual T1 CurrentValue { get; set; }

        public void OnEnable()
        {
            EventsHub.OnNewGameStart.AddSubscriber(InitForNewGame);
            SubscribeDataAction();
        }

        public void OnDisable()
        {
            EventsHub.OnNewGameStart.RemoveSubscriber(InitForNewGame);
            UnsubscribeDataAction();
        }

        protected override void AfterAwake()
        {
            Store(StorableTetrisData);
        }

        protected virtual void InitForNewGame()
        {
            if (IsResettableByNewGame) StorableTetrisData.LoadInitial();
        }

        public T1 Value()
        {
            return CurrentValue;
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
        protected abstract bool IsResettableByNewGame { get; }
        protected abstract void SubscribeDataAction();
        protected abstract void UnsubscribeDataAction();
    }
}