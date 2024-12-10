using System;
using System.Collections.Generic;
using System.Linq;

namespace Systems.Events
{
    public class TetrisEvent : IEvent
    {
        private List<(int priority, Action subscriber)> _subscribers = new();

        public void AddSubscriber(Action subscriber, int priority = 0)
        {
            _subscribers.Add((priority, subscriber));
            _subscribers = _subscribers.OrderBy(h => h.priority).ToList();
        }

        public void RemoveSubscriber(Action subscriber)
        {
            _subscribers.RemoveAll(h => h.subscriber == subscriber);
        }

        public void Trigger()
        {
            foreach ((int _, Action subscriber) in _subscribers)
            {
                subscriber?.Invoke();
            }
        }
    }

    public class TetrisEvent<T> : IEvent<T>
    {
        private List<(int priority, Action<T> subscriber)> _subscribers = new();

        public void AddSubscriber(Action<T> subscriber, int priority = 0)
        {
            _subscribers.Add((priority, subscriber));
            _subscribers = _subscribers.OrderBy(h => h.priority).ToList();
        }

        public void RemoveSubscriber(Action<T> subscriber)
        {
            _subscribers.RemoveAll(h => h.subscriber == subscriber);
        }

        public void Trigger(T t)
        {
            foreach ((int _, Action<T> subscriber) in _subscribers)
            {
                subscriber?.Invoke(t);
            }
        }
    }

    public class TetrisEvent<T1, T2> : IEvent<T1, T2>
    {
        private List<(int priority, Action<T1, T2> subscriber)> _subscribers = new();

        public void AddSubscriber(Action<T1, T2> subscriber, int priority = 0)
        {
            _subscribers.Add((priority, subscriber));
            _subscribers = _subscribers.OrderBy(h => h.priority).ToList();
        }

        public void RemoveSubscriber(Action<T1, T2> subscriber)
        {
            _subscribers.RemoveAll(h => h.subscriber == subscriber);
        }

        public void Trigger(T1 t1, T2 t2)
        {
            foreach ((int _, Action<T1, T2> subscriber) in _subscribers)
            {
                subscriber?.Invoke(t1, t2);
            }
        }
    }
}