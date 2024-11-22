using System.Collections.Generic;
using Pieces;

namespace Actions
{
    public interface IPieceSpawning
    {
        Queue<IPiece> Execute();
    }
}