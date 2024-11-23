using UnityEngine;

namespace Board.PiecePosition
{
    public abstract class Shift : IPosition
    {

        private readonly Vector2Int _currentPiecePosition;

        protected Shift(Vector2Int currentPiecePosition)
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