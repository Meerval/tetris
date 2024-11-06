public interface IController
{
    bool DetectAndExecutePieceRotation();
    bool DetectAndExecutePieceShift();
    bool DetectAndExecutePieceHardDrop();
    void DetectTimeOutAndDropPiece();
    void StepUp();
    void SetNewGame();
}

