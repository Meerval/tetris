using Board;

namespace Menu.SettingsMenuButtonSetters
{
    public class KeyRotateRightSetter : KeySetter
    {
        protected override KeyInfo KeyInfo => KeyMap.KeyInfoOf(KeyMap.CurrentConfig, KeyActions.KeyRotateRight);
    }
}