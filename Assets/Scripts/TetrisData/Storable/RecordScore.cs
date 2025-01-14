﻿using System.Collections.Generic;
using Systems.Chrono;
using Systems.Parsers.Object;
using Systems.Storage;
using Systems.Storage.POCO;
using UnityEngine;

namespace TetrisData.Storable
{
    public class RecordScore : TetrisDataSingleton<long, RecordScore>
    {
        private Timestamp _timestamp = Timestamp.Empty;

        protected override IStorable StorableTetrisData => new RecordScoreStorable();
        protected override bool IsResettableByNewGame => false;

        protected override void SubscribeDataAction()
        {
            EventsHub.OnScoreUp.AddSubscriber(UpdateRecordScore, 1);
        }

        protected override void UnsubscribeDataAction()
        {
            EventsHub.OnScoreUp.RemoveSubscriber(UpdateRecordScore);
        }

        private void UpdateRecordScore(int _)
        {
            if (BoardInfo.Instance.Score() < CurrentValue)
            {
                return;
            }
            CurrentValue = BoardInfo.Instance.Score();
            _timestamp = Timestamp.Now;
            Debug.Log($"Record Score Updated: {CurrentValue}");
            Storage.Instance.SaveGame();
        }

        private class RecordScoreStorable : IStorable
        {
            private readonly RecordScore _recordScore = Instance;
            public string Id => Key.Id;

            public StorableData StorableData => new(Id, new Dictionary<string, object>
                {
                    { Key.Score, _recordScore.CurrentValue },
                    { Key.Time, _recordScore._timestamp.ToString() }
                }
            );

            public void Load(StorableData data)
            {
                var d = data.Data;
                _recordScore.CurrentValue = ObjectToLong.TryParse(d[Key.Score]).OrElse(InitialData.Score);
                _recordScore._timestamp = ObjectToTimestamp.TryParse(d[Key.Time]).OrElse(InitialData.Time);
            }

            public void LoadInitial()
            {
                _recordScore.CurrentValue = InitialData.Score;
                _recordScore._timestamp = InitialData.Time;
            }

            private struct Key
            {
                public const string Id = "RecordScore";
                public const string Score = "Score";
                public const string Time = "Time";
            }
        }

        private struct InitialData
        {
            public const long Score = 0;
            public static readonly Timestamp Time = Timestamp.Empty;
        }
    }
}