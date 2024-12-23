using Systems.Storage;
using Templates.Singleton;
using TetrisData.Storable;
using TetrisData.Storable.Displayable;
using UnityEngine;

namespace TetrisData
{
    public class TetrisInfo : MonoBehaviourSingleton<TetrisInfo>
    {
        private ITetrisData<ETetrisStage> _tetrisStage;
        private ITetrisData<long> _score;
        private ITetrisData<long> _recordScore;

        public void Start()
        {
            _tetrisStage = gameObject.AddComponent<TetrisStage>();
            _score = GetComponentInChildren<Score>();
            _recordScore = GetComponentInChildren<RecordScore>();

            Storage.Instance.LoadGame();
            Debug.Log("TetrisInfo started");
        }

        public ETetrisStage TetrisStage()
        {
            return _tetrisStage.Value();
        }

        public long Score()
        {
            return _score.Value();
        }

        public long RecordScore()
        {
            return _recordScore.Value();
        }
    }
}