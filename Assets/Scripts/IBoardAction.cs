public interface IBoardAction
{
    bool PieceSpawnRandom();
    bool PieceRotateLeft();
    bool PieceRotateRight();
    bool PieceShiftLeft();
    bool PieceShiftRight();
    bool PieceShiftDown();
    bool PieceHardDrop();
    void PieceLock();
    int ClearFullLines();
    void ClearAll();
}