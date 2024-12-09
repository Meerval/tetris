using System;
using System.Collections.Generic;
using Systems.Pretty;
using Templates.Singleton;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Board.Pieces
{
    public class PiecePrefabs : MonoBehaviourSingleton<PiecePrefabs>
    {
        [SerializeField] private List<Piece> piecePrefabs;

        protected override void AfterAwake()
        {
            if (piecePrefabs == null || piecePrefabs.Count == 0)
            {
                throw new Exception("There is no piece prefabs for spawn");
            }

            Debug.Log($"Available pieces: {new PrettyArray<Piece>(piecePrefabs)}");
        }

        public IPiece Get(EPiece pieceType)
        {
            return piecePrefabs.Find(piece => piece.PieceType == pieceType);
        }

        public IPiece GetRandom()
        {
            return piecePrefabs[Random.Range(0, piecePrefabs.Count)];
        }
    }
}