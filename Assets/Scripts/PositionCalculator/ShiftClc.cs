using Pieces;
using UnityEngine;

namespace PositionCalculator
{
    public abstract class ShiftShapeMap : IPositionClc
    {

        private readonly Vector2Int _currentPiecePosition;

        protected ShiftShapeMap(Vector2Int currentPiecePosition)
        {
            _currentPiecePosition = currentPiecePosition;
        }

        protected Vector2Int GetNewPositionOf(Vector2Int translation)
        {
            return _currentPiecePosition + translation;
        }

        public abstract Vector2Int GetNewPosition();
        
    }
}