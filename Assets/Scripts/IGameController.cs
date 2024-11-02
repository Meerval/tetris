public interface IGameController
{
    bool DetectAndExecutePieceRotation();
    bool DetectAndExecutePieceShift();
    bool DetectAndExecutePieceHardDrop();
    TetrisState StepUp();
    TetrisState LevelUp();
    void CreateNewGame();
}

