using System;
using Systems.Events;
using Systems.Storage;
using UnityEngine;

namespace Board.Meta
{
    public class RecordScore : DisplayableProgress<long, RecordScore>, IStorable
    {
        public string Id => "RecordScore";

        public StorableData DataToStore()
        {
            return new StorableData(Id, new object[]{CurrentValue});
        }

        public void RestoreValues(StorableData loadData)
        {
            if (loadData?.Data == null || loadData.Data.Length < 1)
            {
                throw new Exception("Can't restore values.");
            }

            if (long.TryParse(loadData.Data[0].ToString(), out long currentValue)) CurrentValue = currentValue;
        }

        protected override void StartNewDisplayableProgress() { }

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