using System;
using System.Globalization;
using UnityEngine;

namespace Timer
{
    public class TetrisTimer : ITimer
    {
        private readonly float _delay;
        private float _delayTmp;
        private float _timeout;

        public TetrisTimer(float delay)
        {
            _delay = delay;
            _delayTmp = delay;
            UpdateTimeout();
        }

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
            _timeout = Time.time + _delayTmp;
        }

        public void UpdateDelay(Func<float, float> delayFunc)
        {
            _delayTmp = delayFunc.Invoke(_delayTmp);
        }
    }
}