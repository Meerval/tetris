using System;
using System.Collections.Generic;
using System.Linq;
using Pieces;
using PositionCalculator;
using Pretty;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TetrisGrid : MonoBehaviour, IGrid
{
    [SerializeField] private PieceSpawner pieceSpawner;
    [SerializeField] private TilemapController activeTilemap;
    [SerializeField] private TilemapController projectedTilemap;
    [SerializeField] private TileBase projectionTile;
    [SerializeField] private TilemapController lockedTilemap;

    private RectInt _bounds;
    private IPiece _activePiece;
    private readonly Vector2Int _spawnPosition = new(-1, 8);
    private Vector2Int _activePiecePosition;

    public void Awake()
    {
        Vector2Int size = Vector2Int.RoundToInt(GetComponent<SpriteRenderer>().size);
        _bounds = new RectInt(new Vector2Int(-size.x / 2, -size.y / 2), size);

        Debug.Log
        (
            $"StandardGrid is awake with size of {size.ToString()} and bounds {_bounds.ToString()}"
        );
    }


    public bool SpawnNewPiece()
    {
        if (_activePiece != null) _activePiece.Destroy();

        _activePiece = pieceSpawner.SpawnRandom();
        if (IsAvailablePosition(_spawnPosition))
        {
            _activePiecePosition = _spawnPosition;
            SetPiece(_activePiecePosition);
            return true;
        }

        return false;
    }

    private bool IsAvailablePosition(Vector2Int newPosition)
    {
        return CalculateTilesPosition(newPosition).All(cell =>
            {
                if (!_bounds.Contains(cell))
                {
                    Debug.Log($"Tile {cell.ToString()} is out of bounds {_bounds.ToString()}");
                    return false;
                }

                if (lockedTilemap.IsTaken(cell))
                {
                    Debug.Log($"Tile {cell.ToString()} is taken by Tile {lockedTilemap.Get(cell)}");
                    return false;
                }

                return true;
            }
        );
    }


    private Vector2Int[] CalculateTilesPosition(Vector2Int piecePosition)
    {
        return _activePiece.CurrentShapeMap().Select(cell => cell + piecePosition).ToArray();
    }

    private Vector2Int[] CalculateProjectedPosition(Vector2Int piecePosition)
    {
        Vector2Int NewCell(Vector2Int cell) => new(cell.x, cell.y - 1);
        Vector2Int[] projection = CalculateTilesPosition(piecePosition);

        while (projection.All(cell =>
                   {
                       Vector2Int newCell = NewCell(cell);
                       return _bounds.Contains(newCell) && lockedTilemap.IsFree(newCell);
                   }
               ))
        {
            projection = projection.Select(NewCell).ToArray();
        }

        return projection;
    }


    public bool ShiftPiece(Func<Vector2Int, IPositionClc> newPositionFunc)
    {
        Vector2Int newPosition = newPositionFunc.Invoke(_activePiecePosition).GetNewPosition();
        if (IsAvailablePosition(newPosition))
        {
            activeTilemap.ClearAll();
            projectedTilemap.ClearAll();
            SetPiece(newPosition);
            return true;
        }

        Debug.Log
        (
            string.Format("{0} can't be shifted from {1} to {2}",
                _activePiece,
                new PrettyArray<Vector2Int>(CalculateTilesPosition(_activePiecePosition)).Prettify(),
                new PrettyArray<Vector2Int>(CalculateTilesPosition(newPosition)).Prettify())
        );
        return false;
    }

    private void SetPiece(Vector2Int position)
    {
        foreach (Vector2Int tilePosition in CalculateTilesPosition(position))
        {
            activeTilemap.Set(tilePosition, _activePiece.Tile());
        }

        _activePiecePosition = position;

        foreach (Vector2Int tilePosition in CalculateProjectedPosition(_activePiecePosition))
        {
            if (activeTilemap.IsFree(tilePosition)) projectedTilemap.Set(tilePosition, projectionTile);
        }
    }

    public bool RotatePiece(Direction direction)
    {
        _activePiece.AddRotation(direction);
        Debug.Log(
            $"{_activePiece} rotation was settled to {new PrettyArray<Vector2Int>(_activePiece.CurrentShapeMap())}");
        if (
            // TODO: WallKicks take a lot of cells
            // _activePiece.HasNoWallKick(translation => IsAvailablePosition(translation + _activePiecePosition))
            // && 
            IsAvailablePosition(_activePiecePosition)
        )
        {
            activeTilemap.ClearAll();
            projectedTilemap.ClearAll();
            SetPiece(_activePiecePosition);
            return true;
        }

        _activePiece.SubRotation(direction);
        Debug.Log(
            $"{_activePiece} rotation was returned to {new PrettyArray<Vector2Int>(_activePiece.CurrentShapeMap())}");
        return false;
    }

    public void LockPiece()
    {
        Vector2Int[] tilesPosition = CalculateTilesPosition(_activePiecePosition);
        foreach (Vector2Int tilePosition in tilesPosition)
        {
            LockTile(tilePosition, _activePiece.Tile());
        }

        Debug.Log($"{_activePiece} locked on position {new PrettyArray<Vector2Int>(tilesPosition).Prettify()}");
    }

    public void ClearAll()
    {
        activeTilemap.ClearAll();
        projectedTilemap.ClearAll();
        lockedTilemap.ClearAll();
        Debug.Log("Whole grid cleared");
    }

    public int ClearFullLines()
    {
        List<int> clearedLines = new List<int>();

        int y = _bounds.yMin;
        while (y < _bounds.yMax)
        {
            if (IsLineFull(y))
            {
                ClearLine(y);
                clearedLines.Add(y);
            }
            else
            {
                y++;
            }
        }

        if (clearedLines.Count > 0)
        {
            Debug.Log($"Lines were cleared: {new PrettyArray<int>(clearedLines)}");
        }

        return clearedLines.Count;
    }

    private bool IsLineFull(int y)
    {
        return Enumerable
            .Range(_bounds.xMin, _bounds.width)
            .All(x => lockedTilemap.IsTaken(new Vector2Int(x, y)));
    }

    private void ClearLine(int y)
    {
        for (int x = _bounds.xMin; x < _bounds.xMax; x++)
        {
            lockedTilemap.Clear(new Vector2Int(x, y));
        }

        while (y < _bounds.yMax)
        {
            for (int x = _bounds.xMin; x < _bounds.xMax; x++)
            {
                TileBase above = lockedTilemap.Get(new Vector2Int(x, y + 1));
                lockedTilemap.Set(new Vector2Int(x, y), above);
            }

            y++;
            Debug.Log($"y < yMax = {y < _bounds.yMax}; y == {y}; yMax = {_bounds.yMax}");
        }
    }

    private void LockTile(Vector2Int position, TileBase tile)
    {
        activeTilemap.Clear(position);
        lockedTilemap.Set(position, tile);
    }
}