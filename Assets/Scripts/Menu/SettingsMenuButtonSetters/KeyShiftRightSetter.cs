using System.Collections.Generic;
using Board;

namespace Menu.SettingsMenuButtonSetters
{
    public class KeyShiftRightSetter : KeySetter
    {
        protected override KeyInfo KeyInfo => KeyMap.KeyInfoOf(KeyMap.CurrentConfig, KeyAction.KeyShiftRight);
        protected override List<KeySetter> OtherKeySetters => new()
        {
            FindObjectOfType<KeyRotateLeftSetter>(),
            FindObjectOfType<KeyRotateRightSetter>(),
            FindObjectOfType<KeyShiftLeftSetter>(),
            FindObjectOfType<KeyShiftDownSetter>()
        };
    }
}