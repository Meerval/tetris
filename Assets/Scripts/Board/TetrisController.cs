﻿using Board.Timers;
using Systems.Accumulator;
using Systems.Chrono.Timer;
using Systems.Storage;
using TetrisData;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Board
{
    public class TetrisController : MonoBehaviour
    {
        private IGridController _tetrisGrid;
        private IInputExecutor _inputExecutor;

        private BoardInfo _info;
        private ITimer _timerOfDrop;
        private ITimer _timerOfLock;

        private readonly Accumulator _failedShiftDown = new(5);
        private readonly Accumulator _failedShiftLeft = new(5);
        private readonly Accumulator _failedShiftRight = new(5);

        private void Awake()
        {
            _tetrisGrid = GetComponent<TetrisGridController>();
            _inputExecutor = GetComponent<TetrisInputExecutor>();

            _info = FindObjectOfType<BoardInfo>();
            _timerOfDrop = FindObjectOfType<TimerOfPieceDrop>();
            _timerOfLock = FindObjectOfType<TimerOfPieceLock>();
            Debug.Log("TetrisController awoke");
        }

        private void Start()
        {
            if (_info.TetrisStage() == ETetrisScene.OnBoardOfNewGame) SetNewGame();
            Debug.Log("TetrisController started");
        }

        public void DetectAndExecutePieceRotation()
        {
            if (_inputExecutor.OnRotationLeft(() => _tetrisGrid.PieceRotateLeft(), out bool isRotatedLeft))
            {
                if (isRotatedLeft) _timerOfLock.UpdateTimeout();
            }
            else if (_inputExecutor.OnRotationRight(() => _tetrisGrid.PieceRotateRight(), out bool isRotatedRight))
            {
                if (isRotatedLeft || isRotatedRight) _timerOfLock.UpdateTimeout();
            }
        }

        public void DetectAndExecutePieceShift()
        {
            if (_inputExecutor.OnShiftDown(() => _tetrisGrid.PieceShiftDown(), out bool isShiftedDown))
            {
                if (isShiftedDown) _timerOfDrop.UpdateTimeout();
                else
                {
                    _failedShiftDown.Increment();
                    _failedShiftLeft.Reset();
                    _failedShiftRight.Reset();
                }

                if (_failedShiftDown.IsNotFull()) _timerOfLock.UpdateTimeout();
            }
            else if (_inputExecutor.OnShiftLeft(() => _tetrisGrid.PieceShiftLeft(), out bool isShiftedLeft))
            {
                if (!isShiftedLeft)
                {
                    _failedShiftDown.Reset();
                    _failedShiftLeft.Increment();
                    _failedShiftRight.Reset();
                }

                if (_failedShiftLeft.IsNotFull()) _timerOfLock.UpdateTimeout();
            }
            else if (_inputExecutor.OnShiftRight(() => _tetrisGrid.PieceShiftRight(), out bool isShiftedRight))
            {
                if (!isShiftedRight)
                {
                    _failedShiftDown.Reset();
                    _failedShiftLeft.Reset();
                    _failedShiftRight.Increment();
                }

                if (_failedShiftRight.IsNotFull()) _timerOfLock.UpdateTimeout();
            }
            else
            {
                _failedShiftDown.Reset();
                _failedShiftLeft.Reset();
                _failedShiftRight.Reset();
            }
        }

        public void DetectAndExecuteNewGame()
        {
            _inputExecutor.OnNewGame(() =>
            {
                SetNewGame();
                Debug.Log("New game started by keyboard");
            });
        }

        public void DetectAndExecutePause()
        {
            _inputExecutor.OnPauseGame(() =>
            {
                if (_info.TetrisStage() == ETetrisScene.OnBoard || _info.TetrisStage() == ETetrisScene.OnBoardOfNewGame)
                {
                    EventsHub.OnStageChanged.Trigger(ETetrisScene.OnMenuOfPause);
                    Storage.Instance.SaveGame();
                    SceneManager.LoadScene("MainMenu");
                    Debug.Log("Game paused by keyboard");
                }
            });
        }

        public void DropPieceAsTimeout()
        {
            if (IsPieceDropUnavailable()) return;
            if (_tetrisGrid.PieceShiftDown()) _timerOfDrop.UpdateTimeout();
            else
            {
                if (_info.IsBoardLocked() || _timerOfLock.IsInProgress()) return;
                _tetrisGrid.PieceLock();
                _tetrisGrid.ClearFullLines();
                EventsHub.OnWaitForPiece.Trigger();
            }
        }

        private bool IsPieceDropUnavailable()
        {
            return _info.IsBoardLocked() || _info.BoardState().Equals(EBoardState.WaitForActivePiece) ||
                   _timerOfDrop.IsInProgress();
        }

        public void SpawnPieceAsWill()
        {
            if (IsPieceSpawnUnavailable()) return;
            if (!_tetrisGrid.PieceSpawnRandom())
            {
                EventsHub.OnGameOver.Trigger(EGameOverReason.GridFilled);
            }
            else _timerOfDrop.UpdateTimeout();
        }

        private bool IsPieceSpawnUnavailable()
        {
            return _info.IsBoardLocked() || !_info.BoardState().Equals(EBoardState.WaitForActivePiece);
        }

        public void SetNewGame()
        {
            _timerOfDrop.ResetTimer();
            _tetrisGrid.ClearAll();
            EventsHub.OnNewGameStart.Trigger();
            EventsHub.OnStageChanged.Trigger(ETetrisScene.OnBoard);
            Storage.Instance.SaveGame();
        }
    }
}