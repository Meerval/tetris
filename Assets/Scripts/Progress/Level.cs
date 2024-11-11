using Event;
using Pieces;
using UnityEngine;

namespace Progress
{
    public class Level : Progress<int>
    {
        private int _currentStep;
        private int _stepCount;

        protected override void StartNewProgress()
        {
            CurrentValue = 0;
            _currentStep = 0;
            _stepCount = 5;
        }

        protected override void SubscribeProgressAction()
        {
            EventHab.OnSpawnPiece.AddSubscriber(StepUp);
        }

        protected override void UnsubscribeProgressAction()
        {
            EventHab.OnSpawnPiece.RemoveSubscriber(StepUp);
        }

        private void StepUp(IPiece _)
        {
            _currentStep += 1;
            Debug.Log($"Step updated: {_currentStep}/{_stepCount}");
            if (_currentStep <= _stepCount - 1) return;
            CurrentValue += 1;
            _currentStep = 0;
            Debug.Log($"Level updated: {CurrentValue}");
            EventHab.OnLevelUp.Trigger();
        }
    }
}