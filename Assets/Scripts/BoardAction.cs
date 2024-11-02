using PositionCalculator;
using UnityEngine;

public class BoardAction : MonoBehaviour, IBoardAction
{
    private TetrisGrid _grid;

    public void Awake()
    {
        _grid = GetComponentInChildren<TetrisGrid>();
        Debug.Log("BoardAction is awake");
    }

    public bool PieceSpawnRandom()
    {
        if (!_grid.SpawnNewPiece()) return false;
        return true;
    }

    public bool PieceRotateLeft()
    {
        if (!Rotate(Direction.Left)) return false;
        Debug.Log("Piece rotated left");
        return true;
    }

    public bool PieceRotateRight()
    {
        if (!Rotate(Direction.Right)) return false;
        Debug.Log("Piece rotated right");
        return true;
    }

    private bool Rotate(Direction direction)
    {
        return _grid.RotatePiece(direction);
    }

    public bool PieceShiftLeft()
    {
        if (!_grid.ShiftPiece(currentPosition => new ShiftClcLeft(currentPosition))) return false;
        Debug.Log("Piece shifted left");
        return true;
    }

    public bool PieceShiftRight()
    {
        if (!_grid.ShiftPiece(currentPosition => new ShiftClcRight(currentPosition))) return false;
        Debug.Log("Piece shifted right");
        return true;
    }

    public bool PieceShiftDown()
    {
        if (!_grid.ShiftPiece(currentPosition => new ShiftClcDown(currentPosition))) return false;
        Debug.Log("Piece shifted down");
        return true;
    }

    public bool PieceHardDrop()
    {
        while (PieceShiftDown())
        {
        }

        Debug.Log("Piece hard dropped");
        return true;
    }

    public int ClearFullLines()
    {
        return _grid.ClearFullLines();
    }

    public void ClearAll()
    {
        _grid.ClearAll();
    }

    public void PieceLock()
    {
        _grid.LockPiece();
    }
}