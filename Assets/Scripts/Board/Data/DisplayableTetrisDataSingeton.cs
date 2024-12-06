using Board.Data.UI;
using UnityEngine;

namespace Board.Data
{
    public abstract class DisplayableTetrisDataSingleton<T1, T2> : TetrisDataSingleton<T1, T2>, IDisplayable where T2 : MonoBehaviour
    {
        private IProgressText _textToDisplay;

        public void Start()
        {
            _textToDisplay = GetComponentInChildren<ProgressText>();
            _textToDisplay.Display(CurrentValue.ToString());
        }
        
        public void DisplayCurrentValue()
        {
            _textToDisplay.Display(CurrentValue.ToString());
        }
        
        protected override void SubscribeProgressAction()
        {
            EventsHub.OnGameStart.AddSubscriber(DisplayCurrentValue);
            SubscribeDisplayableDataAction();
        }

        protected override void UnsubscribeProgressAction()
        {
            EventsHub.OnGameStart.RemoveSubscriber(DisplayCurrentValue);
            UnsubscribeDisplayableDataAction();
        }

        protected abstract void SubscribeDisplayableDataAction();
        protected abstract void UnsubscribeDisplayableDataAction();
    }
}