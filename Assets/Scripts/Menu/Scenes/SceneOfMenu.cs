using TetrisData;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Menu.Scenes
{
    public abstract class SceneOfMenu : MonoBehaviour
    {
        [SerializeField] protected GameObject startNewGameButtonPrefab;
        [SerializeField] protected GameObject settingsButtonPrefab;
        [SerializeField] protected GameObject exitButtonPrefab;


        private void Start()
        {
            InitButton(startNewGameButtonPrefab, StartNewGame);
            InitButton(settingsButtonPrefab, OpenSettings);
            InitButton(exitButtonPrefab, QuitGame);
            AfterStart();
        }

        protected virtual void AfterStart()
        {
            // To override as will
        }

        protected void InitButton(GameObject buttonPrefab, UnityAction onPressAction)
        {
            if (buttonPrefab == null || transform == null)
            {
                Debug.LogError($"'{buttonPrefab.name}' or 'canvasTransform' not set!");
                return;
            }

            GameObject newButton = Instantiate(buttonPrefab, transform);
            Button buttonComponent = newButton.GetComponent<Button>();

            if (buttonComponent == null) Debug.LogError($"Button component not found on prefab '{buttonPrefab.name}'!");

            buttonComponent.onClick.RemoveAllListeners();
            buttonComponent.onClick.AddListener(onPressAction);
        }
        
        private void StartNewGame()
        {
            LoadBoard(ETetrisScene.OnBoardOfNewGame);
        }
        
        protected void LoadBoard(ETetrisScene boardScene)
        {
            SceneManager.LoadScene("Tetris");
            EventsHub.OnUnlockBoard.Trigger();
            EventsHub.OnStageChanged.Trigger(boardScene);
            Debug.Log($"Scene 'Tetris' loaded with scene {boardScene.ToString()}");
        }

        private void OpenSettings()
        {
            Debug.Log("Settings Button Pressed");
        }

        private void QuitGame()
        {
            Debug.Log("Quit Button Pressed");
            Application.Quit();
        }
    }
}