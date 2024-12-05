namespace Systems.Accumulator
{
    public interface IAccumulator
    {
        public long Value { get; }
        public long Limit { get; }
        public void Increment();
        public void Decrement();
        public void Reset();
        public bool IsFull();
        public bool IsNotFull();
    }
}