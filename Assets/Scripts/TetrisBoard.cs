using Event;
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

    private void OnEnable()
    {
        EventHab.OnGameOver.AddSubscriber(GameOver, 10);
    }


    private void OnDisable()
    {
        EventHab.OnGameOver.RemoveSubscriber(GameOver);
    }

    public void Update()
    {
        _controller.DetectAndExecuteHardDrop();
        _controller.DetectAndExecutePieceRotation();
        _controller.DetectAndExecutePieceShift();
        _controller.DetectTimeOutAndDropPiece();
        _controller.SpawnPieceAsWill();
    }

    private void GameOver(EGameOverReason reason)
    {
        _controller.SetNewGame();
        Debug.Log($"Game Over!\nreason: \"{reason}\"\nlevel: {TetrisProgress.Instance.Level()}, " +
                  $"score: {TetrisProgress.Instance.Score()}");
    }
}