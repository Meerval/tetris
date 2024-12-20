using System.IO;
using Systems.FileAccess;
using Templates.POCO;
using UnityEngine;

namespace Board
{
    public static class KeyMap
    {
        private static KeyConfig _keyConfig = KeyConfig();

        public static KeyCode KeyRotateLeft =  (KeyCode) _keyConfig.KeyRotateLeft.Code;
        public static KeyCode KeyRotateRight = (KeyCode) _keyConfig.KeyRotateRight.Code;
        public static KeyCode KeyShiftLeft = (KeyCode) _keyConfig.KeyShiftLeft.Code;
        public static KeyCode KeyShiftRight = (KeyCode) _keyConfig.KeyShiftRight.Code;
        public static KeyCode KeyShiftDown = (KeyCode) _keyConfig.KeyShiftDown.Code;
        public static KeyCode KeyNewGame = (KeyCode) _keyConfig.KeyNewGame.Code;
        public static KeyCode KeyPause = (KeyCode) _keyConfig.KeyPause.Code;
        public static KeyCode KeyContinue = (KeyCode) _keyConfig.KeyContinue.Code;
        
        private static KeyConfig KeyConfig()
        {
            IFileAccessible<KeyConfig> file = new JsonFile<KeyConfig>
            (
                "KeyMap.json",
                Path.Combine(Application.streamingAssetsPath)
            );
            
            KeyConfig keyConfig = file.Read();
            Debug.Log($"Controller settings uploaded: {keyConfig}");
            return keyConfig;
        }
    }

    public class KeyConfig : Poco
    {
        public KeyAction KeyRotateLeft { get; set; }
        public KeyAction KeyRotateRight { get; set; }
        public KeyAction KeyShiftLeft { get; set; }
        public KeyAction KeyShiftRight { get; set; }
        public KeyAction KeyShiftDown { get; set; }
        public KeyAction KeyNewGame { get; set; }
        public KeyAction KeyPause { get; set; }
        public KeyAction KeyContinue { get; set; }
    }

    public class KeyAction : Poco
    {
        public int Code { get; set; }
        public string Key { get; set; }
    }
}