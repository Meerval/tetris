using System.Collections.Generic;
using Board.Pieces;
using Systems.Parsers.Object;
using Systems.Storage;
using Systems.Storage.POCO;
using UnityEngine;

namespace TetrisData.Storable
{
    public class Level : TetrisDataSingleton<int, Level>
    {
        private int _currentStep;
        private int _stepCount;
        protected override IStorable StorableTetrisData => new LevelStorable();
        protected override bool IsResettableByNewGame => true;

        protected override void SubscribeDataAction()
        {
            EventsHub.OnPieceSpawn.AddSubscriber(StepUp, 1);
        }

        protected override void UnsubscribeDataAction()
        {
            EventsHub.OnPieceSpawn.RemoveSubscriber(StepUp);
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
            _currentStep = InitialData.Step;
            _stepCount += 2;
            Debug.Log($"Level updated: {CurrentValue}");
            EventsHub.OnLevelUp.Trigger();
        }

        private class LevelStorable : IStorable
        {
            private readonly Level _level = Instance;
            public string Id => Key.Id;

            public StorableData StorableData => new(Id, new Dictionary<string, object>
                {
                    { Key.Level, _level.CurrentValue },
                    { Key.Step, _level._currentStep },
                    { Key.StepCount, _level._stepCount },
                }
            );

            public void Load(StorableData data)
            {
                _level.CurrentValue = ObjectToInt.TryParse(data.Data[Key.Level]).OrElse(InitialData.Level);
                _level._currentStep = ObjectToInt.TryParse(data.Data[Key.Step]).OrElse(InitialData.Step);
                _level._stepCount = ObjectToInt.TryParse(data.Data[Key.StepCount]).OrElse(InitialData.StepCount);
            }

            public void LoadInitial()
            {
                _level.CurrentValue = InitialData.Level;
                _level._currentStep = InitialData.Step;
                _level._stepCount = InitialData.StepCount;
            }

            private struct Key
            {
                public const string Id = "Level";
                public const string Level = "Level";
                public const string Step = "Step";
                public const string StepCount = "StepCount";
            }
        }

        private struct InitialData
        {
            public const int Level = 1;
            public const int Step = 1;
            public const int StepCount = 10;
        }
    }
}