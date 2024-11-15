using System.Linq;
using BoardGUI;
using UnityEngine;

namespace Progress
{
    public abstract class DisplayableProgress<T, V> : Progress<T, V>, IDisplayable where V : MonoBehaviour
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