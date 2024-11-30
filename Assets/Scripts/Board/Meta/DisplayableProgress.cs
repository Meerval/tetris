using Board.Meta.UI;
using UnityEngine;

namespace Board.Meta
{
    public abstract class DisplayableProgress<T1, T2> : Progress<T1, T2>, IDisplayable where T2 : MonoBehaviour
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
    }
}