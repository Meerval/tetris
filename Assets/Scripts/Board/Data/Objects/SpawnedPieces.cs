using System.Collections.Generic;
using Board.Pieces;
using Systems.Storage;
using Systems.Pretty;
using UnityEngine;

namespace Board.Data.Objects
{
    public class SpawnedPieces : TetrisDataSingleton<List<(int, IPiece)>, SpawnedPieces>
    {
        protected override IStorable StorableTracker => null;
        
        protected override void StartNewProgress()
        {
            CurrentValue = new List<(int, IPiece)>();
        }

        protected override void SubscribeProgressAction()
        {
            EventsHub.OnSpawnPiece.AddSubscriber(AddPieceToSpawnedPieces, 1);
        }

        protected override void UnsubscribeProgressAction()
        {
            EventsHub.OnSpawnPiece.RemoveSubscriber(AddPieceToSpawnedPieces);
        }

        private void AddPieceToSpawnedPieces(IPiece piece)
        {
            CurrentValue.Add((CurrentValue.Count + 1, piece));
            Debug.Log($"{new PrettyOrdinal(CurrentValue.Count)} piece spawned: {piece}");
        }

    }
}