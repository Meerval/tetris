using Board;

namespace Menu.SettingsMenuButtonSetters
{
    public class KeyShiftLeftSetter : KeySetter
    {
        protected override KeyInfo KeyInfo => KeyMap.KeyInfoOf(KeyMap.CurrentConfig, KeyActions.KeyShiftLeft);
    }
}