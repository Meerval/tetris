using System.Collections.Generic;
using System.Linq;
using Board.Meta.Objects;
using Board.Pieces;
using Systems.Storage;
using Templates.Singleton;
using UnityEngine;

namespace Board.Meta
{
    public class TetrisMeta : MonoBehaviourSingleton<TetrisMeta>
    {
        private IProgress<EState> _state;
        private IProgress<bool> _updateLock;
        private IProgress<float> _pieceDropDelay;
        private IProgress<List<(int, IPiece)>> _spawnPieces;
        private IPiece _activePiece;
        private IProgress<int> _level;
        private IProgress<long> _score;
        private IProgress<long> _recordScore;

        public void Start()
        {
            _state = gameObject.AddComponent<State>();
            _updateLock = gameObject.AddComponent<UpdateLock>();
            _pieceDropDelay = gameObject.AddComponent<PieceDropDelay>();
            _spawnPieces = gameObject.AddComponent<SpawnedPieces>();
            _level = GetComponentInChildren<Level>();
            _score = GetComponentInChildren<Score>();
            _recordScore = GetComponentInChildren<RecordScore>();

            Storage.Instance.LoadGame();
            Debug.Log("TetrisMeta started");
        }

        public EState State()
        {
            return _state.Value();
        }

        public bool IsUpdateLocked()
        {
            return _updateLock.Value();
        }

        public float PieceDropDelay()
        {
            return _pieceDropDelay.Value();
        }

        public int Level()
        {
            return _level.Value();
        }

        public long Score()
        {
            return _score.Value();
        }

        public long RecordScore()
        {
            return _recordScore.Value();
        }

        public List<(int, IPiece)> SpawnPieces()
        {
            _activePiece = _spawnPieces.Value().Last().Item2;
            return _spawnPieces.Value();
        }

        public IPiece ActivePiece()
        {
            return _activePiece;
        }
    }
}