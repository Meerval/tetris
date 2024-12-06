using Board.Data.Objects;
using Systems.Events;
using Systems.Storage;
using UnityEngine;

namespace Board.Data.Objects
{
    public class Score : DisplayableTetrisDataSingleton<long, Score>
    {
        private const int ScoreCoefficient = 100;
        protected override IStorable StorableTracker => null;

        protected override void StartNewProgress()
        {
            CurrentValue = 0L;
        }

        protected override void SubscribeProgressAction()
        {
            EventsHub.OnScoreUp.AddSubscriber(UpdateScore);
        }

        protected override void UnsubscribeProgressAction()
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
    }
}