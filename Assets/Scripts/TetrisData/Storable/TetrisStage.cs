using System;
using System.Collections.Generic;
using Systems.Parsers.Object;
using Systems.Storage;
using Systems.Storage.POCO;

namespace TetrisData.Storable
{
    public class TetrisStage : TetrisDataSingleton<ETetrisScene, TetrisStage>
    {
        protected override IStorable StorableTetrisData => new StateStorable();
        protected override bool IsResettableByNewGame => true;

        protected override void SubscribeDataAction()
        {
            EventsHub.OnStageChanged.AddSubscriber(UpdateStage);
        }

        protected override void UnsubscribeDataAction()
        {
            EventsHub.OnStageChanged.RemoveSubscriber(UpdateStage);
        }
        
        private void UpdateStage(ETetrisScene scene)
        {
            CurrentValue = scene;
        }

        private class StateStorable : IStorable
        {
            private readonly TetrisStage _tetrisStage = Instance;
            public string Id => Key.Id;

            public StorableData StorableData => new(Id, new Dictionary<string, object>
                {
                    { Key.Stage, _tetrisStage.CurrentValue.ToString() }
                }
            );

            public void Load(StorableData data)
            {
                string stageName = ObjectToString.TryParse(data.Data[Key.Stage]).OrElse(InitialData.Stage.ToString());
                _tetrisStage.CurrentValue = Enum.TryParse(stageName, out ETetrisScene stage) ? stage : InitialData.Stage;
            }

            public void LoadInitial()
            {
                _tetrisStage.CurrentValue = InitialData.Stage;
            }

            private struct Key
            {
                public const string Id = "TetrisStage";
                public const string Stage = "Stage";
            }
        }

        private struct InitialData
        {
            public const ETetrisScene Stage = ETetrisScene.OnMenuOfFirstEnter;
        }
    }
}