using Board.Data;
using UnityEngine;

namespace Board
{
    public class GameOver : MonoBehaviour
    {
        private TetrisController _controller;

        public void Start()
        {
            _controller = GetComponentInChildren<TetrisController>();
            Debug.Log("GameOver class started");
        }

        private void OnEnable()
        {
            EventsHub.OnGameOver.AddSubscriber(StartNewGame, 1);
        }
        
        private void OnDisable()
        {
            EventsHub.OnGameOver.RemoveSubscriber(StartNewGame);
        }

        private void StartNewGame(EGameOverReason reason)
        {
            Debug.Log($"Game Over!\nreason: \"{reason}\"\nlevel: {TetrisInfo.Instance.Level()}, " +
                      $"score: {TetrisInfo.Instance.Score()}");
            _controller.SetNewGame();
        }

        private void Update()
        {
            _controller.DetectAndExecuteNewGame();
        }
    }
}