using Menu.Scenes;
using TetrisData;
using UnityEngine;

namespace Menu
{
    public class MainMenuController : MonoBehaviour
    {
        [SerializeField] private Transform transformCanvas;
        [SerializeField] private SceneOfFirstEnterMenu firstEnterMenu;
        [SerializeField] private SceneOfGameOverMenu gameOverMenu;
        [SerializeField] private SceneOfPauseMenu pauseMenu;

        private void Start()
        {
            EventsHub.OnLockBoard.Trigger();
            switch (BoardInfo.Instance.TetrisStage())
            {
                case ETetrisScene.OnMenuOfFirstEnter:
                    Instantiate(firstEnterMenu, transformCanvas);
                    break;
                case ETetrisScene.OnMenuOfGameOver:
                    Instantiate(gameOverMenu, transformCanvas);
                    break;
                case ETetrisScene.OnBoard:
                case ETetrisScene.OnBoardOfNewGame:
                case ETetrisScene.OnMenuOfPause:
                default:
                    Instantiate(pauseMenu, transformCanvas);
                    break;
            }
        }
    }
}