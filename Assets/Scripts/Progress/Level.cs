using Event;
using Pieces;
using UnityEngine;

namespace Progress
{
    public class Level : DisplayableProgress<int, Level>
    {
        private int _currentStep;
        private int _stepCount;

        protected override void StartNewDisplayableProgress()
        {
            CurrentValue = 1;
            _currentStep = 1;
            _stepCount = 10;
        }

        protected override void SubscribeProgressAction()
        {
            EventsHub.OnSpawnPiece.AddSubscriber(StepUp, 1);
        }

        protected override void UnsubscribeProgressAction()
        {
            EventsHub.OnSpawnPiece.RemoveSubscriber(StepUp);
        }

        private void StepUp(IPiece _)
        {
            _currentStep += 1;
            Debug.Log($"Step updated: {_currentStep}/{_stepCount}");
            if (_currentStep <= _stepCount + 1) return;
            CurrentValue += 1;
            _stepCount += 2;
            _currentStep = 1;
            Debug.Log($"Level updated: {CurrentValue}");
            EventsHub.OnLevelUp.Trigger();
            DisplayCurrentValue();
        }
        
    }
}