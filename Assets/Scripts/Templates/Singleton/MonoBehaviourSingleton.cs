﻿using UnityEngine;

namespace Templates.Singleton
{
    public abstract class MonoBehaviourSingleton<T> : MonoBehaviour where T : MonoBehaviour
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
            BeforeAwake();
            
            if (_instance == null)
            {
                _instance = this as T;
                if (transform.parent == null) DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }

            AfterAwake();
        }

        protected virtual void BeforeAwake()
        {
        }

        protected virtual void AfterAwake()
        {
        }
    }
}