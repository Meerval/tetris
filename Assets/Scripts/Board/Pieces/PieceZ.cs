using UnityEngine;

namespace Board.Pieces
{
    public class PieceZ : Piece
    {
        
        public override EPiece PieceType => EPiece.Z;
        private readonly Vector2Int[] _shape = {new(-1, 1), new(0, 1), new(0, 0), new(1, 0)};

        protected override Vector2Int[] ShapeMap()
        {
            return _shape;
        }
    }
}