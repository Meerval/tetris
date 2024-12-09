using System.Collections.Generic;
using System.Linq;
using Board.Pieces;
using Systems.Parsers.Object;
using Systems.Pretty;
using Systems.Storage;
using Systems.Storage.POCO;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Board.Data.Objects
{
    public class TilesPosition : TetrisDataSingleton<List<List<TileBase>>, TilesPosition>
    {
        protected override IStorable StorableTetrisData => new TilesPositionStorable();
        protected override bool IsResettableByNewGame => true;

        protected override void InitForNewGame()
        {
            CurrentValue = InitialData.EmptyTilemap;
        }

        protected override void SubscribeDataAction()
        {
            EventsHub.OnScoreUp.AddSubscriber(UpdateTiles);
        }

        protected override void UnsubscribeDataAction()
        {
            EventsHub.OnScoreUp.RemoveSubscriber(UpdateTiles);
        }

        private void UpdateTiles(int _)
        {
            //
        }

        private class TilesPositionStorable : IStorable
        {
            private readonly TilesPosition _position = Instance;
            public string Id => Key.Id;

            public StorableData StorableData => new(Id, new Dictionary<string, object>
                {
                    {
                        Key.Tiles,
                        string.Concat(_position.CurrentValue.Select(
                            list => string.Concat(list.Select(c => c == null ? "0" : c.name.Take(1)).ToList())
                        ))
                    }
                }
            );

            public void Load(StorableData data)
            {
                string charList = ObjectToString.TryParse(data.Data[Key.Tiles]).OrElse(InitialData.EmptyListStr);

                TileBase GetTilemap(char c)
                {
                    return TileProvider.Instance.Get(ETileParser.Parse(c));
                }

                _position.CurrentValue = charList.Equals(InitialData.EmptyListStr)
                    ? InitialData.EmptyTilemap
                    : charList
                        .Take(200)
                        .Select((c, i) => new { c, i })
                        .GroupBy(x => x.i / 10)
                        .Select(group => group.Select(x => GetTilemap(x.c)).ToList())
                        .ToList();
            }

            public void LoadInitial()
            {
                _position.CurrentValue = InitialData.EmptyTilemap;
            }

            private struct Key
            {
                public const string Id = "TilesPosition";
                public const string Tiles = "Tiles";
            }
        }

        private struct InitialData
        {
            public const string EmptyListStr = "";

            public static readonly List<List<TileBase>> EmptyTilemap =
                Enumerable.Repeat(Enumerable.Repeat<TileBase>(null, 10).ToList(), 20).ToList();
        }
    }
}