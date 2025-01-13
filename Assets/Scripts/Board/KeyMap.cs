using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Systems.FileAccess;
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

        public static KeyCode KeyRotateLeft = CurrentKeyCodeOf(KeyActions.KeyRotateLeft);
        public static KeyCode KeyRotateRight = CurrentKeyCodeOf(KeyActions.KeyRotateRight);
        public static KeyCode KeyShiftLeft = CurrentKeyCodeOf(KeyActions.KeyShiftLeft);
        public static KeyCode KeyShiftRight = CurrentKeyCodeOf(KeyActions.KeyShiftRight);
        public static KeyCode KeyShiftDown = CurrentKeyCodeOf(KeyActions.KeyShiftDown);
        public static KeyCode KeyNewGame = CurrentKeyCodeOf(KeyActions.KeyNewGame);
        public static KeyCode KeyPause = CurrentKeyCodeOf(KeyActions.KeyPause);
        public static KeyCode KeyContinue = CurrentKeyCodeOf(KeyActions.KeyContinue);

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

        private static KeyCode CurrentKeyCodeOf(KeyActions keyAction)
        {
            return (KeyCode)KeyInfoOf(CurrentConfig, keyAction).Code;
        }

        public static KeyInfo KeyInfoOf(KeyConfig config, KeyActions keyAction)
        {
            return config.Keys.Find(key => key.Action.Equals(keyAction.ToString()));
        }

        public static void UpdateKey(String keyAction, KeyCode newKeyCode)
        {
            CurrentConfig.Keys.ForEach
            (
                keyInfo =>
                {
                    if (keyInfo.Action == keyAction)
                        keyInfo.Code = (int)newKeyCode;
                    else
                        keyInfo.Code = keyInfo.Code;
                }
            );

            Type thisClass = typeof(KeyMap);
            FieldInfo fieldInfo = thisClass.GetField(keyAction, BindingFlags.Static | BindingFlags.Public);

            if (fieldInfo != null)
            {
                fieldInfo.SetValue(null, newKeyCode);
            }
            else
            {
                Debug.LogError($"Field with name '{keyAction}' was not found in this class");
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
        public string Action { get; set; }
        public int Code { get; set; }
        public string Key { get; set; }
    }

    public enum KeyActions
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