using Progress;
using Timer;
using UnityEngine;

public class TetrisController : MonoBehaviour, IGameController
{
    private IGridControler _controllable;

    private ITimer _dropTimer;
    private ITimer _moveTimer;

    private void Awake()
    {
        _controllable = GetComponent<TetrisGridController>();
        Debug.Log("TetrisController is awake");
    }

    private void Start()
    {
        InitNewGame();
    }

    public bool DetectAndExecutePieceRotation()
    {
        bool isMoved = false;
        if (IsKeyDown(KeyCode.Q))
        {
            isMoved = _controllable.PieceRotateLeft();
        }
        else if (IsKeyDown(KeyCode.E))
        {
            isMoved = _controllable.PieceRotateRight();
        }

        return ResetTimersIf(isMoved, _moveTimer);
    }

    public bool DetectAndExecutePieceShift()
    {
        bool isMoved = false;
        if (IsKeyDown(KeyCode.S))
        {
            if (_controllable.PieceShiftDown()) _dropTimer.UpdateTimeout();
        }

        if (IsKeyDown(KeyCode.A))
        {
            isMoved = _controllable.PieceShiftLeft();
        }
        else if (IsKeyDown(KeyCode.D))
        {
            isMoved = _controllable.PieceShiftRight();
        }

        return ResetTimersIf(isMoved, _moveTimer);
    }

    private static bool IsKeyDown(KeyCode keyCode)
    {
        if (!Input.GetKeyDown(keyCode)) return false;
        Debug.Log(string.Format("A key was pressed on the keyboard: {0}", keyCode));
        return true;
    }

    public bool DetectAndExecutePieceHardDrop()
    {
        bool isMoved = false;
        if (IsKeyDown(KeyCode.Space))
        {
            isMoved = _controllable.PieceHardDrop();
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
        if (!_controllable.PieceShiftDown())
        {
            StepUp();
            return;
        }

        _dropTimer.UpdateTimeout();
        Debug.Log("Step updated");
    }

    public void StepUp()
    {
        if (_dropTimer.IsInProgress()) return;
        _controllable.PieceLock();
        
        TetrisInfo.Instance.AddScore(new ScoreCalculator(_controllable.ClearFullLines()).Calculate());
        if (_controllable.PieceSpawnRandom())
        {
            _dropTimer.UpdateDelay(delay => delay - 0.001f);
            _dropTimer.UpdateTimeout();
            return;
        }

        TetrisInfo.Instance.OverGame("There is no place to spawn new piece");
    }
    

   public void CreateNewGame()
    {
        _controllable.ClearAll();
        InitNewGame();
    }

    private void InitNewGame()
    {
        TetrisInfo.Instance.StartGame();
        _dropTimer = new TetrisTimer(1f);
        _moveTimer = new TetrisTimer(0.5f);
        _controllable.PieceSpawnRandom();
    }
}