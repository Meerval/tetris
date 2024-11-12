using Event;
using UnityEngine;

namespace Progress
{
    public class Score : Progress<int>
    {
        private const int ScoreCoefficient = 100;
      
        protected override void StartNewProgress()
        {
            CurrentValue = 0;
        }

        protected override void SubscribeProgressAction()
        {
            EventHab.OnScoreUp.AddSubscriber(UpdateScore);
        }

        protected override void UnsubscribeProgressAction()
        {
            EventHab.OnScoreUp.RemoveSubscriber(UpdateScore);
        }
          
        private void UpdateScore(int removedLinesCount)
        {
            if (removedLinesCount == 0) return;
            CurrentValue += 
                ScoreCoefficient * removedLinesCount * Bonus(removedLinesCount) * TetrisProgress.Instance.Level();
            Debug.Log($"Score Updated: {CurrentValue}");
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