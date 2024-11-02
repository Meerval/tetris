public interface IGameController
{
    bool DetectAndExecutePieceRotation();
    bool DetectAndExecutePieceShift();
    bool DetectAndExecutePieceHardDrop();
    void DetectTimeOutAndDropPiece();
    void StepUp();
    void CreateNewGame();
}

