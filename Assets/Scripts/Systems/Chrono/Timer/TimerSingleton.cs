﻿using System.Globalization;
using Templates.Singleton;
using UnityEngine;

namespace Systems.Chrono.Timer
{
    public abstract class TimerSingleton<T> : MonoBehaviourSingleton<T>, ITimer where T : MonoBehaviour, ITimer
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

        public virtual void UpdateTimeout()
        {
            _timeout = Time.time + delay;
        }

        public abstract void ResetTimer();

        public override string ToString()
        {
            return $"Timer {typeof(T).Name} = delay: {delay}, current time: {Time.time.ToString(CultureInfo.InvariantCulture)}, " +
                   $"timeout: {_timeout.ToString(CultureInfo.InvariantCulture)}";
        }
    }
}