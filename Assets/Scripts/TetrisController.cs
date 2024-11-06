using System;
using Timer;
using UnityEngine;

public class TetrisController : MonoBehaviour, IController
{
    public static Action OnGameStart;
    public static Action OnStepUp;
    public static Action<string> OnGameOver;

    private IGridController _tetrisGrid;

    private ITimer _dropTimer;
    private ITimer _moveTimer;

    private void Awake()
    {
        _tetrisGrid = GetComponent<TetrisGridController>();
        _dropTimer = GetComponent<PieceDropTimer>();
        _moveTimer = GetComponent<PieceMoveTimer>();
        Debug.Log("TetrisController awoke");
    }

    private void Start()
    {
        SetNewGame();
        Debug.Log("TetrisController started");
    }

    public bool DetectAndExecutePieceRotation()
    {
        bool isMoved = false;
        if (IsKeyDown(KeyCode.Q))
        {
            isMoved = _tetrisGrid.PieceRotateLeft();
        }
        else if (IsKeyDown(KeyCode.E))
        {
            isMoved = _tetrisGrid.PieceRotateRight();
        }

        return ResetTimersIf(isMoved, _moveTimer);
    }

    public bool DetectAndExecutePieceShift()
    {
        bool isMoved = false;
        if (IsKeyDown(KeyCode.S))
        {
            if (_tetrisGrid.PieceShiftDown()) _dropTimer.UpdateTimeout();
        }

        if (IsKeyDown(KeyCode.A))
        {
            isMoved = _tetrisGrid.PieceShiftLeft();
        }
        else if (IsKeyDown(KeyCode.D))
        {
            isMoved = _tetrisGrid.PieceShiftRight();
        }

        return ResetTimersIf(isMoved, _moveTimer);
    }

    private static bool IsKeyDown(KeyCode keyCode)
    {
        if (!Input.GetKeyDown(keyCode)) return false;
        Debug.Log($"A key was pressed on the keyboard: {keyCode}");
        return true;
    }

    public bool DetectAndExecutePieceHardDrop()
    {
        bool isMoved = false;
        if (IsKeyDown(KeyCode.Space))
        {
            isMoved = _tetrisGrid.PieceHardDrop();
        }

        return ResetTimersIf(isMoved, _moveTimer);
    }

    private bool ResetTimersIf(bool isReason, ITimer timer)
    {
        if (!isReason) return false;
        timer.UpdateTimeout();
        return true;
    }

    public void DetectTimeOutAndDropPiece()
    {
        if (_dropTimer.IsInProgress()) return;
        if (!_tetrisGrid.PieceShiftDown())
        {
            StepUp();
            return;
        }
        _dropTimer.UpdateTimeout();
        Debug.Log("Piece dropped by timer");
    }

    public void StepUp()
    {
        if (_dropTimer.IsInProgress()) return;
        _tetrisGrid.PieceLock();
        _tetrisGrid.ClearFullLines();
        if (_tetrisGrid.PieceSpawnRandom())
        {
            _dropTimer.UpdateTimeout();
            OnStepUp?.Invoke();
            return;
        }

        OnGameOver?.Invoke("There is no place to spawn new piece");
    }


    public void SetNewGame()
    {
        OnGameStart?.Invoke();
        _dropTimer.ResetTimer();
        _moveTimer.ResetTimer();
        _tetrisGrid.ClearAll();
        _tetrisGrid.PieceSpawnRandom();
    }
}