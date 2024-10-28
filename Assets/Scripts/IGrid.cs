using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public interface IGrid
{
    void SetTiles(IEnumerable<Vector2Int> positionList, TileBase tile);
    void LockTiles(IEnumerable<Vector2Int> positionList, TileBase tile);
    bool ShiftTiles(Vector2Int[] currentPosition, Vector2Int[] newPosition);
    bool IsAvailablePosition(IEnumerable<Vector2Int> piecePosition);
    int ClearFullLines();
    void ClearAll();
}