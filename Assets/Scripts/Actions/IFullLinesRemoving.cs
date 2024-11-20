using System;
using System.Collections;
using UnityEngine;

namespace Actions
{
    public interface IFullLinesRemoving
    {
        void Execute(ITilemapController tilemap, RectInt bounds);
    }
}