﻿using UnityEngine;

namespace Board.Pieces
{
    public class PieceS : Piece
    {
        public override EPiece PieceType => EPiece.S;
        private readonly Vector2Int[] _shape = { new(0, 1), new(1, 1), new(-1, 0), new(0, 0) };

        protected override Vector2Int[] ShapeMap()
        {
            return _shape;
        }
    }
}