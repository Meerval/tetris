using System;
using System.Collections;
using Board.Meta;
using Board.Timers;
using Systems.Chrono.Timer;
using Systems.Events;
using UnityEngine;

namespace Board
{
    public class TetrisInputExecutor : MonoBehaviour, IInputExecutor
    {
        private TetrisMeta _meta;
        private ITimer _timerOfMove;
        private ITimer _timerOfClick;
        private bool _isShifted;

        private void Awake()
        {
            _meta = FindObjectOfType<TetrisMeta>();
            _timerOfMove = FindObjectOfType<TimerOfPieceMove>();
            _timerOfClick = FindObjectOfType<TimerOfSingleClick>();
            Debug.Log("TetrisInputExecutor awoke");
        }

        public bool OnRotationLeft(Func<bool> action, out bool isRotated)
        {
            bool isActed = IsRotationKeyDown(KeyCode.Q, action, out bool rotated);
            isRotated = rotated;
            return isActed;
        }

        public bool OnRotationRight(Func<bool> action, out bool isRotated)
        {
            bool isActed = IsRotationKeyDown(KeyCode.E, action, out bool rotated);
            isRotated = rotated;
            return isActed;
        }

        public bool OnShiftLeft(Func<bool> action, out bool isShifted)
        {
            bool isActed = IsShiftKey(KeyCode.A, action, out bool shifted);
            isShifted = shifted;
            return isActed;
        }

        public bool OnShiftRight(Func<bool> action, out bool isShifted)
        {
            bool isActed = IsShiftKey(KeyCode.D, action, out bool shifted);
            isShifted = shifted;
            return isActed;
        }

        public bool OnShiftDown(Func<bool> action, out bool isShifted)
        {
            bool isActed = IsShiftKey(KeyCode.S, action, out bool shifted);
            isShifted = shifted;
            return isActed;
        }

        private bool IsRotationKeyDown(KeyCode keyCode, Func<bool> action, out bool isRotated)
        {
            if (_meta.IsUpdateLocked() || !_meta.State().Equals(EState.PieceInProgress) ||
                !Input.GetKeyDown(keyCode))
            {
                isRotated = false;
                return false;
            }

            isRotated = action.Invoke();
            Debug.Log($"[KEY PRESS] {keyCode}");
            return true;
        }

        private bool IsShiftKey(KeyCode keyCode, Func<bool> action, out bool isShifted)
        {
            if (_meta.IsUpdateLocked() || !_meta.State().Equals(EState.PieceInProgress) ||
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

        private IEnumerator WhileKeyPressedCoroutine(KeyCode keyCode, Func<bool> action)
        {
            EventsHub.OnWaitCoroutineStart.Trigger();
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
            EventsHub.OnWaitCoroutineEnd.Trigger();
        }
    }
}