﻿using System;
using System.Collections.Generic;
using Pieces;
using Pretty;
using Structure;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Actions
{
    public class PieceSpawning : MonoBehaviourSingleton<PieceSpawning>, IPieceSpawning
    {
        [SerializeField] private List<Piece> piecePrefabs;
        private Queue<IPiece> _piecesQueue;

        private void Start()
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
    }
}