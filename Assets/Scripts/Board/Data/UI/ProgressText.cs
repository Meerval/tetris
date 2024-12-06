using TMPro;
using UnityEngine;

namespace Board.Data.UI
{
    public class ProgressText : MonoBehaviour, IProgressText
    {
        private TextMeshProUGUI _textMesh;
        [SerializeField] private string progressName; 

        private void Awake()
        {
            _textMesh = GetComponent<TextMeshProUGUI>();
            if (_textMesh == null) Debug.LogError("TextMeshProUGUI is null");
        }

        public void Display(string progressValue)
        {
            _textMesh.text = $"{progressName}: {progressValue}";
        }

    }
}