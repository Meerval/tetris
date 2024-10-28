using System.Linq;
using UnityEngine;

namespace PositionCalculator
{
    public class RotationPositionCalculatorJLSTZ : RotationPositionCalculator
    {
        public RotationPositionCalculatorJLSTZ(Vector2Int[] currentPiecePosition, RotationDirection rotationDirection)
            : base(currentPiecePosition, rotationDirection)
        {
        }

        public override Vector2Int[] GetNewPiecePosition()
        {
            return  CurrentPiecePosition.Select(cell => new Vector2Int
            (
                Mathf.RoundToInt(GetNewCoordinateX(cell, RotationDirection)),
                Mathf.RoundToInt(GetNewCoordinateY(cell, RotationDirection))
            )).ToArray();
        }
    }
}