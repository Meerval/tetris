using System;
using System.Collections.Generic;
using System.Linq;
using Pieces;
using PositionCalculator;
using Pretty;
using UnityEngine;

public class BoardAction : MonoBehaviour, IBoardAction
{
    private IPiece _piece;
    [SerializeField] private TetrisGrid grid;
    private readonly Vector2Int _spawnPosition = new(-1, 8);
    private Vector2Int[] _currentPiecePosition;
    private RotationDirection _rotationDirection;

    public void Awake()
    {
        Debug.Log("BoardAction is awake");
        grid = FindObjectOfType<TetrisGrid>();
        _rotationDirection = new RotationDirection();
    }

    public bool PieceSpawnRandom()
    {
        _rotationDirection = new RotationDirection();
        _piece = SpawnRandomPieceFunc().Invoke();
        _currentPiecePosition = _piece.ShapeMap().Select(position => position + _spawnPosition).ToArray();
        if (grid.IsAvailablePosition(_currentPiecePosition))
        {
            grid.SetTiles(_currentPiecePosition, _piece.Tile());
            Debug.Log(
                $"A new piece was spawned: {_piece}, {new PrettyVector2IntArray(_currentPiecePosition).Prettify()}");
            return true;
        }
        Debug.Log($"Not valid position for: {_piece}, {new PrettyVector2IntArray(_currentPiecePosition).Prettify()}");
        return false;
    }

    private static Func<IPiece> SpawnRandomPieceFunc()
    {
        var pieceSuppliers = new List<Func<IPiece>>
        {
            FindObjectOfType<PieceI>,
            FindObjectOfType<PieceJ>,
            FindObjectOfType<PieceL>,
            FindObjectOfType<PieceO>,
            FindObjectOfType<PieceS>,
            FindObjectOfType<PieceT>,
            FindObjectOfType<PieceZ>,
        };
        return pieceSuppliers[UnityEngine.Random.Range(0, pieceSuppliers.Count)];
    }

    public bool PieceRotateLeft()
    {
        if (!Rotate(new RotationDirection(Rotation.Left))) return false;
        Debug.Log("A piece rotated left");
        return true;
    }

    public bool PieceRotateRight()
    {
        if (!Rotate(new RotationDirection(Rotation.Right))) return false;
        Debug.Log("A piece rotated right");
        return true;
    }

    private bool Rotate(RotationDirection rotationDirection)
    {
        _rotationDirection.Add(rotationDirection);
        Vector2Int[] newPosition = _piece.RotationCalculator(_currentPiecePosition, _rotationDirection)
            .GetNewPiecePosition();
        if (HasNoWallKick(newPosition) && ShiftPiece(newPosition)) return true;

        _rotationDirection.Subtract(rotationDirection);
        return false;
    }

    private bool HasNoWallKick(Vector2Int[] position)
    {
        Vector2Int[,] wallKicksMap = _piece.WallKicksMap();

        int x = _piece.GetWallKickIndex(_rotationDirection);
        return Enumerable.Range(0, wallKicksMap.GetLength(1))
            .Select(y => wallKicksMap[x, y])
            .All(shift => !grid.IsAvailablePosition(position.Select(tile => tile + shift)));
    }

    public bool PieceShiftLeft()
    {
        if (!ShiftPiece(new ShiftPositionCalculatorLeft(_currentPiecePosition).GetNewPiecePosition())) return false;
        Debug.Log("A piece shifted left");
        return true;
    }

    private bool ShiftPiece(Vector2Int[] newPosition)
    {
        bool isPieceReplaced = grid.ShiftTiles(_currentPiecePosition, newPosition);
        if (isPieceReplaced)
        {
            _currentPiecePosition = newPosition;
        }

        return isPieceReplaced;
    }

    public bool PieceShiftRight()
    {
        if (!ShiftPiece(new ShiftPositionCalculatorRight(_currentPiecePosition).GetNewPiecePosition())) return false;
        Debug.Log("A piece shifted right");
        return true;
    }

    public bool PieceShiftDown()
    {
        if (!ShiftPiece(new ShiftPositionCalculatorDown(_currentPiecePosition).GetNewPiecePosition())) return false;
        Debug.Log("A piece shifted down");
        return true;
    }

    public bool PieceHardDrop()
    {
        while (PieceShiftDown())
        {
        }
        Debug.Log("A piece hard dropped");
        return true;
    }

    public int ClearFullLines()
    {
        return grid.ClearFullLines();
    }

    public void ClearAll()
    {
        grid.ClearAll();
    }

    public void PieceLock()
    {
        grid.LockTiles(_currentPiecePosition, _piece.Tile());
    }
}