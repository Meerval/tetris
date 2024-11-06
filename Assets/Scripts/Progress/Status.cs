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
            TetrisController.OnGameStart += SetStartGame;
            TetrisController.OnGameOver += SetOverGame;
        }

        protected override void UnsubscribeProgressAction()
        {
            TetrisController.OnGameStart -= SetStartGame;
            TetrisController.OnGameOver -= SetOverGame;
        }

        private void SetStartGame()
        {
            CurrentValue = State.InProgress;
            Debug.Log("Game Started!");
        }

        private void SetOverGame(string gameOverReason)
        {
            CurrentValue = State.Over;
            Debug.Log($"Game Over!\nreason: \"{gameOverReason}\"\nlevel: {TetrisProgressController.Instance.Level()}, " +
                      $"score: {TetrisProgressController.Instance.Score()}");
        }
    }
}