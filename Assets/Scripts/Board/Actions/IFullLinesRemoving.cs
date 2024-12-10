using UnityEngine;

namespace Board.Actions
{
    public interface IFullLinesRemoving
    {
        void Execute(TilemapController tilemap, RectInt bounds);
    }
}