using System;
using System.Collections.Generic;
using Systems.Events;
using Systems.Storage;
using Systems.Storage.POCO;
using UnityEngine;

namespace Board.Meta.Objects
{
    public class RecordScore : DisplayableProgress<long, RecordScore>
    {
        protected override IStorable StorableTracker => new RecordScoreTracker();

        protected override void StartNewProgress()
        {
        }

        protected override void SubscribeProgressAction()
        {
            EventsHub.OnScoreUp.AddSubscriber(UpdateRecordScore, 1);
        }

        protected override void UnsubscribeProgressAction()
        {
            EventsHub.OnScoreUp.RemoveSubscriber(UpdateRecordScore);
        }

        private void UpdateRecordScore(int _)
        {
            if (TetrisMeta.Instance.Score() < CurrentValue) return;
            CurrentValue = TetrisMeta.Instance.Score();
            Debug.Log($"Record Score Updated: {CurrentValue}");
            DisplayCurrentValue();
        }

        private class RecordScoreTracker : IStorable
        {
            private readonly RecordScore _recordScore = Instance;

            public string Id => "RecordScore";

            public StorableData StorableData => new(Id, new Dictionary<string, object>
                {
                    { "Score", _recordScore.Value() },
                    { "UserName", "Admin" },
                    { "Date", DateTime.Now }
                }
            );

            public void Load(StorableData loadData)
            {
                if (loadData.TryParseLong("Score", out long currentValue)) _recordScore.CurrentValue = currentValue;
                else LoadInitial();
            }

            public void LoadInitial()
            {
                _recordScore.CurrentValue = 0;
            }
        }
    }
}