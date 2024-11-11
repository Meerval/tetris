using Event;
using Progress;
using Timer;
using UnityEngine;

public class TetrisController : MonoBehaviour, IController
{
    private IGridController _tetrisGrid;

    private void Awake()
    {
        _tetrisGrid = GetComponent<TetrisGridController>();
        Debug.Log("TetrisController awoke");
    }

    private void Start()
    {
        SetNewGame();
        Debug.Log("TetrisController started");
    }

    public void DetectAndExecutePieceRotation()
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

        if (isMoved) TimerOfPieceMove.Instance.UpdateTimeout();
    }

    public void DetectAndExecutePieceShift()
    {
        bool isMoved = false;
        if (IsKeyDown(KeyCode.S))
        {
            if (_tetrisGrid.PieceShiftDown()) TimerOfPieceDrop.Instance.UpdateTimeout();
        }

        if (IsKeyDown(KeyCode.A))
        {
            isMoved = _tetrisGrid.PieceShiftLeft();
        }
        else if (IsKeyDown(KeyCode.D))
        {
            isMoved = _tetrisGrid.PieceShiftRight();
        }

        if (isMoved) TimerOfPieceMove.Instance.UpdateTimeout();
    }

    public void DetectAndExecuteHardDrop()
    {
        if (IsKeyDown(KeyCode.Space))
        {
            _tetrisGrid.PieceHardDrop();
            TimerOfPieceMove.Instance.UpdateTimeout();
        }
    }

    private static bool IsKeyDown(KeyCode keyCode)
    {
        if (!Input.GetKeyDown(keyCode)) return false;
        Debug.Log($"A key was pressed on the keyboard: {keyCode}");
        return true;
    }

    public void DetectTimeOutAndDropPiece()
    {
        if (TimerOfPieceDrop.Instance.IsInProgress() || TimerOfClearLine.Instance.IsInProgress()) return;
        if (!_tetrisGrid.PieceShiftDown())
        {
            TetrisStepUp();
            return;
        }

        TimerOfPieceDrop.Instance.UpdateTimeout();
    }

    private void TetrisStepUp()
    {
        if (TimerOfPieceMove.Instance.IsInProgress()) return;
        _tetrisGrid.PieceLock();
        _tetrisGrid.ClearFullLines();
        if (_tetrisGrid.PieceSpawnRandom())
        {
            TimerOfPieceDrop.Instance.UpdateTimeout();
            return;
        }

        EventHab.OnGameOver.Trigger(EGameOverReason.GridFilled);
    }


    public void SetNewGame()
    {
        EventHab.OnGameStart.Trigger();
        TimerOfPieceDrop.Instance.ResetTimer();
        TimerOfPieceMove.Instance.ResetTimer();
        _tetrisGrid.ClearAll();
        _tetrisGrid.PieceSpawnRandom();
    }
}