public interface IBoardController
{
    bool DetectAndExecutePieceRotation();
    bool DetectAndExecutePieceShift();
    bool DetectAndExecutePieceHardDrop();
    GameInfo StepUp();
    GameInfo LevelUp();
    void CreateNewGame();
}

