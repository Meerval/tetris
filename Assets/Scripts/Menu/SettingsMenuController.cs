using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Board;
using Menu.SettingsMenuButtonSetters;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Menu
{
    public class SettingsMenuController : MonoBehaviour
    {
        [SerializeField] private List<KeySetter> keySetters;
        private bool _isCoroutine;

        public void LoadMainMenu()
        {
            SceneManager.LoadScene("MainMenu");
            Debug.Log("Scene 'Main Menu' loaded");
        }

        private void Update()
        {
            if (_isCoroutine) return;
            StartCoroutine(Coroutine());
        }

        private IEnumerator Coroutine()
        {
            _isCoroutine = true;
            yield return new WaitUntil(() => keySetters.All(keySetter => !keySetter.IsListening()) && !Input.anyKey);
            if (Input.GetKeyDown(KeyMap.KeyContinue))
            {
                Debug.Log($"[KEY PRESS] {KeyMap.KeyContinue.ToString()}");
                LoadMainMenu();
            }
            _isCoroutine = false;
        }
    }
}