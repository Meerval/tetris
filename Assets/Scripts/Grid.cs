using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Grid : MonoBehaviour, IGrid
{
    private static readonly Vector2Int Size = new(10, 20);
    private RectInt _bounds = new(new Vector2Int(-Size.x / 2, -Size.y / 2), Size);
    private Tilemap Tilemap { get; }

    public Grid()
    {
        Tilemap = GetComponentInChildren<Tilemap>();
    }

    public bool IsAvailablePosition(IEnumerable<Vector2Int> piecePosition)
    {
        return piecePosition.All(tile => _bounds.Contains(tile) && IsCellFree(tile));
    }

    public void ClearLines()
    {
        int row = _bounds.yMin;
        while (row < _bounds.yMax)
        {
            if (IsLineFull(row))
            {
                ClearLine(row);
            }
            else
            {
                row++;
            }
        }
    }

    private bool IsLineFull(int row)
    {
        for (int col = _bounds.xMin; col < _bounds.xMax; col++)
        {
            if (IsCellFree(new Vector2Int(col, row)))
            {
                return false;
            }
        }

        return true;
    }

    private void ClearLine(int row)
    {
        for (int col = _bounds.xMin; col < _bounds.xMax; col++)
        {
            SetTile(new Vector2Int(col, row), null);
        }

        while (row < _bounds.yMax)
        {
            for (int col = _bounds.xMin; col < _bounds.xMax; col++)
            {
                TileBase above = GetTile(new Vector2Int(col, row + 1));
                SetTile(new Vector2Int(col, row), above);
            }

            row++;
        }
    }

    public bool ShiftTiles(Vector2Int[] currentPosition, Vector2Int[] newPosition)
    {
        if (!IsAvailablePosition(newPosition)) return false;
        TileBase tile = GetCommonTile(currentPosition);
        ClearTiles(currentPosition);
        SetTiles(newPosition, tile);
        Array.Copy(newPosition, currentPosition, newPosition.Length);
        return true;
    }

    private void SetTiles(IEnumerable<Vector2Int> positionList, TileBase tile)
    {
        foreach (Vector2Int tilePosition in positionList)
        {
            SetTile(tilePosition, tile);
        }
    }

    private void ClearTiles(IEnumerable<Vector2Int> positionList)
    {
        foreach (Vector2Int tilePosition in positionList)
        {
            ClearTile(tilePosition);
        }
    }

    private void SetTile(Vector2Int position, TileBase tile)
    {
        Tilemap.SetTile((Vector3Int)position, tile);
    }

    private void ClearTile(Vector2Int position)
    {
        SetTile(position, null);
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
        return Tilemap.GetTile((Vector3Int)position);
    }

    private bool IsCellTaken(Vector2Int position)
    {
        return Tilemap.HasTile((Vector3Int)position);
    }

    private bool IsCellFree(Vector2Int position)
    {
        return !IsCellTaken(position);
    }
}