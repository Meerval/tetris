using System;
using System.Text.RegularExpressions;
using Board;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Menu.SettingsMenuButtonSetters
{
    public abstract class KeySetter : MonoBehaviour
    {
        private bool _isListening;
        protected abstract KeyInfo KeyInfo { get; }

        private Button _button;
        private TextMeshProUGUI _text;

        private void Start()
        {
            _isListening = false;
            SetButtonText();
            SetButtonBehaviour();
        }
        
        private void SetButtonText()
        {
            _text = GetComponentInChildren<TextMeshProUGUI>();
            ReplaceText(KeyInfo.Key);
        }

        private void ReplaceText(string setKey)
        {
            _text.text = Regex.Replace(_text.text, "- .*", $"- {setKey}");
        }

        private void SetButtonBehaviour()
        {
            _button = GetComponent<Button>();
            if (_button == null) Debug.LogError($"Button component not found on prefab '{_button.name}'!");
            _button.onClick.RemoveAllListeners();
            _button.onClick.AddListener(OnPressButton);
        }

        private void OnPressButton()
        {
            _isListening = true;
            Debug.Log("Key Setter is Listening");
        }

        private void Update()
        {
            if (!_isListening) return;
            
            foreach (KeyCode keyCode in Enum.GetValues(typeof(KeyCode)))
            {
                if (!Input.GetKeyDown(keyCode)) continue;
                Debug.Log($"Selected key: {keyCode}");
                ReplaceText(keyCode.ToString());
                KeyMap.UpdateKey(KeyInfo.Action, keyCode);
                _isListening = false;
                return;
            }
        }
    }
}