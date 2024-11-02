using System;
using UnityEngine;

public class Board : MonoBehaviour
{
    private IBoardController _controller;

    public void Start()
    {
        _controller = GetComponent<BoardController>();
        _controller.CreateNewGame();
        Debug.Log("Game is started");
    }

    public void Update()
    {
        _controller.DetectAndExecutePieceRotation();
        _controller.DetectAndExecutePieceShift();
        if (_controller.DetectAndExecutePieceHardDrop())
        {
            DetectGameOver(_controller.LevelUp());
            return;
        }
        DetectGameOver(_controller.StepUp());
    }

    private void DetectGameOver(GameInfo gameInfo)
    {
        if (gameInfo.IsGameGoingOn()) return;
        _controller.CreateNewGame();
    }
}