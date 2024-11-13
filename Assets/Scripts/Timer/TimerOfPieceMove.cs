using UnityEngine;

namespace Timer
{
    public class TimerOfPieceMove : TimerOfTetris<TimerOfPieceMove>
    {
        private float _initDelay;
        private int _updateCount;
        [SerializeField] private float accelerationFactor;
        [SerializeField] private float minDelay;

        private void Start()
        {
            _initDelay = delay;
            _updateCount = 0;
        }

        public override void UpdateTimeout()
        {
            delay = Mathf.Max(minDelay, delay * Mathf.Pow( accelerationFactor, _updateCount)) ;
            base.UpdateTimeout();
            _updateCount++;
        }

        public override void ResetTimer()
        {
            delay = _initDelay;
            _updateCount = 0;
            Debug.Log($"Timer of piece move resetted\n{this}");
        }
    }
}