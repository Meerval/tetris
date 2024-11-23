using System;
using Board.PiecePosition;
using Board.PiecePosition.RotationMath;
using UnityEngine;

namespace Board
{
    public interface IGrid
    {
        bool SpawnNewPiece();
        bool ShiftPiece(Func<Vector2Int, IPosition> newPositionFunc);
        bool RotatePiece(Direction direction);
        public void LockPiece();
        public void ClearAll();
        public void ClearFullLines();
    }
}