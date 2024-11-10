public interface IController
{
    void DetectAndExecutePieceRotation();
    void DetectAndExecutePieceShift();
    void DetectAndExecuteHardDrop();
    void DetectTimeOutAndDropPiece();
    void SetNewGame();
}

