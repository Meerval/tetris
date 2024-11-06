using System.Globalization;
using UnityEngine;


namespace Timer
{
    public abstract class TetrisTimer : MonoBehaviour, ITimer
    {
        [SerializeField] protected float delay;
        private float _timeout;

        public bool IsInProgress()
        {
            return !IsTimedOut();
        }

        public bool IsTimedOut()
        {
            return Time.time > _timeout;
        }

        public void UpdateTimeout()
        {
            _timeout = Time.time + delay;
        }

        public abstract void ResetTimer();

        public override string ToString()
        {
            return $"delay: {delay}, current time: {Time.time.ToString(CultureInfo.InvariantCulture)}, " +
                   $"timeout: {_timeout.ToString(CultureInfo.InvariantCulture)}";
        }
    }
}