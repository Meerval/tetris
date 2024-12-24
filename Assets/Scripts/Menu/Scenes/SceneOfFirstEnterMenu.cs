using TetrisData;
using TetrisData.Storable.Displayable;

namespace Menu.Scenes
{
    public class SceneOfFirstEnterMenu : SceneOfMenu
    {
        protected override void AfterStart()
        {
            if (BoardInfo.Instance.RecordScore() == 0L)
            {
                FindObjectOfType<DisplayableScore>().Destroy();
                FindObjectOfType<DisplayableRecordScore>().Destroy();
                return;
            }

            DisplayableScore score = FindObjectOfType<DisplayableScore>();
            score.UpdateName("Last Score");
        }
    }
}