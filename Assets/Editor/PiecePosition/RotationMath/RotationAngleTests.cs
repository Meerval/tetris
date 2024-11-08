using NUnit.Framework;
using PiecePosition.RotationMath;
using UnityEngine;

namespace Editor.PiecePosition.RotationMath
{
    public class RotationAngleTests
    {
        [Test]
        public void Rotation_Angle_0_Test()
        {
            AssertRadian(new RotationAngle().Radian(), 0f);
        }

        [Test]
        public void Rotation_Angle_Left_Test()
        {
            RotationAngle angle = new();
            angle.Add(Direction.Left);

            AssertRadian(angle.Radian(), Mathf.PI / 2f);
        }

        [Test]
        public void Rotation_Angle_Right_Test()
        {
            RotationAngle angle = new();
            angle.Add(Direction.Right);

            AssertRadian(angle.Radian(), -Mathf.PI / 2f);
        }

        [Test]
        public void Rotation_Angle_2_Left_Test()
        {
            RotationAngle angle = new();
            angle.Add(Direction.Left);
            angle.Add(Direction.Left);

            AssertRadian(angle.Radian(), Mathf.PI);
        }

        [Test]
        public void Rotation_Angle_2_Right_Test()
        {
            RotationAngle angle = new();
            angle.Add(Direction.Right);
            angle.Add(Direction.Right);

            AssertRadian(angle.Radian(), -Mathf.PI);
        }

        [Test]
        public void Rotation_Angle_3_Left_Test()
        {
            RotationAngle angle = new();
            angle.Add(Direction.Left);
            angle.Add(Direction.Left);
            angle.Add(Direction.Left);

            AssertRadian(angle.Radian(), 3f * Mathf.PI / 2f);
        }

        [Test]
        public void Rotation_Angle_3_Right_Test()
        {
            RotationAngle angle = new();
            angle.Add(Direction.Right);
            angle.Add(Direction.Right);
            angle.Add(Direction.Right);

            AssertRadian(angle.Radian(), -3f * Mathf.PI / 2f);
        }

        [Test]
        public void Rotation_Angle_4_Left_Test()
        {
            RotationAngle angle = new();
            angle.Add(Direction.Left);
            angle.Add(Direction.Left);
            angle.Add(Direction.Left);
            angle.Add(Direction.Left);

            AssertRadian(angle.Radian(), 0);
        }

        [Test]
        public void Rotation_Angle_4_Right_Test()
        {
            RotationAngle angle = new();
            angle.Add(Direction.Right);
            angle.Add(Direction.Right);
            angle.Add(Direction.Right);
            angle.Add(Direction.Right);

            AssertRadian(angle.Radian(), 0);
        }

        [Test]
        public void Rotation_Angle_5_Left_Test()
        {
            RotationAngle angle = new();
            angle.Add(Direction.Left);
            angle.Add(Direction.Left);
            angle.Add(Direction.Left);
            angle.Add(Direction.Left);
            angle.Add(Direction.Left);

            AssertRadian(angle.Radian(), Mathf.PI / 2f);
        }

        [Test]
        public void Rotation_Angle_5_Right_Test()
        {
            RotationAngle angle = new();
            angle.Add(Direction.Right);
            angle.Add(Direction.Right);
            angle.Add(Direction.Right);
            angle.Add(Direction.Right);
            angle.Add(Direction.Right);

            AssertRadian(angle.Radian(), -Mathf.PI / 2f);
        }

        private void AssertRadian(float actual, float expected)
        {
            Assert.That(actual, Is.EqualTo(expected).Within(0.0001f));
        }
    }
}