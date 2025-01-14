using System.Collections.Generic;
using Board;

namespace Menu.SettingsMenuButtonSetters
{
    public class KeyShiftLeftSetter : KeySetter
    {
        protected override KeyInfo KeyInfo => KeyMap.KeyInfoOf(KeyMap.CurrentConfig, KeyAction.KeyShiftLeft);
        protected override List<KeySetter> OtherKeySetters => new()
        {
            FindObjectOfType<KeyRotateLeftSetter>(),
            FindObjectOfType<KeyRotateRightSetter>(),
            FindObjectOfType<KeyShiftRightSetter>(),
            FindObjectOfType<KeyShiftDownSetter>()
        };
    }
}