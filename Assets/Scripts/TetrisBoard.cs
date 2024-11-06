using Progress;
using UnityEngine;

public class TetrisBoard : MonoBehaviour
{
    private IController _controller;
    
    public void Start()
    {
        _controller = GetComponentInChildren<TetrisController>();
        Debug.Log("TetrisBoard started");
    }

    public void Update()
    {
        _controller.DetectAndExecutePieceRotation();
        _controller.DetectAndExecutePieceShift();
        if (_controller.DetectAndExecutePieceHardDrop())
        {
            _controller.StepUp();
            DetectGameOver();
            return;
        }
        _controller.DetectTimeOutAndDropPiece();
        DetectGameOver();
    }

    private void DetectGameOver()
    {
        if (TetrisProgressController.Instance.Status() != State.Over) return;
        _controller.SetNewGame();
    }
}