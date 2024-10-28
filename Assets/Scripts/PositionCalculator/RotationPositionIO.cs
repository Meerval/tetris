using System.Linq;
using UnityEngine;

namespace PositionCalculator
{
    public class RotationPositionCalculatorIO : RotationPositionCalculator
    {
        public RotationPositionCalculatorIO(Vector2Int[] currentPiecePosition, RotationDirection rotationDirection)
            : base(currentPiecePosition, rotationDirection)
        {
        }

        public override Vector2Int[] GetNewPiecePosition()
        {
            return CurrentPiecePosition.Select(cell =>
                {
                    Vector2 cellF = cell;
                    cellF.x -= 0.5f;
                    cellF.y -= 0.5f;
                    return new Vector2Int(
                        Mathf.CeilToInt(GetNewCoordinateX(cellF, RotationDirection)),
                        Mathf.CeilToInt(GetNewCoordinateY(cellF, RotationDirection))
                    );
                }
            ).ToArray();
        }
    }
}