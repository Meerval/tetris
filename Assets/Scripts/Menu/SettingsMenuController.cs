using Board;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Menu
{
    public class SettingsMenuController : MonoBehaviour
    {
        public void LoadMainMenu()
        {
            SceneManager.LoadScene("MainMenu");
            Debug.Log("Scene 'Main Menu' loaded");
        }

        private void Update()
        {
            // TODO: написать проверку на нажатость всех клавишь
            if (!Input.GetKeyDown(KeyMap.KeyContinue)) return;
            Debug.Log($"[KEY PRESS] {KeyMap.KeyContinue.ToString()}");
            LoadMainMenu();
        }
    }
}