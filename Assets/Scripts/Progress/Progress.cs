using UnityEngine;

namespace Progress
{
    public abstract class Progress<T> : MonoBehaviour, IProgress<T>
    {

        protected T CurrentValue;
        
        public void OnEnable()
        {
            TetrisController.OnGameStart += StartNewProgress;
            SubscribeProgressAction();
        }

        public void OnDisable()
        {
            TetrisController.OnGameStart -= StartNewProgress;
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