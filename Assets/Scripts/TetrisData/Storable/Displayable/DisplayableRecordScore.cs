using TetrisData.UI;

namespace TetrisData.Storable.Displayable
{
    public class DisplayableRecordScore : TextField<long>
    {
        protected override ITetrisData<long> Data => FindObjectOfType<RecordScore>();
    }
}