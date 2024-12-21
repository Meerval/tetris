using System.Collections.Generic;
using System.Linq;
using Systems.Parsers.Object;
using Systems.Storage;
using Systems.Storage.POCO;
using UnityEngine;

namespace Board.Data.Objects
{
    public class TilesPosition : TetrisDataSingleton<Dictionary<string, string>, TilesPosition>
    {
        protected override Dictionary<string, string> CurrentValue
        {
            get => base.CurrentValue ?? InitialData.EmptyTilemaps;
            set => base.CurrentValue = value;
        }
        
        
        protected override IStorable StorableTetrisData => new TilesPositionStorable();
        protected override bool IsResettableByNewGame => true;

        protected override void InitForNewGame()
        {
            CurrentValue = InitialData.EmptyTilemaps;
        }

        protected override void SubscribeDataAction()
        {
            EventsHub.OnGridUpdated.AddSubscriber(UpdateTileCodes);
        }

        protected override void UnsubscribeDataAction()
        {
            EventsHub.OnGridUpdated.RemoveSubscriber(UpdateTileCodes);
        }

        private void UpdateTileCodes(TilemapController tilemapController, RectInt bounds)
        {
            CurrentValue[tilemapController.name] = tilemapController.ToStorableString(bounds);
        }

        private class TilesPositionStorable : IStorable
        {
            private readonly TilesPosition _position = Instance;

            public string Id => Key.Id;

            public StorableData StorableData => new(Id, new Dictionary<string, object>
                {
                    { Key.ActiveTilemap, _position.CurrentValue[Key.ActiveTilemap] },
                    { Key.LockedTilemap, _position.CurrentValue[Key.LockedTilemap] },
                    { Key.PredictedTilemap, _position.CurrentValue[Key.PredictedTilemap] },
                    { Key.ProjectedTilemap, _position.CurrentValue[Key.ProjectedTilemap] }
                }
            );

            public void Load(StorableData data)
            {
                _position.CurrentValue[Key.ActiveTilemap] = ObjectToString.TryParse(data.Data[Key.ActiveTilemap])
                        .OrElse(InitialData.EmptyTilemap200);
                _position.CurrentValue[Key.LockedTilemap] = ObjectToString.TryParse(data.Data[Key.LockedTilemap])
                        .OrElse(InitialData.EmptyTilemap200);
                _position.CurrentValue[Key.PredictedTilemap] = ObjectToString.TryParse(data.Data[Key.PredictedTilemap])
                        .OrElse(InitialData.EmptyTilemap24);
                _position.CurrentValue[Key.ProjectedTilemap] = ObjectToString.TryParse(data.Data[Key.ProjectedTilemap])
                        .OrElse(InitialData.EmptyTilemap200);
            }

            public void LoadInitial()
            {
                _position.CurrentValue = InitialData.EmptyTilemaps;
            }
        }

        private struct Key
        {
            public const string Id = "Grid";
            public const string ActiveTilemap = "ActiveTilemap";
            public const string LockedTilemap = "LockedTilemap";
            public const string PredictedTilemap = "PredictedTilemap";
            public const string ProjectedTilemap = "ProjectedTilemap";
        }

        private struct InitialData
        {
            public static readonly string EmptyTilemap200 = string.Concat(Enumerable.Repeat("", 200).ToList());
            public static readonly string EmptyTilemap24 = string.Concat(Enumerable.Repeat("", 200).ToList());

            public static readonly Dictionary<string, string> EmptyTilemaps = new()
            {
                { Key.ActiveTilemap, EmptyTilemap200 },
                { Key.LockedTilemap, EmptyTilemap200 },
                { Key.PredictedTilemap, EmptyTilemap24 },
                { Key.ProjectedTilemap, EmptyTilemap200 }
            };
        }
    }
}