using System.Collections.Generic;
using System.Linq;
using Board.Pieces;
using Systems.Storage;
using Templates.Singleton;
using TetrisData.Storable;
using UnityEngine;

namespace TetrisData
{
    public class BoardInfo : MonoBehaviourSingleton<BoardInfo>
    {
        private ITetrisData<EBoardState> _boardState;
        private ITetrisData<ETetrisStage> _tetrisStage;
        private ITetrisData<bool> _updateLock;
        private ITetrisData<float> _pieceDropDelay;
        private ITetrisData<List<(int, IPiece)>> _spawnPieces;
        private ITetrisData<Queue<IPiece>> _pieceQueue;
        private ITetrisData<Dictionary<string, string>> _tilesPosition;
        private ITetrisData<Vector2Int> _activePiecePosition;
        private ITetrisData<Vector2Int[]> _activePieceShape;
        private ITetrisData<int> _level;
        private ITetrisData<long> _score;
        private ITetrisData<long> _recordScore;

        public void Start()
        {
            _boardState = gameObject.AddComponent<BoardState>();
            _tetrisStage = gameObject.AddComponent<TetrisStage>();
            _updateLock = gameObject.AddComponent<LockBoard>();
            _pieceDropDelay = gameObject.AddComponent<PieceDropDelay>();
            _spawnPieces = gameObject.AddComponent<SpawnedPieces>();
            _pieceQueue = gameObject.AddComponent<PieceQueue>();
            _tilesPosition = gameObject.AddComponent<TilesPosition>();
            _activePiecePosition = gameObject.AddComponent<ActivePiecePosition>();
            _activePieceShape = gameObject.AddComponent<ActivePieceShape>();
            _level = gameObject.AddComponent<Level>();
            _score = gameObject.AddComponent<Score>();
            _recordScore = gameObject.AddComponent<RecordScore>();

            Storage.Instance.LoadGame();
            Debug.Log("BoardInfo started");
        }

        public EBoardState BoardState()
        {
            return _boardState.Value();
        }
        
        public ETetrisStage TetrisStage()
        {
            return _tetrisStage.Value();
        }
        
        public bool IsBoardLocked()
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
            return _spawnPieces.Value().Count == 0 ? null : _spawnPieces.Value().Last().Item2;
        }

        public Vector2Int ActivePiecePosition()
        {
            return _activePiecePosition.Value();
        }

        public Vector2Int[] ActivePieceShape()
        {
            return _activePieceShape.Value();
        }
    }
}