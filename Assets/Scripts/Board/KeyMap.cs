using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Systems.FileAccess;
using Systems.Pretty;
using Templates.POCO;
using UnityEngine;

namespace Board
{
    public static class KeyMap
    {
        private const String KeyMapFile = "KeyMap.json";
        private const String KeyMapDefaultFile = "KeyMapDefault.json";
        private static readonly String ConfigPath = Path.Combine(Application.streamingAssetsPath);

        public static readonly KeyConfig CurrentConfig = ReadConfig(KeyMapFile);
        public static readonly KeyConfig DefaultConfig = ReadConfig(KeyMapDefaultFile);

        public static KeyCode KeyRotateLeft = CurrentKeyCodeOf(KeyAction.KeyRotateLeft);
        public static KeyCode KeyRotateRight = CurrentKeyCodeOf(KeyAction.KeyRotateRight);
        public static KeyCode KeyShiftLeft = CurrentKeyCodeOf(KeyAction.KeyShiftLeft);
        public static KeyCode KeyShiftRight = CurrentKeyCodeOf(KeyAction.KeyShiftRight);
        public static KeyCode KeyShiftDown = CurrentKeyCodeOf(KeyAction.KeyShiftDown);
        public static KeyCode KeyNewGame = CurrentKeyCodeOf(KeyAction.KeyNewGame);
        public static KeyCode KeyPause = CurrentKeyCodeOf(KeyAction.KeyPause);
        public static KeyCode KeyContinue = CurrentKeyCodeOf(KeyAction.KeyContinue);

        private static KeyConfig ReadConfig(string fileName)
        {
            KeyConfig keyConfig = new JsonFile<KeyConfig>(fileName, ConfigPath).Read();
            Debug.Log($"Controller settings uploaded: {keyConfig}");
            return keyConfig;
        }

        private static void SaveConfig()
        {
            new JsonFile<KeyConfig>(KeyMapFile, ConfigPath).Overwrite(CurrentConfig);
            Debug.Log($"Controller settings saved: {CurrentConfig}");
        }

        private static KeyCode CurrentKeyCodeOf(KeyAction keyAction)
        {
            return (KeyCode)KeyInfoOf(CurrentConfig, keyAction).Code;
        }

        public static KeyInfo KeyInfoOf(KeyConfig config, KeyAction keyAction)
        {
            return config.Keys.Find(key => key.Action.Equals(keyAction));
        }

        public static KeyCode ResetKey(KeyAction keyAction)
        {
            var keyCode = (KeyCode)KeyInfoOf(DefaultConfig, keyAction).Code;
            UpdateKey(keyAction, keyCode);
            return keyCode;
        }

        public static bool IsKeyDefault(KeyAction keyAction)
        {
            return KeyInfoOf(DefaultConfig, keyAction).Code == KeyInfoOf(CurrentConfig, keyAction).Code;
        }

        public static void UpdateKey(KeyAction keyAction, KeyCode newKeyCode)
        {
            CurrentConfig.Keys.ForEach
            (
                keyInfo =>
                {
                    if (keyInfo.Action == keyAction)
                    {
                        keyInfo.Code = (int)newKeyCode;
                        keyInfo.Key = newKeyCode.ToString();
                        Debug.Log($"{keyAction} action updated: {keyInfo}");
                    }
                    else
                        keyInfo.Code = keyInfo.Code;
                }
            );

            Debug.Log($"Controller settings updated: {new PrettyArray<KeyInfo>(CurrentConfig.Keys)}");

            Type thisClass = typeof(KeyMap);
            FieldInfo fieldInfo = thisClass.GetField(keyAction.ToString(), BindingFlags.Static | BindingFlags.Public);

            if (fieldInfo != null)
            {
                fieldInfo.SetValue(null, newKeyCode);
            }
            else
            {
                Debug.LogError($"Field with name '{keyAction.ToString()}' was not found in this class");
            }

            SaveConfig();
        }
    }

    public class KeyConfig : Poco
    {
        public List<KeyInfo> Keys { get; set; }
    }

    public class KeyInfo : Poco
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public KeyAction Action { get; set; }

        public int Code { get; set; }
        public string Key { get; set; }
    }

    public enum KeyAction
    {
        KeyRotateLeft,
        KeyRotateRight,
        KeyShiftLeft,
        KeyShiftRight,
        KeyShiftDown,
        KeyNewGame,
        KeyPause,
        KeyContinue,
    }
}