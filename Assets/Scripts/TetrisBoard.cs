using System;
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
        _controller.DetectAndExecutePieceRotation();
        _controller.DetectAndExecutePieceShift();
        if (_controller.DetectAndExecutePieceHardDrop())
        {
            _controller.StepUp();
            return;
        }
        _controller.DetectTimeOutAndDropPiece();
    }

    private void GameOver(EGameOverReason _)
    {
        _controller.SetNewGame();
    }
}