using Board;
using Board.Data;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Menu
{
    public class MainMenuController : MonoBehaviour
    {
        public void StartGame()
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
            if (!Input.GetKeyDown(KeyMap.KeyContinue)) return;
            Debug.Log($"[KEY PRESS] {KeyMap.KeyContinue.ToString()}");
            EventsHub.OnStageChanged.Trigger(ETetrisStage.OnGame);
            SceneManager.LoadScene("Tetris");
            Debug.Log("Game continued by keyboard");
        }
    }
}