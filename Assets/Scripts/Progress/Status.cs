using Event;
using UnityEngine;

namespace Progress
{
    public class Status : Progress<State>
    {
        protected override void StartNewProgress()
        {
            CurrentValue = State.InExpectation;
        }

        protected override void SubscribeProgressAction()
        {
            EventHab.OnGameStart.AddSubscriber(SetStartGame);
            EventHab.OnGameOver.AddSubscriber(SetOverGame);
        }

        protected override void UnsubscribeProgressAction()
        {
            EventHab.OnGameStart.RemoveSubscriber(SetStartGame);
            EventHab.OnGameOver.RemoveSubscriber(SetOverGame);
        }

        private void SetStartGame()
        {
            CurrentValue = State.InProgress;
            Debug.Log("Game Started!");
        }

        private void SetOverGame(EGameOverReason eGameOverReason)
        {
            CurrentValue = State.Over;
            Debug.Log($"Game Over!\nreason: \"{eGameOverReason}\"\nlevel: {TetrisProgressController.Instance.Level()}, " +
                      $"score: {TetrisProgressController.Instance.Score()}");
        }
    }
}