using System.Collections.Generic;
using Board.Pieces;
using Systems.Storage;
using Systems.Pretty;
using UnityEngine;

namespace Board.Data.Objects
{
    public class SpawnedPieces : TetrisDataSingleton<List<(int, IPiece)>, SpawnedPieces>
    {
        protected override IStorable StorableTetrisData => null;

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
            CurrentValue ??= InitialData.List;
            CurrentValue.Add((CurrentValue.Count + 1, piece));
            Debug.Log($"{new PrettyOrdinal(CurrentValue.Count)} piece spawned: {piece}");
        }

        private struct InitialData
        {
            public static readonly List<(int, IPiece)> List = new();
        }
    }
}