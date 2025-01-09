using Board;
using TetrisData;
using UnityEngine;

namespace Menu.MainMenuScenes
{
    public class SceneOfPauseMenu : SceneOfMenu
    {
        [SerializeField] protected GameObject continueButtonPrefab;

        protected override void AfterStart()
        {
            InitButton(continueButtonPrefab, Continue);
        }

        public void Continue()
        {
            LoadBoard(ETetrisScene.OnBoard);
        }

        private void Update()
        {
            if (!Input.GetKeyDown(KeyMap.KeyContinue)) return;
            Debug.Log($"[KEY PRESS] {KeyMap.KeyContinue.ToString()}");
            Continue();
            Debug.Log("Game continued by keyboard");
        }
    }
}