using System.Collections.Generic;
using System.Linq;
using Board.Data.Objects;
using Board.Pieces;
using Systems.Storage;
using Templates.Singleton;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Board.Data
{
    public class TetrisInfo : MonoBehaviourSingleton<TetrisInfo>
    {
        private ITetrisData<EState> _state;
        private ITetrisData<bool> _updateLock;
        private ITetrisData<float> _pieceDropDelay;
        private ITetrisData<List<(int, IPiece)>> _spawnPieces;
        private ITetrisData<Queue<IPiece>> _pieceQueue;
        private ITetrisData<Dictionary<string, string>> _tilesPosition;
        private IPiece _activePiece;
        private ITetrisData<int> _level;
        private ITetrisData<long> _score;
        private ITetrisData<long> _recordScore;

        public void Start()
        {
            _state = gameObject.AddComponent<State>();
            _updateLock = gameObject.AddComponent<UpdateLock>();
            _pieceDropDelay = gameObject.AddComponent<PieceDropDelay>();
            _spawnPieces = gameObject.AddComponent<SpawnedPieces>();
            _pieceQueue = gameObject.AddComponent<PieceQueue>();
            _tilesPosition = gameObject.AddComponent<TilesPosition>();
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

        public Queue<IPiece> PieceQueue()
        {
            return _pieceQueue.Value();
        }

        public Dictionary<string, string> TilesPosition()
        {
            return _tilesPosition.Value();
        }

        public IPiece ActivePiece()
        {
            return _activePiece;
        }
    }
}