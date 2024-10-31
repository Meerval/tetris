using UnityEngine;

namespace PositionCalculator
{
    public abstract class RotationClc : IShapeMapClc
    {
        protected readonly Vector2Int[] ShapeMap;
        protected readonly RotationAngle Angle;

        protected RotationClc(Vector2Int[] shapeMap, RotationAngle angle)
        {
            ShapeMap = shapeMap;
            Angle = angle;
        }

        public abstract Vector2Int[] GetRotatedShapeMap();
    }
}