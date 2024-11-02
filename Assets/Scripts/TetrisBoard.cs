using UnityEngine;

public class TetrisBoard : MonoBehaviour
{
    private IGameController _gameController;

    public void Start()
    {
        _gameController = GetComponent<TetrisController>();
        _gameController.CreateNewGame();
        Debug.Log("Game is started");
    }

    public void Update()
    {
        _gameController.DetectAndExecutePieceRotation();
        _gameController.DetectAndExecutePieceShift();
        if (_gameController.DetectAndExecutePieceHardDrop())
        {
            DetectGameOver(_gameController.LevelUp());
            return;
        }
        DetectGameOver(_gameController.StepUp());
    }

    private void DetectGameOver(TetrisState tetrisState)
    {
        if (tetrisState.IsGameGoingOn()) return;
        _gameController.CreateNewGame();
    }
}