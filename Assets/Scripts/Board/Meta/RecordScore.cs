using System;
using System.Collections.Generic;
using Systems.Events;
using Systems.Storage.POCO;
using UnityEngine;

namespace Board.Meta
{
    public class RecordScore : DisplayableProgress<long, RecordScore>, IStorable
    {
        public string Id => "RecordScore";

        public StorableData StorableData => new(Id, new Dictionary<string, object>
            {
                { "Score", CurrentValue },
                { "UserName", "Admin" },
                { "Date", DateTime.Now }
            }
        );

        public void Load(StorableData loadData)
        {
            if (loadData.TryParseLong("Score", out long currentValue)) CurrentValue = currentValue;
            else LoadInitial();
        }

        public void LoadInitial()
        {
            CurrentValue = 0;
        }

        protected override void StartNewDisplayableProgress()
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
    }
}