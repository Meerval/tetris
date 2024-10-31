using UnityEngine;

public class GameInfo
{
    private int _step;
    private int _score;
    private bool _isGameOver;
    private string _gameOverReason;

    public GameInfo()
    {
        _step = 1;
        _score = 0;
        _isGameOver = false;
    }

    public void PieceCountUp()
    {
        _step += 1;
        Debug.Log($"Level Upped: {_step}");
    }

    public void UpdateScore(int score)
    {
        _score += score;
        if (score != 0) Debug.Log($"Score Upped: {score}");
    }

    public void GameOver(string reason)
    {
        _isGameOver = true;
        _gameOverReason = reason;
        
        Debug.Log($"Game Over!\n " +
                  $"isGameOver = {_isGameOver}; reason = {_gameOverReason}; step = {_step}; score = {_score}");
    }

    public bool IsGameOver()
    {
        return _isGameOver;
    }

    public bool IsGameGoingOn()
    {
        return !_isGameOver;
    }

    public string GameOverReason()
    {
        return _gameOverReason;
    }
}