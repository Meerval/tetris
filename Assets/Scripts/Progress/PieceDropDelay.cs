using System;
using Event;
using UnityEngine;

namespace Progress
{
    public class PieceDropDelay : Progress<float, PieceDropDelay>
    {
        private const float BaseDelay = 1.0f;
        private const float DecreaseFactor = 0.5f;

        protected override void StartNewProgress()
        {
            CurrentValue = BaseDelay;
        }

        protected override void SubscribeProgressAction()
        {
            EventsHub.OnLevelUp.AddSubscriber(CountDelay, 1);
        }

        protected override void UnsubscribeProgressAction()
        {
            EventsHub.OnLevelUp.RemoveSubscriber(CountDelay);
        }

        private void CountDelay()
        {
            CurrentValue = BaseDelay / (1 + Mathf.Sqrt(DecreaseFactor * TetrisMeta.Instance.Level()));
            Debug.Log($"Piece drop delay updated: {CurrentValue}");
        }
    }
}