using UnityEngine;

namespace Structure
{
    public abstract class MonoBehaviourSingleton<T> : MonoBehaviour, IMonoBehaviourSingleton where T : MonoBehaviour
    {
        private static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance != null) return _instance;
                _instance = FindObjectOfType<T>();
                return _instance == null ? new GameObject(typeof(T).Name).AddComponent<T>() : _instance;
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
    }
}