using System;
using System.Collections.Generic;
using Pretty;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Pieces
{
    public class PieceSpawner : MonoBehaviour
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

        public IPiece SpawnRandom()
        {
            return Instantiate(piecePrefabs[Random.Range(0, piecePrefabs.Count)]);
        }
    }
}