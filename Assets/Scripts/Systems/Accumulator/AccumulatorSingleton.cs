using Templates.Singleton;

namespace Systems.Accumulator
{
    abstract class AccumulatorSingleton<T> : SimpleSingleton<T>, IAccumulator where T : new()
    {
        private readonly Accumulator _instance;
        public long Value => _instance.Value;
        public long Limit => _instance.Limit;
        
        protected AccumulatorSingleton(long limit)
        {
            _instance = new Accumulator(limit);
        }

        public void Increment()
        {
            _instance.Increment();
        }

        public void Decrement()
        {
            _instance.Decrement();
        }

        public void Reset()
        {
           _instance.Reset();
        }

        public bool IsFull()
        {
            return _instance.IsFull();
        }

        public bool IsNotFull()
        {
            return _instance.IsNotFull();
        }

        public override string ToString()
        {
            return $"{typeof(T).Name} : {Value}/{Limit}";
        }
    }
}