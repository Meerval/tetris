using System.Collections.Generic;
using Systems.Chrono;
using Systems.Events;
using Systems.Parsers.Object;
using Systems.Storage;
using Systems.Storage.POCO;
using UnityEngine;

namespace Board.Meta.Objects
{
    public class RecordScore : DisplayableProgressSingleton<long, RecordScore>
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

            public StorableData StorableData => new(Id, new Dictionary<string, object>
                {
                    { Key.Score, _recordScore.Value() },
                    { Key.UserName, _recordScore._username },
                    { Key.Time, _recordScore._timestamp.ToString() }
                }
            );

            public void Load(StorableData data)
            {
                var d = data.Data;
                _recordScore.CurrentValue = ObjectToLong.TryParse(d[Key.Score]).OrElse(InitialData.Score);
                _recordScore._username = ObjectToString.TryParse(d[Key.UserName]).OrElse(InitialData.UserName);
                _recordScore._timestamp = ObjectToTimestamp.TryParse(d[Key.Time]).OrElse(InitialData.Time);
            }

            public void LoadInitial()
            {
                _recordScore.CurrentValue = InitialData.Score;
                _recordScore._username = InitialData.UserName;
                _recordScore._timestamp = InitialData.Time;
            }

            private struct InitialData
            {
                public const long Score = 0;
                public const string UserName = "-";
                public static readonly Timestamp Time = Timestamp.Empty;
            }

            private struct Key
            {
                public const string Score = "Score";
                public const string UserName = "UserName";
                public const string Time = "Time";
            }
        }
    }
}