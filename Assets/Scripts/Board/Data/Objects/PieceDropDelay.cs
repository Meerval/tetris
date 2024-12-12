using System.Collections.Generic;
using Systems.Parsers.Object;
using Systems.Storage;
using Systems.Storage.POCO;
using UnityEngine;

namespace Board.Data.Objects
{
    public class PieceDropDelay : TetrisDataSingleton<float, PieceDropDelay>
    {
        private const float DecreaseFactor = 0.5f;
        
        protected override IStorable StorableTetrisData => new PieceDropDelayStorable();
        protected override bool IsResettableByNewGame => true;
        
        
        protected override void SubscribeDataAction()
        {
            EventsHub.OnLevelUp.AddSubscriber(CountDelay, 1);
        }

        protected override void UnsubscribeDataAction()
        {
            EventsHub.OnLevelUp.RemoveSubscriber(CountDelay);
        }

        private void CountDelay()
        {
            CurrentValue = InitialData.BaseDelay / (1 + Mathf.Sqrt(DecreaseFactor * TetrisInfo.Instance.Level()));
            Debug.Log($"Piece drop delay updated: {CurrentValue}");
            
            Storage.Instance.SaveGame();
        }
        
        private class PieceDropDelayStorable : IStorable
        {
            private readonly PieceDropDelay _delay = Instance;
            public string Id => Key.Id;

            public StorableData StorableData => new(Id, new Dictionary<string, object>
                {
                    { Key.Delay, _delay.CurrentValue }
                }
            );

            public void Load(StorableData data)
            {
                _delay.CurrentValue = ObjectToFloat.TryParse(data.Data[Key.Delay]).OrElse(InitialData.BaseDelay);
            }

            public void LoadInitial()
            {
                _delay.CurrentValue = InitialData.BaseDelay;
            }

            private struct Key
            {
                public const string Id = "PieceDropDelay";
                public const string Delay = "Delay";
            }
        }

        private struct InitialData
        {
            public const float BaseDelay = 1f;
        }
    }
}