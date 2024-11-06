using Position.Math;
using UnityEngine;

namespace PiecePosition
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
            return Wrapper.Wrap(_idx, 0, PositionsCount());
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
            return _idx * Delay;
        }

        public int GetWallKickIdx()
        {
            int wallKickIndex = Idx() * 2;

            if (_direction < 0) {
                wallKickIndex--;
            }
            
            return Wrapper.Wrap(wallKickIndex, 0,  PositionsCount() * 2);
        }
    }

    public enum Direction
    {
        Left = 1,
        Right = -1
    }
}