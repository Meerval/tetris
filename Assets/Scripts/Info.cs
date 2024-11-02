public interface IGameInfo
{
    void StartGame();
    void OverGame(string gameOverReason);
    void AddScore(int score);
    void LevelUp();
    void StepUp();
    GameState Status();
    int Score();
    int Level();
    int Step();
}