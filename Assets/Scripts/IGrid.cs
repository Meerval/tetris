using System.Collections.Generic;
using UnityEngine;

public interface IGrid
{
    bool ShiftTiles(Vector2Int[] currentPosition, Vector2Int[] newPosition);
    bool IsAvailablePosition(IEnumerable<Vector2Int> piecePosition);
    void ClearLines();
}