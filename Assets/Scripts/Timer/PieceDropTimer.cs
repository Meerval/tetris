using Event;
using Progress;
using UnityEngine;

namespace Timer
{
    public class PieceDropTimer : TetrisTimer
    {
        private float _initDelay;

        private void Awake()
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
            Debug.Log($"Piece move timer delay updated\n{this}");
        }

        public override void ResetTimer()
        {
            delay = _initDelay;
            UpdateTimeout();
            Debug.Log($"Piece move timer reset\n{this}");
        }
    }
}