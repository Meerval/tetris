using UnityEngine;
using UnityEngine.Tilemaps;

public interface ITilemapController
{
    bool IsTaken(Vector2Int position);
    bool IsFree(Vector2Int position);
    TileBase Get(Vector2Int position);
    void Clear(Vector2Int position);
    void Set(Vector2Int position, TileBase tile);
    void ClearAll();
}