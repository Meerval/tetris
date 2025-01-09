using Board;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Menu
{
    public class SettingsMenuController : MonoBehaviour
    {
        [SerializeField] private GameObject shiftLeft;
        [SerializeField] private GameObject shiftRight;
        [SerializeField] private GameObject shiftDown;
        [SerializeField] private GameObject rotateLeft;
        [SerializeField] private GameObject rotateRight;

        private void Start()
        {
            TextMeshProUGUI shiftLeftTxt = shiftLeft.GetComponentInChildren<TextMeshProUGUI>();
            shiftLeftTxt.text = shiftLeftTxt.text.Replace("%", KeyMap.KeyShiftLeft.ToString());
            
            TextMeshProUGUI shiftRightTxt = shiftRight.GetComponentInChildren<TextMeshProUGUI>();
            shiftRightTxt.text = shiftRightTxt.text.Replace("%", KeyMap.KeyShiftRight.ToString());
            
            TextMeshProUGUI shiftDownTxt = shiftDown.GetComponentInChildren<TextMeshProUGUI>();
            shiftDownTxt.text = shiftDownTxt.text.Replace("%", KeyMap.KeyShiftDown.ToString());
            
            TextMeshProUGUI rotateLeftTxt = rotateLeft.GetComponentInChildren<TextMeshProUGUI>();
            rotateLeftTxt.text = rotateLeftTxt.text.Replace("%", KeyMap.KeyRotateLeft.ToString());
            
            TextMeshProUGUI rotateRightTxt = rotateRight.GetComponentInChildren<TextMeshProUGUI>();
            rotateRightTxt.text = rotateRightTxt.text.Replace("%", KeyMap.KeyRotateRight.ToString());
        }

        public void LoadMainMenu()
        {
            SceneManager.LoadScene("MainMenu");
            Debug.Log("Scene 'Main Menu' loaded");
        }

        private void Update()
        {
            if (!Input.GetKeyDown(KeyMap.KeyContinue)) return;
            Debug.Log($"[KEY PRESS] {KeyMap.KeyContinue.ToString()}");
            LoadMainMenu();
        }
    }
}