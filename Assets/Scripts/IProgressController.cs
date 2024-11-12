using System.Collections.Generic;
using Pieces;
using Progress;

public interface IConditionController
{
    EState State();
    float PieceDropDelay();
    List<(int, IPiece)> SpawnPieces();
    IPiece ActivePiece();
    int Level();
    int Score();
}