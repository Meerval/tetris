using Systems.Storage;
using TetrisData;
using UnityEngine;

namespace Menu.Scenes
{
    public class SceneOfGameOverMenu : SceneOfMenu
    {
        [SerializeField] protected GameObject gameOverTxtPrefab;

        protected override void AfterStart()
        {
            if (gameOverTxtPrefab == null || transform == null)
                Debug.LogError("Button prefab or canvasTransform not set!");
            else
                Instantiate(gameOverTxtPrefab, transform);
            EventsHub.OnStageChanged.Trigger(ETetrisScene.OnMenuOfFirstEnter);
            Storage.Instance.SaveGame();
        }
    }
}