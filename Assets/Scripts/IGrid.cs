using System;
using PositionCalculator;
using UnityEngine;

public interface IGrid
{
    bool SpawnNewPiece();
    bool ShiftPiece(Func<Vector2Int, IPositionClc> newPositionFunc);
    bool RotatePiece(Direction direction);
    public void LockPiece();
    public void ClearAll();
    public int ClearFullLines();

}