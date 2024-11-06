using System;
using UnityEngine;

namespace Progress
{
    public class PieceDropDelay : Progress<float>
    {
        private const float BaseDelay = 1.0f;
        private const float DecreaseFactor = 0.5f;
        
        public static Action<float> OnUpdated;
        
        protected override void StartNewProgress()
        {
            CurrentValue = BaseDelay;
        }

        protected override void SubscribeProgressAction()
        {
            Level.OnLevelUp += CountDelay;
        }

        protected override void UnsubscribeProgressAction()
        {
            Level.OnLevelUp -= CountDelay;
        }

        private void CountDelay()
        {
            CurrentValue = BaseDelay / (1 + DecreaseFactor * Mathf.Sqrt(TetrisProgressController.Instance.Level()));
            Debug.Log($"Piece drop delay updated: {CurrentValue}");
            OnUpdated?.Invoke(CurrentValue);
        }
    }
}