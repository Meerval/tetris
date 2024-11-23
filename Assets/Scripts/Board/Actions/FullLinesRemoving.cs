using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Board.Timer;
using Systems.Events;
using Templates.Pretty;
using UnityEngine;

namespace Board.Actions
{
    public class FullLinesRemoving : MonoBehaviour, IFullLinesRemoving
    {
        private ITilemapController _tilemap;
        private RectInt _bounds;

        public void Execute(ITilemapController tilemap, RectInt bounds)
        {
            _tilemap = tilemap;
            _bounds = bounds;
            StartCoroutine(Coroutine());
        }

        private IEnumerator Coroutine()
        {
            EventsHub.OnWaitCoroutineStart.Trigger();
            List<int> fullLines = new List<int>();

            for (int line = _bounds.yMin; line < _bounds.yMax;)
            {
                if (!IsLineFull(line))
                {
                    line++;
                    continue;
                }

                fullLines.Add(line);
                for (int x = _bounds.xMax - _bounds.width / 2; x < _bounds.xMax; x++)
                {
                    TimerOfClearLine.Instance.UpdateTimeout();
                    yield return new WaitWhile(TimerOfClearLine.Instance.IsInProgress);
                    _tilemap.Clear(new Vector2Int(x, line));
                    _tilemap.Clear(new Vector2Int(_bounds.xMax - _bounds.width / 2 - x - 1, line));
                }

                for (int y = line; y < _bounds.yMax; y++)
                {
                    for (int x = _bounds.xMin; x < _bounds.xMax; x++)
                    {
                        _tilemap.Set
                        (
                            new Vector2Int(x, y),
                            _tilemap.Get(new Vector2Int(x, y + 1)
                            )
                        );
                    }
                }
            }

            if (fullLines.Count > 0)
            {
                Debug.Log($"Lines were cleared: {new PrettyArray<int>(fullLines)}");
                EventsHub.OnScoreUp.Trigger(fullLines.Count);
            }

            EventsHub.OnWaitCoroutineEnd.Trigger();
        }
        
        private bool IsLineFull(int y)
        {
            return Enumerable
                .Range(_bounds.xMin, _bounds.width)
                .All(x => _tilemap.IsTaken(new Vector2Int(x, y)));
        }
    }
}