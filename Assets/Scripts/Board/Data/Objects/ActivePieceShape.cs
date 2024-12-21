using System.Collections.Generic;
using System.Text.RegularExpressions;
using Board.Pieces;
using Systems.Parsers.Object;
using Systems.Pretty;
using Systems.Storage;
using Systems.Storage.POCO;
using UnityEngine;

namespace Board.Data.Objects
{
    public class ActivePieceShape : TetrisDataSingleton<Vector2Int[], ActivePieceShape>
    {
        protected override IStorable StorableTetrisData => new ActivePieceShapeStorable();
        protected override bool IsResettableByNewGame => true;

        protected override void InitForNewGame()
        {
            CurrentValue = InitialData.Coordinates;
        }

        protected override void SubscribeDataAction()
        {
            EventsHub.OnPieceSpawn.AddSubscriber(UpdateShape, 1);
            EventsHub.OnPieceRotate.AddSubscriber(UpdateShape, 1);
        }

        protected override void UnsubscribeDataAction()
        {
            EventsHub.OnPieceSpawn.RemoveSubscriber(UpdateShape);
            EventsHub.OnPieceRotate.RemoveSubscriber(UpdateShape);
        }

        private void UpdateShape(IPiece piece)
        {
            CurrentValue = piece.CurrentShapeMap();
        }
        
        private void UpdateShape(Vector2Int[] position)
        {
            CurrentValue = position;
        }

        private class ActivePieceShapeStorable : IStorable
        {
            private readonly ActivePieceShape _shape = Instance;
            public string Id => Key.Id;

            public StorableData StorableData => new(Id, new Dictionary<string, object>
                {
                    { Key.Coordinates, new PrettyArray<Vector2Int>(_shape.CurrentValue).Prettify() }
                }
            );

            public void Load(StorableData data)
            {
                const string pattern = @"-?\d+";
                string shape = ObjectToString.TryParse(data.Data[Key.Coordinates]).OrElse(InitialData.EmptyStr);
                MatchCollection shapeMatches = Regex.Matches(shape, pattern);
                _shape.CurrentValue = shapeMatches.Count >= 8
                    ? new Vector2Int[]
                    {
                        new(int.Parse(shapeMatches[0].Value), int.Parse(shapeMatches[1].Value)),
                        new(int.Parse(shapeMatches[2].Value), int.Parse(shapeMatches[3].Value)),
                        new(int.Parse(shapeMatches[4].Value), int.Parse(shapeMatches[5].Value)),
                        new(int.Parse(shapeMatches[6].Value), int.Parse(shapeMatches[7].Value))
                    }
                    : InitialData.Coordinates;
            }

            public void LoadInitial()
            {
                _shape.CurrentValue = InitialData.Coordinates;
            }

            private struct Key
            {
                public const string Id = "PieceShape";
                public const string Coordinates = "Coordinates";
            }
        }

        private struct InitialData
        {
            public const string EmptyStr = "";
            public static readonly Vector2Int[] Coordinates = { new(0, 0), new(0, 0), new(0, 0), new(0, 0) };
        }
    }
}