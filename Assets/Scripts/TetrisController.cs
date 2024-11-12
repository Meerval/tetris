using Event;
using Progress;
using Timer;
using UnityEngine;

public class TetrisController : MonoBehaviour, IController
{
    private IGridController _tetrisGrid;
    private bool _hasToWait;

    private void OnEnable()
    {
        EventHab.OnWaitCoroutineStart.AddSubscriber(HasToWait);
        EventHab.OnWaitCoroutineStop.AddSubscriber(HasNotToWait);
    }

    private void OnDisable()
    {
        EventHab.OnWaitCoroutineStart.RemoveSubscriber(HasToWait);
        EventHab.OnWaitCoroutineStop.RemoveSubscriber(HasNotToWait);
    }

    private void HasToWait()
    {
        _hasToWait = true;
    }

    private void HasNotToWait()
    {
        _hasToWait = false;
    }


    private void Awake()
    {
        _tetrisGrid = GetComponent<TetrisGridController>();
        _hasToWait = false;
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

    private bool IsKeyDown(KeyCode keyCode)
    {
        if (
            _hasToWait
            || !TetrisProgress.Instance.State().Equals(EState.PieceInProgress)
            || !Input.GetKeyDown(keyCode)
        ) return false;
        Debug.Log($"[KEY PRESSED] {keyCode}");
        return true;
    }

    public void DetectTimeOutAndDropPiece()
    {
        if (
            _hasToWait
            || TimerOfPieceDrop.Instance.IsInProgress()
            || TetrisProgress.Instance.State().Equals(EState.WaitForActivePiece)
        ) return;
        if (_tetrisGrid.PieceShiftDown())
        {
            TimerOfPieceDrop.Instance.UpdateTimeout();
        }
        else
        {
            if (_hasToWait || TimerOfPieceMove.Instance.IsInProgress()) return;
            _tetrisGrid.PieceLock();
            _tetrisGrid.ClearFullLines();
            EventHab.OnWaitForActivePiece.Trigger();
        }
    }

    public void SpawnPieceAsWill()
    {
        if (_hasToWait || !TetrisProgress.Instance.State().Equals(EState.WaitForActivePiece)) return;
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
    }
}