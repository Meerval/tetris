using System;
using System.Collections;
using Event;
using Progress;
using Timer;
using UnityEngine;

public class TetrisController : MonoBehaviour, IController
{
    private IGridController _tetrisGrid;

    private IMeta _meta;
    private ITimer _timerOfDrop;
    private ITimer _timerOfLock;
    private ITimer _timerOfMove;
    private ITimer _timerOfClick;

    private void Awake()
    {
        _tetrisGrid = GetComponent<TetrisGridController>();
        _meta = FindObjectOfType<TetrisMeta>();
        _timerOfDrop = FindObjectOfType<TimerOfPieceDrop>();
        _timerOfLock = FindObjectOfType<TimerOfPieceLock>();
        _timerOfMove = FindObjectOfType<TimerOfPieceMove>();
        _timerOfClick = FindObjectOfType<TimerOfPieceMove>();
        Debug.Log("TetrisController awoke");
    }

    private void Start()
    {
        SetNewGame();
        Debug.Log("TetrisController started");
    }

    public void DetectAndExecutePieceRotation()
    {
        if (IsRotationKeyDown(KeyCode.Q)) _tetrisGrid.PieceRotateLeft();
        else if (IsRotationKeyDown(KeyCode.E)) _tetrisGrid.PieceRotateRight();
        else return;
        _timerOfLock.UpdateTimeout();
    }

    public void DetectAndExecutePieceShift()
    {
        if (IsShiftKey(KeyCode.S, () => _tetrisGrid.PieceShiftDown()))
        {
            _timerOfDrop.UpdateTimeout();
        }
        else if (IsShiftKey(KeyCode.A, () => _tetrisGrid.PieceShiftLeft()))
        {
        }
        else if (IsShiftKey(KeyCode.D, () => _tetrisGrid.PieceShiftRight()))
        {
        }
    }

    private bool IsRotationKeyDown(KeyCode keyCode)
    {
        if (_meta.IsUpdateLocked() || !_meta.State().Equals(EState.PieceInProgress) ||
            !Input.GetKeyDown(keyCode)) return false;
        Debug.Log($"[KEY PRESS] {keyCode}");
        return true;
    }

    private bool IsShiftKey(KeyCode keyCode, Action action)
    {
        if (_meta.IsUpdateLocked() || !_meta.State().Equals(EState.PieceInProgress) ||
            !Input.GetKey(keyCode)) return false;
        StartCoroutine(WhileKeyDown(keyCode, action));
        Debug.Log($"[KEY PRESS] {keyCode}");
        return true;
    }

    private IEnumerator WhileKeyDown(KeyCode keyCode, Action action)
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
        _timerOfLock.UpdateTimeout();
        EventsHub.OnWaitCoroutineEnd.Trigger();
    }

    public void DetectTimeOutAndDropPiece()
    {
        if (_meta.IsUpdateLocked() || _meta.State().Equals(EState.WaitForActivePiece) || _timerOfDrop.IsInProgress())
        {
            return;
        }

        if (_tetrisGrid.PieceShiftDown())
        {
            _timerOfDrop.UpdateTimeout();
        }

        else
        {
            if (_meta.IsUpdateLocked() || _timerOfLock.IsInProgress()) return;
            _tetrisGrid.PieceLock();
            _tetrisGrid.ClearFullLines();
            EventsHub.OnWaitForPiece.Trigger();
        }
    }

    public void SpawnPieceAsWill()
    {
        if (_meta.IsUpdateLocked() || !_meta.State().Equals(EState.WaitForActivePiece)) return;
        if (_tetrisGrid.PieceSpawnRandom())
        {
            _timerOfDrop.UpdateTimeout();
            return;
        }

        EventsHub.OnGameOver.Trigger(EGameOverReason.GridFilled);
    }

    public void SetNewGame()
    {
        EventsHub.OnGameStart.Trigger();
        _timerOfDrop.ResetTimer();
        _tetrisGrid.ClearAll();
    }
}