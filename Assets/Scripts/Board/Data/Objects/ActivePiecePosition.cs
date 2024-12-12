using System.Collections.Generic;
using System.Text.RegularExpressions;
using Systems.Parsers.Object;
using Systems.Storage;
using Systems.Storage.POCO;
using UnityEngine;

namespace Board.Data.Objects
{
    public class ActivePiecePosition : TetrisDataSingleton<Vector2Int, ActivePiecePosition>
    {
        protected override IStorable StorableTetrisData => new ActivePiecePositionStorable();
        protected override bool IsResettableByNewGame => true;

        protected override void InitForNewGame()
        {
            CurrentValue = InitialData.Coordinates;
        }

        protected override void SubscribeDataAction()
        {
            EventsHub.OnPieceShift.AddSubscriber(UpdatePosition, 1);
        }

        protected override void UnsubscribeDataAction()
        {
            EventsHub.OnPieceShift.RemoveSubscriber(UpdatePosition);
        }

        private void UpdatePosition(Vector2Int position)
        {
            CurrentValue = position;
            Storage.Instance.SaveGame();
        }

        private class ActivePiecePositionStorable : IStorable
        {
            private readonly ActivePiecePosition _position = Instance;
            public string Id => Key.Id;

            public StorableData StorableData => new(Id, new Dictionary<string, object>
                {
                    { Key.Coordinates, _position.CurrentValue.ToString() }
                }
            );

            public void Load(StorableData data)
            {
                string position = ObjectToString.TryParse(data.Data[Key.Coordinates]).OrElse(InitialData.EmptyStr);
                string pattern = @"-?\d+";
                MatchCollection positionMatches = Regex.Matches(position, pattern);
                _position.CurrentValue = positionMatches.Count >= 2
                    ? new Vector2Int(int.Parse(positionMatches[0].Value), int.Parse(positionMatches[1].Value))
                    : InitialData.Coordinates;
            }

            public void LoadInitial()
            {
                _position.CurrentValue = InitialData.Coordinates;
            }

            private struct Key
            {
                public const string Id = "PiecePosition";
                public const string Coordinates = "Coordinates";
            }
        }

        private struct InitialData
        {
            public const string EmptyStr = "";
            public static readonly Vector2Int Coordinates = new(-1, 8);
        }
    }
}