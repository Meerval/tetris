using PositionCalculator;
using UnityEngine;

public class BoardAction : MonoBehaviour, IBoardAction
{
    [SerializeField] private TetrisGrid grid;

    public void Awake()
    {
        Debug.Log("BoardAction is awake");
        grid = FindObjectOfType<TetrisGrid>();
    }

    public bool PieceSpawnRandom()
    {
        if (!grid.SpawnNewPiece()) return false;
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
        return grid.RotatePiece(direction);
    }

    public bool PieceShiftLeft()
    {
        if (!grid.ShiftPiece(currentPosition => new ShiftClcLeft(currentPosition))) return false;
        Debug.Log("Piece shifted left");
        return true;
    }

    public bool PieceShiftRight()
    {
        if (!grid.ShiftPiece(currentPosition => new ShiftClcRight(currentPosition))) return false;
        Debug.Log("Piece shifted right");
        return true;
    }

    public bool PieceShiftDown()
    {
        if (!grid.ShiftPiece(currentPosition => new ShiftClcDown(currentPosition))) return false;
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
        return grid.ClearFullLines();
    }

    public void ClearAll()
    {
        grid.ClearAll();
    }

    public void PieceLock()
    {
        grid.LockPiece();
    }
}