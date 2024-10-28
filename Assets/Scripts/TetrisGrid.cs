using System;
using System.Collections.Generic;
using System.Linq;
using Pretty;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TetrisGrid : MonoBehaviour, IGrid
{
    private RectInt _bounds;
    [SerializeField] private Tilemap activeTiles;
    [SerializeField] private Tilemap lockedTiles;

    public void Awake()
    {
        Vector2Int size = Vector2Int.RoundToInt(GetComponent<SpriteRenderer>().size);
        _bounds = new RectInt(new Vector2Int(-size.x / 2, -size.y / 2), size);
        Debug.Log($"StandardGrid is awake with size of {size.ToString()} and bounds {_bounds.ToString()}");
    }

    public bool IsAvailablePosition(IEnumerable<Vector2Int> piecePosition)
    {
        return piecePosition.All(tile =>
            {
                if (!_bounds.Contains(tile))
                {
                    Debug.Log($"Tile {tile.ToString()} is out of bounds {_bounds.ToString()}");
                    return false;
                }
                if (IsCellTaken(tile))
                {
                    Debug.Log($"Tile {tile.ToString()} is taken");
                    return false;
                }
                return true;
            }
        );
    }

    public void ClearAll()
    {
        activeTiles.ClearAllTiles();
        lockedTiles.ClearAllTiles();
        Debug.Log("Whole grid cleared");
    }

    public int ClearFullLines()
    {
        int clearedLinesCount = 0;
        int row = _bounds.yMin;
        while (row < _bounds.yMax)
        {
            if (IsLineFull(row))
            {
                ClearLine(row);
                Debug.Log($"Row cleared: {row}");
                clearedLinesCount++;
            }
            else
            {
                row++;
            }
        }

        return clearedLinesCount;
    }

    private bool IsLineFull(int row)
    {
        for (int col = _bounds.xMin; col < _bounds.xMax; col++)
        {
            if (IsCellFree(new Vector2Int(col, row))) return false;
        }

        return true;
    }

    private void ClearLine(int row)
    {
        for (int col = _bounds.xMin; col < _bounds.xMax; col++)
        {
            ClearTileFromLockedTilemap(new Vector2Int(col, row));
        }

        while (row < _bounds.yMax)
        {
            for (int col = _bounds.xMin; col < _bounds.xMax; col++)
            {
                TileBase above = GetTile(new Vector2Int(col, row + 1));
                SetTileToLockedTilemap(new Vector2Int(col, row), above);
            }

            row++;
        }
    }

    public bool ShiftTiles(Vector2Int[] currentPosition, Vector2Int[] newPosition)
    {
        if (!IsAvailablePosition(newPosition))
        {
            Debug.LogWarning($"Piece can't be shifted from {new PrettyVector2IntArray(currentPosition).Prettify()} " +
                             $"to {new PrettyVector2IntArray(newPosition).Prettify()}");
            return false;
        }

        TileBase tile = GetCommonTile(currentPosition);
        ClearTiles(currentPosition);
        SetTiles(newPosition, tile);
        Array.Copy(newPosition, currentPosition, newPosition.Length);
        Debug.Log($"Piece shifted from {new PrettyVector2IntArray(currentPosition).Prettify()} " +
                  $"to {new PrettyVector2IntArray(newPosition).Prettify()}");
        return true;
    }

    public void SetTiles(IEnumerable<Vector2Int> positionList, TileBase tile)
    {
        foreach (Vector2Int tilePosition in positionList)
        {
            SetTileToActiveTilemap(tilePosition, tile);
        }
    }

    public void LockTiles(IEnumerable<Vector2Int> positionList, TileBase tile)
    {
        foreach (Vector2Int tilePosition in positionList)
        {
            LockTile(tilePosition, tile);
        }
        Debug.Log($"Piece locked on position {new PrettyVector2IntArray(positionList).Prettify()}");
    }

    private void ClearTiles(IEnumerable<Vector2Int> positionList)
    {
        foreach (Vector2Int tilePosition in positionList)
        {
            ClearTileFromActiveTilemap(tilePosition);
        }
    }

    private void SetTileToActiveTilemap(Vector2Int position, TileBase tile)
    {
        activeTiles.SetTile((Vector3Int)position, tile);
    }

    private void LockTile(Vector2Int position, TileBase tile)
    {
        ClearTileFromActiveTilemap(position);
        SetTileToLockedTilemap(position, tile);
    }

    private void SetTileToLockedTilemap(Vector2Int position, TileBase tile)
    {
        lockedTiles.SetTile((Vector3Int)position, tile);
    }

    private void ClearTileFromActiveTilemap(Vector2Int position)
    {
        SetTileToActiveTilemap(position, null);
    }

    private void ClearTileFromLockedTilemap(Vector2Int position)
    {
        SetTileToLockedTilemap(position, null);
    }

    private TileBase GetCommonTile(Vector2Int[] piecePosition)
    {
        List<TileBase> tiles = piecePosition.Select(GetTile).ToList();
        TileBase tile = tiles.Aggregate((tileA, tileB) => tileA.Equals(tileB)
            ? tileA
            : throw new NotSupportedException($"There are different tiles in one pieces: {tiles}")
        );
        return tile;
    }

    private TileBase GetTile(Vector2Int position)
    {
        return activeTiles.GetTile((Vector3Int)position);
    }

    private bool IsCellTaken(Vector2Int position)
    {
        return lockedTiles.HasTile((Vector3Int)position);
    }

    private bool IsCellFree(Vector2Int position)
    {
        return !IsCellTaken(position);
    }
}