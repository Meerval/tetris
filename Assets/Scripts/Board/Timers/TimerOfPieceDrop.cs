using Board.Data;
using Systems.Chrono.Timer;
using Systems.Events;
using UnityEngine;

namespace Board.Timers
{
    public class TimerOfPieceDrop : TimerSingleton<TimerOfPieceDrop>
    {
        private float _initDelay;

        private void Start()
        {
            _initDelay = delay;
            delay = TetrisMeta.Instance.PieceDropDelay();
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