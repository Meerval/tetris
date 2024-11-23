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
}