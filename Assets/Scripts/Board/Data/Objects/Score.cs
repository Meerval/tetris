using System.Collections.Generic;
using Systems.Parsers.Object;
using Systems.Storage;
using Systems.Storage.POCO;
using UnityEngine;

namespace Board.Data.Objects
{
    public class Score : DisplayableTetrisDataSingleton<long, Score>
    {
        private const int ScoreCoefficient = 100;
        
        protected override IStorable StorableTetrisData => new ScoreStorable();

        protected override void InitForNewGame()
        {
            CurrentValue = InitialData.Score;
        }

        protected override void SubscribeDisplayableDataAction()
        {
            EventsHub.OnScoreUp.AddSubscriber(UpdateScore);
        }

        protected override void UnsubscribeDisplayableDataAction()
        {
            EventsHub.OnScoreUp.RemoveSubscriber(UpdateScore);
        }

        private void UpdateScore(int removedLines)
        {
            if (removedLines == 0) return;
            CurrentValue += ScoreCoefficient * removedLines * Bonus(removedLines);
            Debug.Log($"Score Updated: {CurrentValue}");
            DisplayCurrentValue();
        }

        private int Bonus(int removedLinesCount)
        {
            int bonus = removedLinesCount switch
            {
                1 => 1,
                2 => 3,
                3 => 5,
                4 => 10,
                _ => 0
            };
            return bonus;
        }

        private class ScoreStorable : IStorable
        {
            private readonly Score _score = Instance;
            public string Id => Key.Id;

            public StorableData StorableData => new(Id, new Dictionary<string, object>
                {
                    { Key.Score, _score.CurrentValue }
                }
            );

            public void Load(StorableData data)
            {
                _score.CurrentValue = ObjectToLong.TryParse(data.Data[Key.Score]).OrElse(InitialData.Score);
            }

            public void LoadInitial()
            {
                _score.CurrentValue = InitialData.Score;
            }

            private struct Key
            {
                public const string Id = "Score";
                public const string Score = "Score";
            }
        }

        private struct InitialData
        {
            public const long Score = 0;
        }
    }
}