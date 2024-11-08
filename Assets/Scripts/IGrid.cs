using System;
using PiecePosition;
using PiecePosition.RotationMath;
using UnityEngine;

public interface IGrid
{
    bool SpawnNewPiece();
    bool ShiftPiece(Func<Vector2Int, IPosition> newPositionFunc);
    bool RotatePiece(Direction direction);
    public void LockPiece();
    public void ClearAll();
    public void ClearFullLines();

}