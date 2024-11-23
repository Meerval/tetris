using System.Linq;
using Board.Progress.UI;
using Board.Progress;
using UnityEngine;

namespace Board.Progress
{
    public abstract class DisplayableProgress<T1, T2> : Progress<T1, T2>, IDisplayable where T2 : MonoBehaviour
    {
        private IProgressText _textToDisplay;

        protected override void StartNewProgress()
        {
            _textToDisplay = GetComponentsInChildren<ProgressText>()
                .First(component => component.name.Contains(GetType().Name));
            
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