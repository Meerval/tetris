using UnityEngine;

public class Board : MonoBehaviour
{
    [SerializeField] private BoardController controller;

    public void Start()
    {
        Debug.Log("Game is started");
        controller.CreateNewGame();
    }

    public void Update()
    {
        controller.DetectAndExecutePieceRotation();
        controller.DetectAndExecutePieceShift();
        if (controller.DetectAndExecutePieceHardDrop())
        {
            DetectGameOver(controller.LevelUp());
        }
        DetectGameOver(controller.StepUp());
    }

    private void DetectGameOver(GameInfo gameInfo)
    {
        if (gameInfo.IsGameGoingOn()) return;
        Debug.Log($"Game Over !!!\nlvl: {gameInfo.Level()}\nscore: {gameInfo.Score()}");
        controller.CreateNewGame();
    }
}