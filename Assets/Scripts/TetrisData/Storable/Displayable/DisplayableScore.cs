using TetrisData.UI;

namespace TetrisData.Storable.Displayable
{
    public class DisplayableScore : TextField<long>
    {
        protected override ITetrisData<long> Data => FindObjectOfType<Score>();
    }
}