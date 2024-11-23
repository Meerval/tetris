using System;
using System.Collections.Generic;
using Board.PiecePosition.RotationMath;
using NUnit.Framework;
using UnityEngine;

namespace Editor.PiecePosition.RotationMath
{
    [TestFixture]
    [Category("Unit")]
    public class RotationAngleTests
    {
        [Test]
        [TestCaseSource(nameof(GetAngleInRadianTestCases))]
        public void Get_Angle_In_Radian_Test(float actual, float expected)
        {
            Assert.That(actual, Is.EqualTo(expected).Within(0.0001f));
        }

        private static IEnumerable<TestCaseData> GetAngleInRadianTestCases
        {
            get
            {
                Func<Action<RotationAngle>, int, float> rotationAngle = (action, invocationCount) =>
                {
                    RotationAngle rotationAngle = new();
                    for (int j = 0; j < invocationCount; j++)
                    {
                        action.Invoke(rotationAngle);
                    }

                    return rotationAngle.Radian();
                };

                yield return new TestCaseData(new RotationAngle().Radian(), 0).SetName("Get angle of Default (0) in radian");
                
                yield return new TestCaseData(rotationAngle.Invoke(r => r.Add(Direction.Left), 1), Mathf.PI / 2f)
                    .SetName("Get angle of 1 Left (pi/2) in radian");
                yield return new TestCaseData(rotationAngle.Invoke(r => r.Add(Direction.Right), 1), -Mathf.PI / 2f)
                    .SetName("Get angle of 1 Right (-pi/2) in radian");
                yield return new TestCaseData(rotationAngle.Invoke(r => r.Add(Direction.Left), 2), Mathf.PI)
                    .SetName("Get angle of 2 Left (pi) in radian");
                yield return new TestCaseData(rotationAngle.Invoke(r => r.Add(Direction.Right), 2), -Mathf.PI)
                    .SetName("Get angle of 2 Right (-pi) in radian");
                yield return new TestCaseData(rotationAngle.Invoke(r => r.Add(Direction.Left), 3), 3 * Mathf.PI / 2f)
                    .SetName("Get angle of 3 Left (3pi/2) in radian");
                yield return new TestCaseData(rotationAngle.Invoke(r => r.Add(Direction.Right), 3), 3 * -Mathf.PI / 2f)
                    .SetName("Get angle of 3 Right (-3pi/2) in radian");
                yield return new TestCaseData(rotationAngle.Invoke(r => r.Add(Direction.Left), 4), 0)
                    .SetName("Get angle of 4 Left (0) in radian");
                yield return new TestCaseData(rotationAngle.Invoke(r => r.Add(Direction.Right), 4), 0)
                    .SetName("Get angle of 4 Right (0) in radian");
                yield return new TestCaseData(rotationAngle.Invoke(r => r.Add(Direction.Left), 5), Mathf.PI / 2f)
                    .SetName("Get angle of 5 Left (pi/2) in radian");
                yield return new TestCaseData(rotationAngle.Invoke(r => r.Add(Direction.Right), 5), -Mathf.PI / 2f)
                    .SetName("Get angle of 5 Right (-pi/2) in radian");

                yield return new TestCaseData(rotationAngle.Invoke(r => r.Sub(Direction.Left), 1), -Mathf.PI / 2f)
                    .SetName("Get angle of -1 Left (-pi/2) in radian");
                yield return new TestCaseData(rotationAngle.Invoke(r => r.Sub(Direction.Right), 1), Mathf.PI / 2f)
                    .SetName("Get angle of -1 Right (pi/2) in radian");
                yield return new TestCaseData(rotationAngle.Invoke(r => r.Sub(Direction.Left), 2), -Mathf.PI)
                    .SetName("Get angle of -2 Left (-pi) in radian");
                yield return new TestCaseData(rotationAngle.Invoke(r => r.Sub(Direction.Right), 2), Mathf.PI)
                    .SetName("Get angle of -2 Right (pi) in radian");
                yield return new TestCaseData(rotationAngle.Invoke(r => r.Sub(Direction.Left), 3), 3 * -Mathf.PI / 2f)
                    .SetName("Get angle of -3 Left (-3pi/2) in radian");
                yield return new TestCaseData(rotationAngle.Invoke(r => r.Sub(Direction.Right), 3), 3 * Mathf.PI / 2f)
                    .SetName("Get angle of -3 Right (3pi/2) in radian");
                yield return new TestCaseData(rotationAngle.Invoke(r => r.Sub(Direction.Left), 4), 0)
                    .SetName("Get angle of -4 Left (0) in radian");
                yield return new TestCaseData(rotationAngle.Invoke(r => r.Sub(Direction.Right), 4), 0)
                    .SetName("Get angle of -4 Right (0) in radian");
                yield return new TestCaseData(rotationAngle.Invoke(r => r.Sub(Direction.Left), 5), -Mathf.PI / 2f)
                    .SetName("Get angle of -5 Left (-pi/2) in radian");
                yield return new TestCaseData(rotationAngle.Invoke(r => r.Sub(Direction.Right), 5), Mathf.PI / 2f)
                    .SetName("Get angle of -5 Right (pi/2) in radian");
            }
        }

    }
}