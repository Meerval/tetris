using System.Collections.Generic;
using Board;

namespace Menu.SettingsMenuButtonSetters
{
    public class KeyRotateLeftSetter : KeySetter
    {
        protected override KeyInfo KeyInfo => KeyMap.KeyInfoOf(KeyMap.CurrentConfig, KeyAction.KeyRotateLeft);
        
        protected override List<KeySetter> OtherKeySetters => new()
        {
            FindObjectOfType<KeyRotateRightSetter>(),
            FindObjectOfType<KeyShiftLeftSetter>(),
            FindObjectOfType<KeyShiftRightSetter>(),
            FindObjectOfType<KeyShiftDownSetter>()
        };
    }
}