﻿using UnityEngine;

namespace Board.Pieces.Types
{
    public class PieceL : Piece
    {
        public override EPiece PieceType => EPiece.L;
        private readonly Vector2Int[] _shape = { new(1, 1), new(-1, 0), new(0, 0), new(1, 0) };

        protected override Vector2Int[] ShapeMap()
        {
            return _shape;
        }
    }
}