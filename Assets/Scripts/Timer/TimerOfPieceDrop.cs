using Event;
using Progress;
using UnityEngine;

namespace Timer
{
    public class TimerOfPieceDrop : TimerOfTetris<TimerOfPieceDrop>
    {
        private float _initDelay;

        private void Start()
        {
            _initDelay = delay;
        }

        public void OnEnable()
        {
            EventHab.OnLevelUp.AddSubscriber(UpdateDelay, 2);
        }

        public void OnDisable()
        {
            EventHab.OnLevelUp.RemoveSubscriber(UpdateDelay);
        }

        private void UpdateDelay()
        {
            delay = TetrisProgressController.Instance.PieceDropDelay();
            Debug.Log($"Delay of piece drop timer updated\n{this}");
        }

        public override void ResetTimer()
        {
            delay = _initDelay;
            UpdateTimeout();
            Debug.Log($"Timer of piece drop resetted\n{this}");
        }
    }
}