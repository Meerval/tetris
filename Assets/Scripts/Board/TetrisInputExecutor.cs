using System;
using System.Collections;
using Board.Data;
using Board.Timers;
using Systems.Chrono.Timer;
using UnityEngine;

namespace Board
{
    public class TetrisInputExecutor : MonoBehaviour, IInputExecutor
    {
        private TetrisInfo _info;
        private ITimer _timerOfMove;
        private ITimer _timerOfClick;
        private bool _isShifted;

        private void Awake()
        {
            _info = FindObjectOfType<TetrisInfo>();
            _timerOfMove = FindObjectOfType<TimerOfPieceMove>();
            _timerOfClick = FindObjectOfType<TimerOfSingleClick>();
            Debug.Log("TetrisInputExecutor awoke");
        }

        public bool OnRotationLeft(Func<bool> action, out bool isRotated)
        {
            bool isActed = OnRotationKeyDown(KeyMap.KeyRotateLeft, action, out bool rotated);
            isRotated = rotated;
            return isActed;
        }

        public bool OnRotationRight(Func<bool> action, out bool isRotated)
        {
            bool isActed = OnRotationKeyDown(KeyMap.KeyRotateRight, action, out bool rotated);
            isRotated = rotated;
            return isActed;
        }

        public bool OnShiftLeft(Func<bool> action, out bool isShifted)
        {
            bool isActed = OnShiftKey(KeyMap.KeyShiftLeft, action, out bool shifted);
            isShifted = shifted;
            return isActed;
        }

        public bool OnShiftRight(Func<bool> action, out bool isShifted)
        {
            bool isActed = OnShiftKey(KeyMap.KeyShiftRight, action, out bool shifted);
            isShifted = shifted;
            return isActed;
        }

        public bool OnShiftDown(Func<bool> action, out bool isShifted)
        {
            bool isActed = OnShiftKey(KeyMap.KeyShiftDown, action, out bool shifted);
            isShifted = shifted;
            return isActed;
        }

        public bool OnNewGame(Action action)
        {
            return OnKeyDown(KeyMap.KeyNewGame, action);
        }

        public bool OnPauseGame(Func<bool> action, out bool isActed)
        {
            bool isPaused = OnKeyDown(KeyMap.KeyPause, action, out bool acted);
            isActed = acted;
            return isPaused;
        }

        private bool OnRotationKeyDown(KeyCode keyCode, Func<bool> action, out bool isRotated)
        {
            if (_info.IsBoardLocked() || !_info.BoardState().Equals(EBoardState.PieceInProgress) ||
                !Input.GetKeyDown(keyCode))
            {
                isRotated = false;
                return false;
            }

            isRotated = action.Invoke();
            Debug.Log($"[KEY PRESS] {keyCode}");
            return true;
        }

        private bool OnShiftKey(KeyCode keyCode, Func<bool> action, out bool isShifted)
        {
            if (_info.IsBoardLocked() || !_info.BoardState().Equals(EBoardState.PieceInProgress) ||
                !Input.GetKey(keyCode))
            {
                isShifted = false;
                return false;
            }

            StartCoroutine(WhileKeyPressedCoroutine(keyCode, action));
            Debug.Log($"[KEY PRESS] {keyCode}");
            isShifted = _isShifted;
            return true;
        }

        private bool OnKeyDown(KeyCode keyCode, Action action)
        {
            if (!Input.GetKeyDown(keyCode))
            {
                return false;
            }

            Debug.Log($"[KEY PRESS] {keyCode}");
            action.Invoke();
            return true;
        }

        private bool OnKeyDown(KeyCode keyCode, Func<bool> action, out bool isActed)
        {
            if (!Input.GetKeyDown(keyCode))
            {
                isActed = false;
                return false;
            }

            Debug.Log($"[KEY PRESS] {keyCode}");
            isActed = action.Invoke();
            return true;
        }

        private IEnumerator WhileKeyPressedCoroutine(KeyCode keyCode, Func<bool> action)
        {
            EventsHub.OnLockBoard.Trigger();
            _timerOfClick.UpdateTimeout();
            _isShifted = false;
            while (Input.GetKey(keyCode) && action.Invoke())
            {
                _isShifted = true;
                if (_timerOfClick.IsInProgress())
                {
                    yield return new WaitWhile(_timerOfClick.IsInProgress);
                }

                _timerOfMove.UpdateTimeout();
                yield return new WaitWhile(_timerOfMove.IsInProgress);
                Debug.Log($"[KEY PRESS LONG] {keyCode}");
            }

            _timerOfMove.ResetTimer();
            EventsHub.OnUnlockBoard.Trigger();
        }
    }
}