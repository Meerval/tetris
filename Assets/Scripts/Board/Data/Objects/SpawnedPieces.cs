using System.Collections.Generic;
using System.Linq;
using Board.Pieces;
using Systems.Parsers.Object;
using Systems.Pretty;
using Systems.Storage;
using Systems.Storage.POCO;
using UnityEngine;

namespace Board.Data.Objects
{
    public class SpawnedPieces : TetrisDataSingleton<List<(int, IPiece)>, SpawnedPieces>
    {
        protected override IStorable StorableTetrisData => new SpawnedPiecesStorable();
        protected override bool IsResettableByNewGame => true;

        protected override void SubscribeDataAction()
        {
            EventsHub.OnSpawnPiece.AddSubscriber(AddPieceToSpawnedPieces, 1);
        }

        protected override void UnsubscribeDataAction()
        {
            EventsHub.OnSpawnPiece.RemoveSubscriber(AddPieceToSpawnedPieces);
        }

        private void AddPieceToSpawnedPieces(IPiece piece)
        {
            CurrentValue ??= InitialData.EmptyList;
            CurrentValue.Add((CurrentValue.Count + 1, piece));
            Debug.Log($"{new PrettyOrdinal(CurrentValue.Count)} piece spawned: {piece}");
        }

        private class SpawnedPiecesStorable : IStorable
        {
            private readonly SpawnedPieces _pieces = Instance;
            public string Id => Key.Id;

            public StorableData StorableData => new(Id, new Dictionary<string, object>
                {
                    {
                        Key.Pieces,
                        string.Concat(_pieces.CurrentValue
                            .OrderBy(item => item.Item1)
                            .Select(item => item.Item2.PieceType.ToString()))
                    }
                }
            );

            public void Load(StorableData data)
            {
                LoadInitial();
                string charList = ObjectToString.TryParse(data.Data[Key.Pieces]).OrElse(InitialData.EmptyListStr);
                
                IPiece CreateIPiece(char c)
                {
                    IPiece piece = PieceProvider.Instance.Get(EPieceParser.Parse(c));
                    return piece;
                }
                
                _pieces.CurrentValue = charList.Equals(InitialData.EmptyListStr)
                    ? InitialData.EmptyList
                    : charList.Select((pieceCode, i) => (i, CreateIPiece(pieceCode))).ToList();
            }

            public void LoadInitial()
            {
                _pieces.CurrentValue = InitialData.EmptyList;
            }

            private struct Key
            {
                public const string Id = "SpawnedPieces";
                public const string Pieces = "Pieces";
            }
        }

        private struct InitialData
        {
            public const string EmptyListStr = "";
            public static readonly List<(int, IPiece)> EmptyList = new();
        }
    }
}