using System.Collections.Generic;
using Board.Pieces;

namespace Board.Actions
{
    public interface IPieceSpawning
    {
        Queue<IPiece> Execute();
        public IPiece ExecuteShadow(EPiece piece);
    }
}