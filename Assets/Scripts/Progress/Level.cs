using System;
using UnityEngine;

namespace Progress
{
    public class Level : Progress<int>
    {
        private int _currentStep;
        private int _stepCount;

        public static Action OnLevelUp;

        protected override void StartNewProgress()
        {
            CurrentValue = 0;
            _currentStep = 0;
            _stepCount = 5;
        }

        protected override void SubscribeProgressAction()
        {
            TetrisController.OnStepUp += StepUp;
        }

        protected override void UnsubscribeProgressAction()
        {
            TetrisController.OnStepUp -= StepUp;
        }

        private void StepUp()
        {
            _currentStep += 1;
            Debug.Log($"Step updated: {_currentStep}/{_stepCount}");
            if (_currentStep <= _stepCount) return;
            CurrentValue += 1;
            _currentStep = 1;
            OnLevelUp?.Invoke();
            Debug.Log($"Level updated: {CurrentValue}");
        }
    }
}