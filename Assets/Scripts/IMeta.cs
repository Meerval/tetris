using System.Collections.Generic;
using Pieces;
using Progress;

public interface IMeta
{
    EState State();
    bool IsUpdateLocked();
    float PieceDropDelay();
    List<(int, IPiece)> SpawnPieces();
    IPiece ActivePiece();
    int Level();
    int Score();
}