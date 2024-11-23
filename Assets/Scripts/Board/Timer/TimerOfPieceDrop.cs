using Systems.Events;
using UnityEngine;

namespace Board.Timer
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
            EventsHub.OnLevelUp.AddSubscriber(UpdateDelay, 2);
        }

        public void OnDisable()
        {
            EventsHub.OnLevelUp.RemoveSubscriber(UpdateDelay);
        }

        private void UpdateDelay()
        {
            delay = TetrisMeta.Instance.PieceDropDelay();
            Debug.Log($"Delay of piece drop timer updated\n{this}");
        }

        public override void ResetTimer()
        {
            delay = _initDelay;
            Debug.Log($"Timer of piece drop resetted\n{this}");
        }
    }
}