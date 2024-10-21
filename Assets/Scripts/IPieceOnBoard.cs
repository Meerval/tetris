using PositionCalculator;

public interface IPieceOnBoard
{
    bool Rotate(RotationDirection direction);
    bool ShiftLeft();
    bool ShiftRight();
    bool ShiftDown();
    bool HardDrop();
}