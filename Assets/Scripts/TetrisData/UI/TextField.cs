using TMPro;
using UnityEngine;

namespace TetrisData.UI
{
    public abstract class TextField<T> : MonoBehaviour, ITextField
    {
        private TextMeshProUGUI _textMesh;
        [SerializeField] private string fieldName;

        private void Awake()
        {
            _textMesh = GetComponent<TextMeshProUGUI>();
            if (_textMesh == null) Debug.LogError("TextMeshProUGUI is null");
        }

        private void Start()
        {
            Show();
        }

        private void Update()
        {
            Show();
        }

        public void Show()
        {
            _textMesh.text = $"{fieldName}: {Data.Value().ToString()}";
        }
        protected abstract ITetrisData<T> Data { get; }
    }
}