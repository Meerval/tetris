﻿using Math;
using UnityEngine;

namespace PositionCalculator
{
    public class RotationClcJLSTZ : RotationClc
    {
        public RotationClcJLSTZ(Vector2Int[] shapeMap, RotationAngle angle) : base(shapeMap, angle)
        {
        }

        public override Vector2Int[] GetRotatedShapeMap()
        {
            Vector2Int[] result = new Vector2Int[ShapeMap.Length];
            for (int i = 0; i < ShapeMap.Length; i++)
            {
                Vector2 newPosition = new RotationFunction(ShapeMap[i], Angle).GetRotatedPosition();
                result[i] = new Vector2Int
                (
                    Mathf.RoundToInt(newPosition.x),
                    Mathf.RoundToInt(newPosition.y)
                );
            }

            return result;
        }
    }
}