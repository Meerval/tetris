using Board;

namespace Menu.SettingsMenuButtonSetters
{
    public class KeyRotateLeftSetter : KeySetter
    {
        protected override KeyInfo KeyInfo => KeyMap.KeyInfoOf(KeyMap.CurrentConfig, KeyAction.KeyRotateLeft);
    }
}