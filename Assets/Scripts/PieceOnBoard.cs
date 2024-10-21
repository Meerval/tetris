using System;
using System.Linq;
using Math;
using Pieces;
using PositionCalculator;
using UnityEngine;

public class PieceOnBoard : IPieceOnBoard
{
    private PieceOnBoard(IPiece piece)
    {
        _piece = piece;
        Vector2Int spawnPosition = new(-1, 8);
        _currentPiecePosition = _piece.ShapeMap().Select(position => position + spawnPosition).ToArray();
        _rotationDirection = new RotationDirection();
    }

    private readonly IPiece _piece;
    private IGrid _grid;
    private Vector2Int[] _currentPiecePosition;
    private readonly RotationDirection _rotationDirection;

    public bool Rotate(RotationDirection rotationDirection)
    {
        _rotationDirection.Add(rotationDirection);
        Vector2Int[] newPosition = _piece.RotationType() switch
        {
            RotationType.A => new RotationPositionCalculatorA(_currentPiecePosition, _rotationDirection)
                .GetNewPiecePosition(),
            RotationType.B => new RotationPositionCalculatorB(_currentPiecePosition, _rotationDirection)
                .GetNewPiecePosition(),
            _ => throw new ArgumentOutOfRangeException()
        };
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
            .All(shift => !_grid.IsAvailablePosition(position.Select(tile => tile + shift)));
    }

    public bool ShiftLeft()
    {
        return ShiftPiece(new ShiftPositionCalculatorLeft(_currentPiecePosition).GetNewPiecePosition());
    }

    private bool ShiftPiece(Vector2Int[] newPosition)
    {
        bool isPieceReplaced = _grid.ShiftTiles(_currentPiecePosition, newPosition);
        if (isPieceReplaced)
        {
            _currentPiecePosition = newPosition;
        }

        return isPieceReplaced;
    }

    public bool ShiftRight()
    {
        return ShiftPiece(new ShiftPositionCalculatorRight(_currentPiecePosition).GetNewPiecePosition());
    }

    public bool ShiftDown()
    {
        return ShiftPiece(new ShiftPositionCalculatorDown(_currentPiecePosition).GetNewPiecePosition());
    }

    public bool HardDrop()
    {
        while (ShiftDown())
        {
        }

        return true;
    }
}