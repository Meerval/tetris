using System;
using System.Collections.Generic;
using System.Linq;
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
        protected abstract List<KeySetter> OtherKeySetters { get; }

        private Button _button;
        private TextMeshProUGUI _text;
        private TextMeshProUGUI _info;


        private void Start()
        {
            _isListening = false;
            SetButtonTextAndInfo();
            SetButtonBehaviour();
        }

        private void SetButtonTextAndInfo()
        {
            _text = GetComponentsInChildren<TextMeshProUGUI>().ToList().Find(x => x.name == "Text");
            _info = GetComponentsInChildren<TextMeshProUGUI>().ToList().Find(x => x.name == "Info");
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
            OtherKeySetters.ForEach(ks => { ks.TerminateButtonAction(); });
            _isListening = true;
            _button.interactable = false;
            _info.text = "Press a new key for this action";
            if (!KeyMap.IsKeyDefault(KeyInfo.Action)) _info.text += "\nPress <b>Backspace</b> to return default key";
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveListener(OnPressButton);
        }

        private void Update()
        {
            if (!_isListening) return;

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                TerminateButtonAction();
                return;
            }

            if (Input.GetKeyDown(KeyCode.Backspace))
            {
                Debug.Log($"Key for action '{KeyInfo.Action}' resetted to default");
                KeyCode defaultKeyCode = KeyMap.ResetKey(KeyInfo.Action);
                ReplaceText(defaultKeyCode.ToString());
                TerminateButtonAction();
                return;
            }

            foreach (KeyCode keyCode in Enum.GetValues(typeof(KeyCode)))
            {
                if (!Input.GetKeyDown(keyCode)) continue;
                if (!IsKeyboardKey(keyCode)) continue;
                if (KeyMap.CurrentConfig.Keys.Any(key => key.Code == (int)keyCode))
                {
                    _info.text = $"Key <b>{keyCode}</b> is already taken!\nPress another key for this action";
                    continue;
                }

                Debug.Log($"Selected key: {keyCode}");
                KeyMap.UpdateKey(KeyInfo.Action, keyCode);
                ReplaceText(keyCode.ToString());
                TerminateButtonAction();
                return;
            }
        }

        private void TerminateButtonAction()
        {
            _button.interactable = true;
            _isListening = false;
            _info.text = "";
        }

        private bool IsKeyboardKey(KeyCode key)
        {
            return key is >= KeyCode.A and <= KeyCode.Z or
                >= KeyCode.Alpha0 and <= KeyCode.Alpha9 or
                >= KeyCode.Keypad0 and <= KeyCode.Keypad9 or
                >= KeyCode.F1 and <= KeyCode.F12 or
                KeyCode.UpArrow or KeyCode.DownArrow or KeyCode.LeftArrow or KeyCode.RightArrow or
                KeyCode.Space or KeyCode.LeftShift or KeyCode.RightShift;
        }

        public bool IsListening()
        {
            return _isListening;
        }
    }
}