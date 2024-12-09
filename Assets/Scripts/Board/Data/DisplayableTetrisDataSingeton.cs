using Board.Data.UI;
using UnityEngine;

namespace Board.Data
{
    public abstract class DisplayableTetrisDataSingleton<T1, T2> : TetrisDataSingleton<T1, T2>, IDisplayable
        where T2 : MonoBehaviour
    {
        private ITextField _textFieldToDisplay;

        public void Start()
        {
            _textFieldToDisplay = GetComponentInChildren<TextField>();
            _textFieldToDisplay.Show(CurrentValue.ToString());
        }

        public void DisplayCurrentValue()
        {
            _textFieldToDisplay.Show(CurrentValue.ToString());
        }

        protected override void SubscribeDataAction()
        {
            EventsHub.OnNewGameStart.AddSubscriber(DisplayCurrentValue, 1);
            SubscribeDisplayableDataAction();
        }

        protected override void UnsubscribeDataAction()
        {
            EventsHub.OnNewGameStart.RemoveSubscriber(DisplayCurrentValue);
            UnsubscribeDisplayableDataAction();
        }

        protected abstract void SubscribeDisplayableDataAction();
        protected abstract void UnsubscribeDisplayableDataAction();
    }
}