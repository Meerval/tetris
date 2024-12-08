using System;
using System.Collections.Generic;
using Board.Pieces;
using Systems.Pretty;
using Templates.Singleton;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Board.Actions
{
    public class PieceSpawning : MonoBehaviourSingleton<PieceSpawning>, IPieceSpawning
    {
        [SerializeField] private List<Piece> piecePrefabs;
        private Queue<IPiece> _piecesQueue;

        protected override void AfterAwake()
        {
            if (piecePrefabs == null || piecePrefabs.Count == 0)
            {
                throw new Exception("There is no piece prefabs for spawn");
            }

            Debug.Log($"Available pieces: {new PrettyArray<Piece>(piecePrefabs)}");
            _piecesQueue = new Queue<IPiece>();
        }

        public Queue<IPiece> Execute()
        {
            while (_piecesQueue.Count < 2)
            {
                Piece piece = Instantiate(piecePrefabs[Random.Range(0, piecePrefabs.Count)], gameObject.transform);
                piece.name = piece.ToString();
                _piecesQueue.Enqueue(piece);
            }

            return _piecesQueue;
        }

        public IPiece ExecuteShadow(EPiece pieceType)
        {
            return piecePrefabs.Find(piece => piece.PieceType == pieceType);
        }
    }
}