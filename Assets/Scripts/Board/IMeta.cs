using System.Collections.Generic;
using Board.Progress;
using Board.Pieces;

namespace Board
{
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
}