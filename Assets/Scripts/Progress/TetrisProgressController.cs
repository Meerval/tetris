using UnityEngine;

namespace Progress
{
    public class TetrisProgressController : MonoBehaviour, IConditionController
    {
        private IProgress<State> _state;
        private IProgress<int> _level;
        private IProgress<int> _score;
        private IProgress<float> _pieceDropDelay;

        public static IConditionController Instance;

        private TetrisProgressController()
        {
        }

        public void Start()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
                return;
            }

            DontDestroyOnLoad(gameObject);

            _score = gameObject.AddComponent<Score>();
            _level = gameObject.AddComponent<Level>();
            _pieceDropDelay = gameObject.AddComponent<PieceDropDelay>();
            _state = gameObject.AddComponent<Status>();
        }

        public State Status()
        {
            return _state.Value();
        }

        public float PieceDropDelay()
        {
            return _pieceDropDelay.Value();
        }

        public int Level()
        {
            return _level.Value();
        }

        public int Score()
        {
            return _score.Value();
        }
    }
}