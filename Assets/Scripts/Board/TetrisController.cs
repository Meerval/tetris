using Board.Meta;
using Board.Timers;
using Systems.Events;
using Systems.Timer;
using UnityEngine;

namespace Board
{
    public class TetrisController : MonoBehaviour, IController
    {
        private IGridController _tetrisGrid;
        private IInputExecutor _inputExecutor;

        private TetrisMeta _meta;
        private ITimer _timerOfDrop;
        private ITimer _timerOfLock;

        private void Awake()
        {
            _tetrisGrid = GetComponent<TetrisGridController>();
            _inputExecutor = GetComponent<TetrisInputExecutor>();

            _meta = FindObjectOfType<TetrisMeta>();
            _timerOfDrop = FindObjectOfType<TimerOfPieceDrop>();
            _timerOfLock = FindObjectOfType<TimerOfPieceLock>();
            Debug.Log("TetrisController awoke");
        }

        private void Start()
        {
            SetNewGame();
            Debug.Log("TetrisController started");
        }

        public void DetectAndExecutePieceRotation()
        {
            if (_inputExecutor.OnRotationLeft(() => _tetrisGrid.PieceRotateLeft())) { }
            else if (_inputExecutor.OnRotationRight(() => _tetrisGrid.PieceRotateRight())) { }
            else return;

            _timerOfLock.UpdateTimeout();
        }

        public void DetectAndExecutePieceShift()
        {
            if (_inputExecutor.OnShiftDown(() => _tetrisGrid.PieceShiftDown()))
            {
                _timerOfDrop.UpdateTimeout();
            }
            else if (_inputExecutor.OnShiftLeft(() => _tetrisGrid.PieceShiftLeft())) { }
            else if (_inputExecutor.OnShiftRight(() => _tetrisGrid.PieceShiftRight())) { }
            else return;

            _timerOfLock.UpdateTimeout();
        }

        public void DropPieceAsTimeout()
        {
            if (IsPieceDropUnavailable()) return;
            if (_tetrisGrid.PieceShiftDown())  _timerOfDrop.UpdateTimeout();
            else
            {
                if (_meta.IsUpdateLocked() || _timerOfLock.IsInProgress()) return;
                _tetrisGrid.PieceLock();
                _tetrisGrid.ClearFullLines();
                EventsHub.OnWaitForPiece.Trigger();
            }
        }

        private bool IsPieceDropUnavailable()
        {
            return _meta.IsUpdateLocked() || _meta.State().Equals(EState.WaitForActivePiece) || _timerOfDrop.IsInProgress();
        }

        public void SpawnPieceAsWill()
        {
            if (IsPieceSpawnUnavailable()) return;
            if (!_tetrisGrid.PieceSpawnRandom()) EventsHub.OnGameOver.Trigger(EGameOverReason.GridFilled);
            else _timerOfDrop.UpdateTimeout();
        }

        private bool IsPieceSpawnUnavailable()
        {
            return _meta.IsUpdateLocked() || !_meta.State().Equals(EState.WaitForActivePiece);
        }

        public void SetNewGame()
        {
            EventsHub.OnGameStart.Trigger();
            _timerOfDrop.ResetTimer();
            _tetrisGrid.ClearAll();
        }
    }
}