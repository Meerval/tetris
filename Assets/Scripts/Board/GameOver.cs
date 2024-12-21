using TetrisData;
using UnityEngine;
using UnityEngine.SceneManagement;

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
            Debug.Log($"Game Over!\nreason: \"{reason}\"\nlevel: {BoardInfo.Instance.Level()}, " +
                      $"score: {BoardInfo.Instance.Score()}");
            _controller.SetNewGame();
            if (reason == EGameOverReason.GridFilled)
            {
                SceneManager.LoadScene("MainMenu");
            }
        }

        private void Update()
        {
            _controller.DetectAndExecuteNewGame();
        }
    }
}