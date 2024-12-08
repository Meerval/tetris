using UnityEngine;

namespace Board.Pieces
{
    public class PieceJ : Piece
    {
        public override EPiece PieceType => EPiece.J;
        private readonly Vector2Int[] _shape = { new(-1, 1), new(-1, 0), new(0, 0), new(1, 0) };

        protected override Vector2Int[] ShapeMap()
        {
            return _shape;
        }
    }
}