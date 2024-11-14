public interface IController
{
    void DetectAndExecutePieceRotation();
    void DetectAndExecutePieceShift();
    void DropPieceAsTimeout();
    void SpawnPieceAsWill();
    void SetNewGame();
}

