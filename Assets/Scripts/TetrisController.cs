using Timer;
using UnityEngine;

public class TetrisController : MonoBehaviour, IGameController
{
    private IGridControler _controllable;
    private TetrisState _tetrisState;

    private ITimer _stepTimer;
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
            if (_controllable.PieceShiftDown()) _stepTimer.UpdateTimeout();
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

    public TetrisState StepUp()
    {
        if (_stepTimer.IsInProgress()) return _tetrisState;
        if (!_controllable.PieceShiftDown()) return LevelUp();
        _stepTimer.UpdateTimeout();
        Debug.Log("Step updated");
        return _tetrisState;
    }

    public TetrisState LevelUp()
    {
        if (_stepTimer.IsInProgress()) return _tetrisState;
        _controllable.PieceLock();
        _tetrisState.UpdateScore(_controllable.ClearFullLines());
        if (_controllable.PieceSpawnRandom())
        {
            _stepTimer.UpdateDelay(delay => delay - 0.0001f);
            _stepTimer.UpdateTimeout();
            _tetrisState.PieceCountUp();
            return _tetrisState;
        }

        _tetrisState.GameOver("There is no place to spawn new piece");
        return _tetrisState;
    }

    public void CreateNewGame()
    {
        _controllable.ClearAll();
        InitNewGame();
    }

    private void InitNewGame()
    {
        _tetrisState = new TetrisState();
        _stepTimer = new TetrisTimer(1f);
        _moveTimer = new TetrisTimer(0.5f);
        _controllable.PieceSpawnRandom();
    }
}