using TMPro;
using UnityEngine;

namespace Board.Data.UI
{
    public class TextField : MonoBehaviour, ITextField
    {
        private TextMeshProUGUI _textMesh;
        [SerializeField] private string fieldName; 

        private void Awake()
        {
            _textMesh = GetComponent<TextMeshProUGUI>();
            if (_textMesh == null) Debug.LogError("TextMeshProUGUI is null");
        }

        public void Show(string value)
        {
            _textMesh.text = $"{fieldName}: {value}";
        }

    }
}