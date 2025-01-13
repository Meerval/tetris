using Board;

namespace Menu.SettingsMenuButtonSetters
{
    public class KeyShiftDownSetter : KeySetter
    {
        protected override KeyInfo KeyInfo => KeyMap.KeyInfoOf(KeyMap.CurrentConfig, KeyActions.KeyShiftDown);
    }
}