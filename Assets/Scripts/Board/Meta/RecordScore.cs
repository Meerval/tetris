using System;
using Systems.Events;
using Systems.Storage.POCO;
using Templates.POCO;
using UnityEngine;

namespace Board.Meta
{
    public class RecordScore : DisplayableProgress<long, RecordScore>, IStorable
    {
        public string Id => "RecordScore";
        public StorableData StorableData => new(Id, new RecordScoreData(CurrentValue));

        public void Load(StorableData loadData)
        {
            CurrentValue = ((RecordScoreData)loadData.Data).Score;
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

    [Serializable]
    public class RecordScoreData : Poco<object>
    {
        public long Score { get; set; }
        public string UserName { get; set; }
        public DateTime Date { get; set; }

        public RecordScoreData(long score)
        {
            Score = score;
            UserName = "Admin";
            Date = DateTime.Now;
        }

        public RecordScoreData()
        {
            Score = 0;
            UserName = "-";
            Date = DateTime.Now;
        }
    }
}