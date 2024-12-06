using Board.Data;
using Systems.Events;
using Systems.Storage;
using UnityEngine;

namespace Board
{
    public class TetrisBoard : MonoBehaviour
    {
        private IController _controller;

        public void Start()
        {
            _controller = GetComponentInChildren<TetrisController>();
            Debug.Log("TetrisBoard started");
        }

        private void OnEnable()
        {
            EventsHub.OnGameOver.AddSubscriber(GameOver, 1);
        }


        private void OnDisable()
        {
            EventsHub.OnGameOver.RemoveSubscriber(GameOver);
        }

        public void Update()
        {
            _controller.DetectAndExecutePieceRotation();
            _controller.DetectAndExecutePieceShift();
            _controller.DropPieceAsTimeout();
            _controller.SpawnPieceAsWill();
        }

        private void GameOver(EGameOverReason reason)
        {
            Debug.Log($"Game Over!\nreason: \"{reason}\"\nlevel: {TetrisMeta.Instance.Level()}, " +
                      $"score: {TetrisMeta.Instance.Score()}");
            _controller.SetNewGame();
        }
    }
}