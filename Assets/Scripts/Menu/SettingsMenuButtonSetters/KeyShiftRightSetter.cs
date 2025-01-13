using Board;

namespace Menu.SettingsMenuButtonSetters
{
    public class KeyShiftRightSetter : KeySetter
    {
        protected override KeyInfo KeyInfo => KeyMap.KeyInfoOf(KeyMap.CurrentConfig, KeyActions.KeyShiftRight);
    }
}