using System.Collections.Generic;
using Pieces;
using Progress;

public interface IConditionController
{
    State Status();
    float PieceDropDelay();
    List<(int, IPiece)> SpawnPieces();
    int Level();
    int Score();
}