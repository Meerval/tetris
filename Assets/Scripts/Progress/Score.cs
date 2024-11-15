using Event;
using UnityEngine;

namespace Progress
{
    public class Score : DisplayableProgress<int>
    {
        private const int ScoreCoefficient = 100;

        protected override void StartNewDisplayableProgress()
        {
            CurrentValue = 0;
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
            CurrentValue += ScoreCoefficient * removedLines * Bonus(removedLines) * TetrisMeta.Instance.Level();
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