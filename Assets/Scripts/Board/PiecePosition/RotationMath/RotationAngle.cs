using UnityEngine;

namespace Board.PiecePosition.RotationMath
{
    public class RotationAngle
    {
        private int _idx;
        private int _direction;
        private const float Delay = Mathf.PI / 2f;
        
        public RotationAngle()
        {
            _direction = 0;
            _idx = 0;
        }

        private int Idx()
        {
            return CircleWrapper.Wrap(_idx, PositionsCount());
        }

        private static int PositionsCount()
        {
            return Mathf.RoundToInt(2 * Mathf.PI / Delay);
        }

        public void Add(Direction direction)
        {
            _direction = (int) direction;
            _idx += _direction;
        }

        public void Sub(Direction direction)
        {
            _direction = -(int) direction;
            _idx += _direction;
        }

        public float Radian()
        {
            float radians = _idx * Delay;
            float twoPi = 2 * Mathf.PI;
            radians %= twoPi;
            
            if (radians < -twoPi)
                radians += twoPi;
            else if (radians > twoPi)
                radians -= twoPi;

            return radians;
        }

        public int GetWallKickIdx()
        {
            int wallKickIndex = Idx() * 2;

            if (_direction < 0) {
                wallKickIndex--;
            }
            
            return CircleWrapper.Wrap(wallKickIndex,  PositionsCount() * 2);
        }
    }

    public enum Direction
    {
        Left = 1,
        Right = -1
    }
}