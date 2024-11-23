namespace Board
{
    public interface IGridController
    {
        bool PieceSpawnRandom();
        bool PieceRotateLeft();
        bool PieceRotateRight();
        bool PieceShiftLeft();
        bool PieceShiftRight();
        bool PieceShiftDown();
        void PieceLock();
        void ClearFullLines();
        void ClearAll();
    }
}