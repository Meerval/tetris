using System;
using System.Collections.Generic;
using NUnit.Framework;
using PiecePosition.RotationMath;
using UnityEngine;

namespace Editor.PiecePosition.RotationMath
{
    [TestFixture]
    [Category("Unit")]
    public class RotationFunctionTests
    {
        [Test]
        [TestCaseSource(nameof(GetRotatedPositionTestCases))]
        public void Get_Rotated_Position_Test(Vector2 currentPosition, RotationAngle rotationAngle, Vector2 expected)
        {
            Vector2 actual = new RotationFunction(currentPosition, rotationAngle).GetRotatedPosition();
            Assert.That(actual.x, Is.EqualTo(expected.x).Within(0.001f), $"x coordinate is: {actual.x}, but expected is: {expected.x}");
            Assert.That(actual.y, Is.EqualTo(expected.y).Within(0.001f), $"y coordinate is: {actual.y}, but expected is: {expected.y}");
        }

        private static IEnumerable<TestCaseData> GetRotatedPositionTestCases
        {
            get
            {
                Func<Action<RotationAngle>, int, RotationAngle> a = (action, invocationCount) =>
                {
                    RotationAngle rotationAngle = new();
                    for (int j = 0; j < invocationCount; j++)
                    {
                        action.Invoke(rotationAngle);
                    }

                    return rotationAngle;
                };

                yield return new TestCaseData
                    (
                        new Vector2(1f, 1f),
                        new RotationAngle(),
                        new Vector2(1f, 1f)
                    )
                    .SetName("Get position after 0 rotation");

                yield return new TestCaseData
                    (
                        new Vector2(1f, 1f),
                        a.Invoke(r => r.Add(Direction.Left), 1),
                        new Vector2(-1f, 1f)
                    )
                    .SetName("Get position after 1 Left (pi/2) rotation");
                yield return new TestCaseData
                    (
                        new Vector2(1f, 1f),
                        a.Invoke(r => r.Add(Direction.Right), 1),
                        new Vector2(1f, -1f)
                    )
                    .SetName("Get position after 1 Right (-pi/2) rotation");
                yield return new TestCaseData
                    (
                        new Vector2(1f, 1f),
                        a.Invoke(r => r.Add(Direction.Left), 2),
                        new Vector2(-1f, -1f)
                    )
                    .SetName("Get position after 2 Left (pi) rotation");
                yield return new TestCaseData
                    (
                        new Vector2(1f, 1f),
                        a.Invoke(r => r.Add(Direction.Right), 2),
                        new Vector2(-1f, -1f)
                    )
                    .SetName("Get position after 2 Right (-pi) rotation");
                yield return new TestCaseData
                    (
                        new Vector2(1f, 1f),
                        a.Invoke(r => r.Add(Direction.Left), 3),
                        new Vector2(1f, -1f)
                    )
                    .SetName("Get position after 3 Left (3pi/2) rotation");
                yield return new TestCaseData
                    (
                        new Vector2(1f, 1f),
                        a.Invoke(r => r.Add(Direction.Right), 3),
                        new Vector2(-1f, 1f)
                    )
                    .SetName("Get position after 3 Right (-3pi/2) rotation");
                yield return new TestCaseData
                    (
                        new Vector2(1f, 1f),
                        a.Invoke(r => r.Add(Direction.Left), 4),
                        new Vector2(1f, 1f)
                    )
                    .SetName("Get position after 4 Left (0) rotation");
                yield return new TestCaseData
                    (
                        new Vector2(1f, 1f),
                        a.Invoke(r => r.Add(Direction.Right), 4),
                        new Vector2(1f, 1f)
                    )
                    .SetName("Get position after 4 Right (0) rotation");
                yield return new TestCaseData
                    (
                        new Vector2(1f, 1f),
                        a.Invoke(r => r.Add(Direction.Left), 5),
                        new Vector2(-1f, 1f)
                    )
                    .SetName("Get position after 5 Left (pi/2) rotation");
                yield return new TestCaseData
                    (
                        new Vector2(1f, 1f),
                        a.Invoke(r => r.Add(Direction.Right), 5),
                        new Vector2(1f, -1f)
                    )
                    .SetName("Get position after 5 Right (-pi/2) rotation");
            }
        }
    }
}