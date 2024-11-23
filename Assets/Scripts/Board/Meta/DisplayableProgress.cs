using System.Linq;
using Board.Meta.UI;
using Board.Meta;
using UnityEngine;

namespace Board.Meta
{
    public abstract class DisplayableProgress<T1, T2> : Progress<T1, T2>, IDisplayable where T2 : MonoBehaviour
    {
        private IProgressText _textToDisplay;

        protected override void StartNewProgress()
        {
            _textToDisplay = GetComponentInChildren<ProgressText>();
            
            StartNewDisplayableProgress();
            DisplayCurrentValue();
        }

        protected abstract void StartNewDisplayableProgress();
        protected abstract override void SubscribeProgressAction();
        protected abstract override void UnsubscribeProgressAction();

        public void DisplayCurrentValue()
        {
            _textToDisplay.Display(CurrentValue.ToString());
        }
    }
}