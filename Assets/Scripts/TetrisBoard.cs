using System;
using Event;
using Progress;
using Timer;
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
        if (TimerOfPieceHardDrop.Instance.IsInProgress() || TimerOfClearLine.Instance.IsInProgress()) return;
        _controller.DetectAndExecuteHardDrop();
        _controller.DetectAndExecutePieceRotation();
        _controller.DetectAndExecutePieceShift();
        _controller.DetectTimeOutAndDropPiece();
    }

    private void GameOver(EGameOverReason _)
    {
        _controller.SetNewGame();
    }
}