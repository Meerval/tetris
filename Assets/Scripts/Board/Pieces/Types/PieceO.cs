﻿using Board.PiecePosition;
using UnityEngine;

namespace Board.Pieces.Types
{
    public class PieceO : Piece
    {
        public override EPiece PieceType => EPiece.O;
        private readonly Vector2Int[] _shape = { new(0, 1), new(1, 1), new(0, 0), new(1, 0) };

        protected override Vector2Int[] ShapeMap()
        {
            return _shape;
        }

        protected override IShapeMap RotationCalculator()
        {
            return new RotationIO(ShapeMap(), RotationAngle);
        }
        
        public override Vector2Int MonitorOffset()
        {
            return new Vector2Int(-1, 0);
        }
    }
}