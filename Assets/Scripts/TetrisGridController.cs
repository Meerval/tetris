using System.Collections;
using PiecePosition;
using PiecePosition.RotationMath;
using Timer;
using UnityEngine;

public class TetrisGridController : MonoBehaviour, IGridController
{
    private IGrid _grid;

    private void Awake()
    {
        _grid = GetComponent<TetrisGrid>();
        Debug.Log("TetrisGridController awoke");
    }

    public bool PieceSpawnRandom()
    {
        return _grid.SpawnNewPiece();
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
        if (!_grid.ShiftPiece(currentPosition => new ShiftLeft(currentPosition))) return false;
        Debug.Log("Piece shifted left");
        return true;
    }

    public bool PieceShiftRight()
    {
        if (!_grid.ShiftPiece(currentPosition => new ShiftRight(currentPosition))) return false;
        Debug.Log("Piece shifted right");
        return true;
    }

    public bool PieceShiftDown()
    {
        if (!_grid.ShiftPiece(currentPosition => new ShiftDown(currentPosition))) return false;
        Debug.Log("Piece shifted down");
        return true;
    }

    public void PieceHardDrop()
    {
        StartCoroutine(PieceHardDropCoroutine());
        Debug.Log("Piece hard dropped");
    }

    private IEnumerator PieceHardDropCoroutine()
    {
        int tryCount = 1000;
        int spawnedPiecesCount = TetrisProgressController.Instance.SpawnPieces().Count;
        while
        (
            tryCount > 0
            && spawnedPiecesCount == TetrisProgressController.Instance.SpawnPieces().Count
            && PieceShiftDown()
        )
        {
            TimerOfPieceHardDrop.Instance.UpdateTimeout();
            yield return new WaitWhile(TimerOfPieceHardDrop.Instance.IsInProgress);
            tryCount--;
        }

        Debug.Log("Piece hard dropped");
    }

    public void ClearFullLines()
    {
        _grid.ClearFullLines();
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