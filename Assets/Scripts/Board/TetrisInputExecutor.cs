using System;
using System.Collections;
using Board.Meta;
using Board.Timers;
using Systems.Events;
using Systems.Timer;
using UnityEngine;

namespace Board
{
    public class TetrisInputExecutor : MonoBehaviour, IInputExecutor
    {
        private TetrisMeta _meta;
        private ITimer _timerOfMove;
        private ITimer _timerOfClick;

        private void Awake()
        {
            _meta = FindObjectOfType<TetrisMeta>();
            _timerOfMove = FindObjectOfType<TimerOfPieceMove>();
            _timerOfClick = FindObjectOfType<TimerOfSingleClick>();
            Debug.Log("TetrisInputExecutor awoke");
        }

        public bool OnRotationLeft(Action action)
        {
            return IsRotationKeyDown(KeyCode.Q, action);
        }

        public bool OnRotationRight(Action action)
        {
            return IsRotationKeyDown(KeyCode.E, action);
        }

        public bool OnShiftLeft(Action action)
        {
            return IsShiftKey(KeyCode.A, action);
        }

        public bool OnShiftRight(Action action)
        {
            return IsShiftKey(KeyCode.D, action);
        }

        public bool OnShiftDown(Action action)
        {
            return IsShiftKey(KeyCode.S, action);
        }

        private bool IsRotationKeyDown(KeyCode keyCode, Action action)
        {
            if (_meta.IsUpdateLocked() || !_meta.State().Equals(EState.PieceInProgress) ||
                !Input.GetKeyDown(keyCode)) return false;
            action.Invoke();
            Debug.Log($"[KEY PRESS] {keyCode}");
            return true;
        }

        private bool IsShiftKey(KeyCode keyCode, Action action)
        {
            if (_meta.IsUpdateLocked() || !_meta.State().Equals(EState.PieceInProgress) ||
                !Input.GetKey(keyCode)) return false;
            StartCoroutine(WhileKeyPressedCoroutine(keyCode, action));
            Debug.Log($"[KEY PRESS] {keyCode}");
            return true;
        }

        private IEnumerator WhileKeyPressedCoroutine(KeyCode keyCode, Action action)
        {
            EventsHub.OnWaitCoroutineStart.Trigger();
            _timerOfClick.UpdateTimeout();
            while (Input.GetKey(keyCode))
            {
                action.Invoke();
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