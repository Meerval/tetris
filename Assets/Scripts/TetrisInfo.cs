using UnityEngine;

public class TetrisInfo : MonoBehaviour, IGameInfo
{
    private int _score;
    private int _level;
    private int _step;
    private GameState _gameState;

    public static IGameInfo Instance;

    private TetrisInfo()
    {
    }

    public void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        _score = 0;
        _step = 0;
        _level = 0;
        _gameState = GameState.InExpectation;
    }

    public void StartGame()
    {
        _score = 0;
        _step = 1;
        _level = 1;
        _gameState = GameState.InProgress;
        Debug.Log("Game Started!");
    }

    public void OverGame(string gameOverReason)
    {
        _gameState = GameState.Over;
        Debug.Log($"Game Over!\nreason: \"{gameOverReason}\";\nscore: {_score}");
    }

    public void AddScore(int score)
    {
        if (score == 0) return;
        _score += score;
        Debug.Log($"Score Updated: {_score}");
    }

    public void LevelUp()
    {
        _level += 1;
        Debug.Log($"Level Updated: {_level}");
    }

    public void StepUp()
    {
        _step += 1;
        Debug.Log($"Step Updated: {_step}");
    }

    public GameState Status()
    {
        return _gameState;
    }

    public int Score()
    {
        return _score;
    }

    public int Level()
    {
        return _level;
    }

    public int Step()
    {
        return _step;
    }
}

public enum GameState
{
    InExpectation = -1,
    InProgress = 1,
    Over = 0
}