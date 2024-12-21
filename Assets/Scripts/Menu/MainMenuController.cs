using Board;
using Board.Data;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Menu
{
    public class MainMenuController : MonoBehaviour
    {
        [SerializeField] private GameObject gameOverTxtPrefab;
        [SerializeField] private GameObject continueButtonPrefab;
        [SerializeField] private Transform canvasTransform;

        private void Start()
        {
            // if (_info.TetrisStage() != ETetrisStage.OnPauseMenu) return;
            if (continueButtonPrefab == null || canvasTransform == null)
            {
                Debug.LogError("Button prefab or canvasTransform not set!");
                return;
            }

            GameObject newButton = Instantiate(continueButtonPrefab, canvasTransform);
            Button buttonComponent = newButton.GetComponent<Button>();

            if (buttonComponent == null) Debug.LogError("Button component not found on prefab!");

            buttonComponent.onClick.RemoveAllListeners();
            buttonComponent.onClick.AddListener(Continue);
        }

        public void StartNewGame()
        {
            SceneManager.LoadScene("Tetris");
            EventsHub.OnStageChanged.Trigger(ETetrisStage.OnGame);
            EventsHub.OnGameOver.Trigger(EGameOverReason.AsWill);
            Debug.Log("Scene 'Tetris' loaded with New Game");
        }

        public void Continue()
        {
            SceneManager.LoadScene("Tetris");
            EventsHub.OnStageChanged.Trigger(ETetrisStage.OnGame);
            Debug.Log("Scene 'Tetris' loaded");
        }

        public void OpenSettings()
        {
            Debug.Log("Settings Button Pressed");
        }

        public void QuitGame()
        {
            Debug.Log("Quit Button Pressed");
            Application.Quit();
        }

        private void Update()
        {
            // if (_info.TetrisStage() != ETetrisStage.OnPauseMenu) return;
            if (!Input.GetKeyDown(KeyMap.KeyContinue)) return;
            Debug.Log($"[KEY PRESS] {KeyMap.KeyContinue.ToString()}");
            EventsHub.OnStageChanged.Trigger(ETetrisStage.OnGame);
            SceneManager.LoadScene("Tetris");
            Debug.Log("Game continued by keyboard");
        }
    }
}