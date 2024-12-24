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
            EventsHub.OnGameOver.AddSubscriber(Init, 1);
        }

        private void OnDisable()
        {
            EventsHub.OnGameOver.RemoveSubscriber(Init);
        }

        private void Init(EGameOverReason reason)
        {
            switch (reason)
            {
                case EGameOverReason.GridFilled:
                    EventsHub.OnStageChanged.Trigger(ETetrisScene.OnMenuOfGameOver);
                    SceneManager.LoadScene("MainMenu");
                    break;
                case EGameOverReason.AsWill:
                    _controller.SetNewGame();
                    Debug.Log("New game started by keyboard");
                    break;
            }
        }

        private void Update()
        {
            
            // TODO: doesn't work correctly
            _controller.DetectAndExecuteNewGame();
        }
    }
}