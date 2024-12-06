﻿using Templates.Singleton;
using UnityEngine;

namespace Systems.Accumulator
{
    class Accumulator: IAccumulator
    {
        public long Value { get; private set; }
        public long Limit { get; }

        public Accumulator(long limit)
        {
            Value = 0;
            Limit = limit;
        }

        public void Increment()
        {
            if (IsNotFull())
            {
                Value++;
            }
            else Debug.LogWarning($"Accumulator '{this}' is full and cannot be incremented");
        }

        public void Decrement()
        {
            if (Value > 0) 
            {
                Value--;
            }
            else Debug.LogWarning($"Accumulator '{this}' is empty and cannot be decremented");
        }

        public void Reset()
        {
            Value = 0;
        }

        public bool IsFull()
        {
            return Value == Limit;
        }

        public bool IsNotFull()
        {
            return Value < Limit;
        }

        public override string ToString()
        {
            return $"Accumulator : {Value}/{Limit}";
        }
    }
}