using System.Collections.Generic;
using Systems.Chrono;
using Systems.Events;
using Systems.Storage;
using Systems.Storage.POCO;
using UnityEngine;

namespace Board.Meta.Objects
{
    public class RecordScore : DisplayableProgress<long, RecordScore>
    {
        private Timestamp _timestamp;
        private string _username;

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
            _timestamp = Timestamp.Now;
            _username = "Admin";
            Debug.Log($"Record Score Updated: {CurrentValue}");
            DisplayCurrentValue();
        }

        private class RecordScoreTracker : IStorable
        {
            private readonly RecordScore _recordScore = Instance;


            public string Id => "RecordScore";
            private const string Score = "Score";
            private const string UserName = "UserName";
            private const string Time = "Time";

            private Dictionary<string, object> Data => new()
            {
                { Score, _recordScore.Value() },
                { UserName, _recordScore._username },
                { Time, _recordScore._timestamp.ToString() }
            };

            private Dictionary<string, object> InitData => new()
            {
                { Score, 0 },
                { UserName, "-" },
                { Time, Timestamp.Empty }
            };

            public StorableData StorableData => new(Id, Data);

            public void Load(StorableData data)
            {
                _recordScore.CurrentValue = data.TryParse(Score, out long value)
                    ? value
                    : (long)InitData[Score];
                _recordScore._username = data.TryParse(UserName, out string username)
                    ? username
                    : (string)InitData[UserName];
                _recordScore._timestamp = data.TryParse(Time, out Timestamp time)
                    ? time
                    : (Timestamp)InitData[Time];
            }

            public void LoadInitial()
            {
                _recordScore.CurrentValue = (long)InitData[Score];
                _recordScore._username = (string)InitData[UserName];
                _recordScore._timestamp = (Timestamp)InitData[Time];
            }
        }
    }
}