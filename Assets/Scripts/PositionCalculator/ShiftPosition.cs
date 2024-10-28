using System.Linq;
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
            return _currentPiecePosition.Select(cell =>
                {
                    cell += translation;
                    return cell;
                }
            ).ToArray();
        }

        public abstract Vector2Int[] GetNewPiecePosition();
    }
}