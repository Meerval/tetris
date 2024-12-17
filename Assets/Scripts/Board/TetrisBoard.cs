using Board.Data;
using Systems.Events;
using Systems.Storage;
using UnityEngine;

namespace Board
{
    public class TetrisBoard : MonoBehaviour
    {
        private TetrisController _controller;

        public void Start()
        {
            _controller = GetComponentInChildren<TetrisController>();
            Debug.Log("TetrisBoard class started");
        }

        public void Update()
        {
            _controller.DetectAndExecutePieceRotation();
            _controller.DetectAndExecutePieceShift();
            _controller.DropPieceAsTimeout();
            _controller.SpawnPieceAsWill();
        }
    }
}