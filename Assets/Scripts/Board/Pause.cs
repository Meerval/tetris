using UnityEngine;

namespace Board
{
    public class Pause : MonoBehaviour
    {
        private TetrisController _controller;

        public void Start()
        {
            _controller = GetComponentInChildren<TetrisController>();
            Debug.Log("Pause class started");
        }

        private void Update()
        {
            _controller.DetectAndExecutePause();
            _controller.DetectAndExecuteUnpause();
        }
    }
}