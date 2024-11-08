using PiecePosition.RotationMath;
using UnityEngine;

namespace PiecePosition
{
    public class RotationIO : Rotation {
        public RotationIO(Vector2Int[] shapeMap, RotationAngle angle) : base(shapeMap, angle)
        {
        }

        public override Vector2Int[] GetRotatedShapeMap()
        {
            Vector2Int[] result = new Vector2Int[ShapeMap.Length];
            for (int i = 0; i < ShapeMap.Length; i++)
            {
                Vector2 cell = ShapeMap[i];
                cell.x -= 0.5f;
                cell.y -= 0.5f;

                Vector2 newPosition = new RotationFunction(cell, Angle).GetRotatedPosition();
                result[i] = new Vector2Int
                (
                    Mathf.CeilToInt(newPosition.x),
                    Mathf.CeilToInt(newPosition.y)
                );
            }

            return result;
        }
    }
}