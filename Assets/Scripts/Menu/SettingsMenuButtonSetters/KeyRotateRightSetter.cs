using System.Collections.Generic;
using Board;

namespace Menu.SettingsMenuButtonSetters
{
    public class KeyRotateRightSetter : KeySetter
    {
        protected override KeyInfo KeyInfo => KeyMap.KeyInfoOf(KeyMap.CurrentConfig, KeyAction.KeyRotateRight);
        protected override List<KeySetter> OtherKeySetters => new()
        {
            FindObjectOfType<KeyRotateLeftSetter>(),
            FindObjectOfType<KeyShiftLeftSetter>(),
            FindObjectOfType<KeyShiftRightSetter>(),
            FindObjectOfType<KeyShiftDownSetter>()
        };
    }
}