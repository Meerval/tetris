using Board.Pieces;
using Systems.Storage;
using UnityEngine;

namespace Board.Data.Objects
{
    public class Level : DisplayableTetrisDataSingleton<int, Level>
    {
        private int _currentStep;
        private int _stepCount;
        protected override IStorable StorableTetrisData => null;

        protected override void StartNewProgress()
        {
            CurrentValue = 1;
            _currentStep = 0;
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
            if (_currentStep < _stepCount)
            {
                Debug.Log($"Step updated: {_currentStep}/{_stepCount}");
                return;
            }

            CurrentValue += 1;
            _currentStep = 1;
            _stepCount += 2;
            Debug.Log($"Level updated: {CurrentValue}");
            EventsHub.OnLevelUp.Trigger();
            DisplayCurrentValue();
        }
    }
}