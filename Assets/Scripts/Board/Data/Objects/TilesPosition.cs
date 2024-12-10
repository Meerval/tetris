using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Systems.Parsers.Object;
using Systems.Storage;
using Systems.Storage.POCO;

namespace Board.Data.Objects
{
    public class TilesPosition : TetrisDataSingleton<string, TilesPosition>
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
                { { Key.Tiles, _position.CurrentValue } }
            );

            public void Load(StorableData data)
            {
                string charList = ObjectToString.TryParse(data.Data[Key.Tiles]).OrElse(InitialData.EmptyTilemap);
                _position.CurrentValue = Regex.IsMatch(charList, @"^[0CBOYGPR]{200}$")
                    ? charList
                    : InitialData.EmptyTilemap;
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
            public static readonly string EmptyTilemap = string.Concat(Enumerable.Repeat("", 200).ToList());
        }
    }
}