public class GameInfo
{
    private int _level;
    private int _score;
    private bool _isGameOver;

    public GameInfo()
    {
        _level = 1;
        _score = 0;
        _isGameOver = false;
    }

    public void LevelUp()
    {
        _level += 1;
    }

    public int Level()
    {
        return _level;
    }

    public void UpdateScore(int score)
    {
        _score += score;
    }
    
    public int Score()
    {
        return _score;
    }

    public void GameOver()
    {
        _isGameOver = true;
    }

    public bool IsGameOver()
    {
        return _isGameOver;
    }

    public bool IsGameGoingOn()
    {
        return !_isGameOver;
    }
}