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
            PieceDropDelay.OnUpdated += UpdateDelay;
        }

        public void OnDisable()
        {
            PieceDropDelay.OnUpdated -= UpdateDelay;
        }

        private void UpdateDelay(float newDelay)
        {
            delay = newDelay;
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