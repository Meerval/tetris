using Timer;
using UnityEngine;

public class BoardController : MonoBehaviour, IBoardController
{
    private IBoardAction _controllable;
    private GameInfo _gameInfo;

    private ITimer _stepTimer;
    private ITimer _moveTimer;

    private void Awake()
    {
        Debug.Log("BoardController is awake");
        _controllable = GetComponent<BoardAction>();
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

    public GameInfo StepUp()
    {
        if (_stepTimer.IsInProgress()) return _gameInfo;
        if (!_controllable.PieceShiftDown()) return LevelUp();
        _stepTimer.UpdateTimeout();
        Debug.Log("Step updated");
        return _gameInfo;
    }

    public GameInfo LevelUp()
    {
        if (_stepTimer.IsInProgress()) return _gameInfo;
        _controllable.PieceLock();
        _gameInfo.UpdateScore(_controllable.ClearFullLines());
        if (_controllable.PieceSpawnRandom())
        {
            _stepTimer.UpdateDelay(delay => delay - 0.0001f);
            _stepTimer.UpdateTimeout();
            _gameInfo.PieceCountUp();
            return _gameInfo;
        }

        _gameInfo.GameOver("There is no place to spawn new piece");
        return _gameInfo;
    }

    public void CreateNewGame()
    {
        _controllable.ClearAll();
        InitNewGame();
    }

    private void InitNewGame()
    {
        _gameInfo = new GameInfo();
        _stepTimer = new TetrisTimer(1f);
        _moveTimer = new TetrisTimer(0.5f);
        _controllable.PieceSpawnRandom();
    }
}