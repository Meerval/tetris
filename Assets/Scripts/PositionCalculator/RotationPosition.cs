using UnityEngine;

namespace PositionCalculator
{
    public abstract class RotationPositionCalculator : IPositionCalculator
    {
        private readonly float _cos = Mathf.Cos(Mathf.PI / 2f);
        private readonly float _sin = Mathf.Sin(Mathf.PI / 2f);
        
        protected readonly Vector2Int[] CurrentPiecePosition;
        protected readonly RotationDirection RotationDirection;

        protected RotationPositionCalculator(Vector2Int[] currentPiecePosition, RotationDirection rotationDirection)
        {
            CurrentPiecePosition = currentPiecePosition;
            RotationDirection = rotationDirection;
        }

        protected float GetNewCoordinateX(Vector2 currentPosition, RotationDirection rotationDirection)
        {
            return (currentPosition.x * _cos + currentPosition.y * _sin) * rotationDirection.Value();
        }

        protected float GetNewCoordinateY(Vector2 currentPosition, RotationDirection rotationDirection)
        {
            return (currentPosition.x * -_sin + currentPosition.y * _cos) * rotationDirection.Value();
        }

        public abstract Vector2Int[] GetNewPiecePosition();

    }
}