using UnityEngine;

public class TetrisBoard : MonoBehaviour
{
    private IGameController _gameController;
    
    public void Start()
    {
        _gameController = GetComponent<TetrisController>();
        _gameController?.CreateNewGame();
        Debug.Log("Game is started");
    }

    public void Update()
    {
        _gameController.DetectAndExecutePieceRotation();
        _gameController.DetectAndExecutePieceShift();
        if (_gameController.DetectAndExecutePieceHardDrop())
        {
            _gameController.StepUp();
            DetectGameOver();
            return;
        }

        _gameController.DetectTimeOutAndDropPiece();
        DetectGameOver();
    }

    private void DetectGameOver()
    {
        if (TetrisInfo.Instance.Status() != GameState.Over) return;
        _gameController.CreateNewGame();
    }
}