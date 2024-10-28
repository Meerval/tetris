using Math;

namespace PositionCalculator
{
    public class RotationDirection
    {
        private int _value;

        public RotationDirection(Rotation rotation)
        {
            _value =  (int) rotation;
        }

        public RotationDirection()
        {
            _value = 0;
        }

        public int Value()
        {
            return Wrapper.Wrap(_value, 0, 4);
        }

        public void Right()
        {
            _value++;
        }

        public void Left()
        {
            _value--;
        }

        public void Add(RotationDirection rotationDirection)
        {
            _value += rotationDirection._value;
        }

        public void Subtract(RotationDirection rotationDirection)
        {
            _value -= rotationDirection._value;
        }
    }

    public enum Rotation
    {
        Left = -1, Right = 1
    }
}