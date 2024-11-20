using System;
using System.Collections.Generic;
using Pieces;
using Pretty;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Actions
{
    public class PieceSpawning : MonoBehaviour, IPieceSpawning
    {
        [SerializeField] private List<Piece> piecePrefabs;

        private void Awake()
        {
            if (piecePrefabs == null || piecePrefabs.Count == 0)
            {
                throw new Exception("There is no piece prefabs for spawn");
            }

            Debug.Log($"Available pieces: {new PrettyArray<Piece>(piecePrefabs)}");
        }

        public IPiece Execute()
        {
            Piece piece = Instantiate(piecePrefabs[Random.Range(0, piecePrefabs.Count)], gameObject.transform);
            piece.name = piece.ToString();
            return piece;
        }
    }
}