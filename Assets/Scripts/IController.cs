public interface IController
{
    void DetectAndExecutePieceRotation();
    void DetectAndExecutePieceShift();
    void DetectTimeOutAndDropPiece();
    void SpawnPieceAsWill();
    void SetNewGame();
}

