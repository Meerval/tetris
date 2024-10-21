using UnityEngine;

namespace PositionCalculator
{
    public abstract class ShiftPositionCalculator : IPositionCalculator
    {
        private readonly Vector2Int[] _currentPiecePosition;

        protected ShiftPositionCalculator(Vector2Int[] currentPiecePosition)
        {
            _currentPiecePosition = currentPiecePosition;
        }
        
        protected Vector2Int[] GetNewPositionOf(Vector2Int translation)
        {
            var newPosition = new Vector2Int[_currentPiecePosition.Length];

            for (var i = 0; i < _currentPiecePosition.Length; i++)
            {
                Vector2Int cell = _currentPiecePosition[i];
                cell.x += translation.x;
                cell.y += translation.y;
                newPosition[i] = new Vector2Int(translation.x, translation.y);
            }

            return newPosition;
        }
        
        public abstract Vector2Int[] GetNewPiecePosition();

    }
}