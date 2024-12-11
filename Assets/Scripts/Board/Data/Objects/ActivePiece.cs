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
    public class ActivePiece : TetrisDataSingleton<IPiece, ActivePiece>
    {
        protected override IStorable StorableTetrisData => new PieceStorable();
        protected override bool IsResettableByNewGame => true;

        protected override IPiece CurrentValue
        {
            get => base.CurrentValue ?? InitialData.RandomPiece;
            set => base.CurrentValue = value;
        }

        private Vector2Int[] _shape;
        private Vector2Int _position;

        protected override void InitForNewGame()
        {
            CurrentValue = InitialData.RandomPiece;
        }

        protected override void SubscribeDataAction()
        {
            EventsHub.OnPieceSpawn.AddSubscriber(UpdatePiece, 1);
            EventsHub.OnPieceMove.AddSubscriber(UpdatePosition, 1);
            EventsHub.OnPieceRotate.AddSubscriber(UpdateShape, 1);
        }

        protected override void UnsubscribeDataAction()
        {
            EventsHub.OnPieceSpawn.RemoveSubscriber(UpdatePiece);
            EventsHub.OnPieceMove.RemoveSubscriber(UpdatePosition);
            EventsHub.OnPieceRotate.RemoveSubscriber(UpdateShape);
        }

        private void UpdatePiece(IPiece piece)
        {
            CurrentValue = piece;
        }

        private void UpdatePosition(Vector2Int position)
        {
            _position = position;
        }

        private void UpdateShape(Vector2Int[] shape)
        {
            _shape = shape;
        }

        private class PieceStorable : IStorable
        {
            private readonly ActivePiece _piece = Instance;
            public string Id => Key.Id;

            public StorableData StorableData => new(Id, new Dictionary<string, object>
                {
                    { Key.Piece, _piece.CurrentValue.PieceType.ToString() },
                    { Key.Position, _piece.CurrentValue.PieceType.ToString() },
                    { Key.Shape, new PrettyArray<Vector2Int>(_piece.CurrentValue.CurrentShapeMap()).Prettify() },
                }
            );

            public void Load(StorableData data)
            {
                string pieceCode = ObjectToString.TryParse(data.Data[Key.Piece]).OrElse(InitialData.EmptyStr);

                _piece.CurrentValue = pieceCode.Equals(InitialData.EmptyStr) || pieceCode.Length != 1
                    ? InitialData.RandomPiece
                    : PieceProvider.Instance.Get(EPieceParser.Parse(pieceCode[0]));

                string position = ObjectToString.TryParse(data.Data[Key.Position]).OrElse(InitialData.EmptyStr);
                string pattern = @"-?\d+";
                MatchCollection positionMatches = Regex.Matches(position, pattern);
                _piece._position = positionMatches.Count >= 2
                    ? new Vector2Int(int.Parse(positionMatches[0].Value), int.Parse(positionMatches[1].Value))
                    : InitialData.Position;
                string shape = ObjectToString.TryParse(data.Data[Key.Shape]).OrElse(InitialData.EmptyStr);
                MatchCollection shapeMatches = Regex.Matches(shape, pattern);
                _piece._shape = shapeMatches.Count >= 8
                    ? new Vector2Int[]
                    {
                        new(int.Parse(shapeMatches[0].Value), int.Parse(shapeMatches[1].Value)),
                        new(int.Parse(shapeMatches[2].Value), int.Parse(shapeMatches[3].Value)),
                        new(int.Parse(shapeMatches[4].Value), int.Parse(shapeMatches[5].Value)),
                        new(int.Parse(shapeMatches[6].Value), int.Parse(shapeMatches[7].Value))
                    }
                    : InitialData.Shape;
            }

            public void LoadInitial()
            {
                _piece.CurrentValue = InitialData.RandomPiece;
                _piece._position = InitialData.Position;
                _piece._shape = InitialData.Shape;
            }

            private struct Key
            {
                public const string Id = "ActivePiece";
                public const string Piece = "Piece";
                public const string Shape = "Shape";
                public const string Position = "Position";
            }
        }

        private struct InitialData
        {
            public const string EmptyStr = "";
            public static readonly IPiece RandomPiece = PieceProvider.Instance.GetRandom();
            public static readonly Vector2Int[] Shape = RandomPiece.CurrentShapeMap();
            public static readonly Vector2Int Position = new(-1, 8);
        }
    }
}