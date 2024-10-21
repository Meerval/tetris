using UnityEngine;

namespace PositionCalculator
{
    public class RotationPositionCalculatorA : RotationPositionCalculator
    {
        public RotationPositionCalculatorA(Vector2Int[] currentPiecePosition, RotationDirection rotationDirection)
            : base(currentPiecePosition, rotationDirection)
        {
        }

        public override Vector2Int[] GetNewPiecePosition()
        {
            var newPosition = new Vector2Int[CurrentPiecePosition.Length];

            for (var i = 0; i < CurrentPiecePosition.Length; i++)
            {
                Vector2 cell = CurrentPiecePosition[i];
                newPosition[i] = new Vector2Int
                (
                    Mathf.RoundToInt(GetNewCoordinateX(cell, RotationDirection)),
                    Mathf.RoundToInt(GetNewCoordinateY(cell, RotationDirection))
                );
            }

            return newPosition;
        }
    }
}