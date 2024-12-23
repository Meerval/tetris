using TetrisData.UI;

namespace TetrisData.Storable.Displayable
{
    public class DisplayableLevel : TextField<int>
    {
        protected override ITetrisData<int> Data => FindObjectOfType<Level>();
    }
}