using System;

namespace Systems.Events
{
    public interface IEvent
    {
        void AddSubscriber(Action handler, int priority = 0);
        void RemoveSubscriber(Action handler);
        void Trigger();
    }

    public interface IEvent<T>
    {
        void AddSubscriber(Action<T> handler, int priority = 0);
        void RemoveSubscriber(Action<T> handler);
        void Trigger(T t);
    }

    public interface IEvent<T1, T2>
    {
        void AddSubscriber(Action<T1, T2> handler, int priority = 0);
        void RemoveSubscriber(Action<T1, T2> handler);
        void Trigger(T1 t1, T2 t2);
    }
}