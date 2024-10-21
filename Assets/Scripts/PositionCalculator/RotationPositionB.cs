using UnityEngine;

namespace PositionCalculator
{
    public class RotationPositionCalculatorB : RotationPositionCalculator
    {
        public RotationPositionCalculatorB(Vector2Int[] currentPiecePosition, RotationDirection rotationDirection)
            : base(currentPiecePosition, rotationDirection)
        {
        }

        public override Vector2Int[] GetNewPiecePosition()
        {
            var newPosition = new Vector2Int[CurrentPiecePosition.Length];

            for (var i = 0; i < CurrentPiecePosition.Length; i++)
            {
                Vector2 cell = CurrentPiecePosition[i];
                cell.x -= 0.5f;
                cell.y -= 0.5f;
                newPosition[i] = new Vector2Int(
                    Mathf.CeilToInt(GetNewCoordinateX(cell, RotationDirection)),
                    Mathf.CeilToInt(GetNewCoordinateY(cell, RotationDirection))
                );
            }
            
            return newPosition;
        }
    }
}