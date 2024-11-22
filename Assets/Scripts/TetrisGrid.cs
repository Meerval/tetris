using System;
using System.Linq;
using Actions;
using Event;
using PiecePosition;
using PiecePosition.RotationMath;
using Pieces;
using Pretty;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TetrisGrid : MonoBehaviour, IGrid
{
    [SerializeField] private TileBase projectionTile;
    [SerializeField] private TilemapController tilemapPrefab;
    [SerializeField] private PieceSpawning pieceSpawning;

    private ITilemapController _activeTilemap;
    private ITilemapController _projectedTilemap;
    private ITilemapController _lockedTilemap;

    private Vector2Int _size;
    private RectInt _bounds;

    private IPiece _activePiece;
    private readonly Vector2Int _spawnPosition = new(-1, 8);
    private Vector2Int _activePiecePosition;

    private IFullLinesRemoving _fullLinesRemoving;

    public void Awake()
    {
        _size = Vector2Int.RoundToInt(GetComponent<SpriteRenderer>().size);
        _bounds = new RectInt(new Vector2Int(-_size.x / 2, -_size.y / 2), _size);

        _activeTilemap = Instantiate(tilemapPrefab, gameObject.transform);
        _projectedTilemap = Instantiate(tilemapPrefab, gameObject.transform);
        _lockedTilemap = Instantiate(tilemapPrefab, gameObject.transform);

        ((TilemapController)_activeTilemap).gameObject.name = "ActiveTilemap";
        ((TilemapController)_projectedTilemap).gameObject.name = "ProjectedTilemap";
        ((TilemapController)_lockedTilemap).gameObject.name = "LockedTilemap";

        _fullLinesRemoving = gameObject.AddComponent<FullLinesRemoving>();

        Debug.Log
        (
            $"TetrisGrid awoke with size of {_size.ToString()} and bounds {_bounds.ToString()}"
        );
    }

    public bool SpawnNewPiece()
    {
        _activePiece?.Destroy();
        _activePiece = pieceSpawning.Execute().Dequeue();
        if (!IsAvailablePosition(_spawnPosition)) return false;
        _activePiecePosition = _spawnPosition;
        SetPiece(_activePiecePosition);
        EventsHub.OnSpawnPiece.Trigger(_activePiece);
        return true;
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

                if (_lockedTilemap.IsTaken(cell))
                {
                    Debug.Log($"Tile {cell.ToString()} is taken by Tile {_lockedTilemap.Get(cell)}");
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
                       return _bounds.Contains(newCell) && _lockedTilemap.IsFree(newCell);
                   }
               ))
        {
            projection = projection.Select(NewCell).ToArray();
        }

        return projection;
    }


    public bool ShiftPiece(Func<Vector2Int, IPosition> newPositionFunc)
    {
        Vector2Int newPosition = newPositionFunc.Invoke(_activePiecePosition).GetNewPosition();
        if (IsAvailablePosition(newPosition))
        {
            _activeTilemap.ClearAll();
            _projectedTilemap.ClearAll();
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
            _activeTilemap.Set(tilePosition, _activePiece.Tile());
        }

        _activePiecePosition = position;

        foreach (Vector2Int tilePosition in CalculateProjectedPosition(_activePiecePosition))
        {
            if (_activeTilemap.IsFree(tilePosition)) _projectedTilemap.Set(tilePosition, projectionTile);
        }
    }

    public bool RotatePiece(Direction direction)
    {
        _activePiece.AddRotation(direction);
        Debug.Log($"{_activePiece} rotated to {new PrettyArray<Vector2Int>(_activePiece.CurrentShapeMap())}");
        if (
            _activePiece.HasNoWallKick(cell => _lockedTilemap.IsFree(cell + _activePiecePosition))
            && IsAvailablePosition(_activePiecePosition)
        )
        {
            _activeTilemap.ClearAll();
            _projectedTilemap.ClearAll();
            SetPiece(_activePiecePosition);
            return true;
        }

        _activePiece.SubRotation(direction);
        Debug.Log($"{_activePiece} rotation returned to {new PrettyArray<Vector2Int>(_activePiece.CurrentShapeMap())}");
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

    public void ClearFullLines()
    {
        _fullLinesRemoving.Execute(_lockedTilemap, _bounds);
    }

    public void ClearAll()
    {
        _activeTilemap.ClearAll();
        _projectedTilemap.ClearAll();
        _lockedTilemap.ClearAll();
        Debug.Log("Whole grid cleared");
    }

    private void LockTile(Vector2Int position, TileBase tile)
    {
        _activeTilemap.Clear(position);
        _lockedTilemap.Set(position, tile);
    }
}