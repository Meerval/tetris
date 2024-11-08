using PiecePosition.RotationMath;
using UnityEngine;

namespace PiecePosition
{
    public abstract class Rotation : IShapeMap
    {
        protected readonly Vector2Int[] ShapeMap;
        protected readonly RotationAngle Angle;

        protected Rotation(Vector2Int[] shapeMap, RotationAngle angle)
        {
            ShapeMap = shapeMap;
            Angle = angle;
        }

        public abstract Vector2Int[] GetRotatedShapeMap();
    }
}