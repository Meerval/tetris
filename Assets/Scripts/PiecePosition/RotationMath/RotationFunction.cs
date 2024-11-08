using UnityEngine;

namespace PiecePosition.RotationMath
{
    public class RotationFunction
    {
        private readonly Vector2 _position;
        private readonly float _radian;

        public RotationFunction(Vector2 position, RotationAngle angle)
        {
            _position = new Vector2
            {
                x = position.x,
                y = position.y
            };
            _radian = angle.Radian();
        }

        public Vector2 GetRotatedPosition()
        {
            return new Vector2(XPosition(), YPosition());
        }

        private float XPosition()
        {
            return _position.x * Cos() - _position.y * Sin();
        }

        private float YPosition()
        {
            return _position.x * Sin() + _position.y * Cos();
        }

        private float Sin()
        {
            return Mathf.Sin(_radian);
        }

        private float Cos()
        {
            return Mathf.Cos(_radian);
        }
    }
}