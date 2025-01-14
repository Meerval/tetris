using System.Collections.Generic;
using Board;

namespace Menu.SettingsMenuButtonSetters
{
    public class KeyShiftDownSetter : KeySetter
    {
        protected override KeyInfo KeyInfo => KeyMap.KeyInfoOf(KeyMap.CurrentConfig, KeyAction.KeyShiftDown);
        protected override List<KeySetter> OtherKeySetters => new()
        {
            FindObjectOfType<KeyRotateLeftSetter>(),
            FindObjectOfType<KeyRotateRightSetter>(),
            FindObjectOfType<KeyShiftLeftSetter>(),
            FindObjectOfType<KeyShiftRightSetter>()
        };
    }
}