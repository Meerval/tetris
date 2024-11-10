using System.Globalization;
using UnityEngine;


namespace Timer
{
    public abstract class TimerOfTetris<T> : MonoBehaviour, ITimer where T : MonoBehaviour, ITimer
    {
        private static T _instance;
        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<T>();
                    if (_instance == null)
                        return new GameObject(typeof(T).Name).AddComponent<T>();
                    return _instance;
                }
                return _instance;
            }
        }
        
        protected virtual void Awake()
        {
            if (_instance == null)
            {
                _instance = this as T;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
        
        
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