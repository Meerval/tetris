using UnityEngine;

namespace Board.Actions
{
    public interface IFullLinesRemoving
    {
        void Execute(ITilemapController tilemap, RectInt bounds);
    }
}