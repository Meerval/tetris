using NUnit.Framework;
using PiecePosition;
using PiecePosition.RotationMath;
using UnityEngine;

namespace Editor.PiecePosition.RotationMath
{
    public class RotationFunctionTests
    {
        private readonly Vector2 _position = new(1f, 1f);

        [Test]
        public void Rotation_Function_0_Test()
        {
            AssertRotated(new RotationAngle(), _position);
        }

        [Test]
        public void Rotation_Function_Left_Test()
        {
            RotationAngle angle = new();
            angle.Add(Direction.Left);

            AssertRotated(angle, new Vector2(-1f, 1f));
        }

        [Test]
        public void Rotation_Function_Right_Test()
        {
            RotationAngle angle = new();
            angle.Add(Direction.Right);

            AssertRotated(angle, new Vector2(1f, -1f));
        }

        [Test]
        public void Rotation_Function_2_Left_Test()
        {
            RotationAngle angle = new();
            angle.Add(Direction.Left);
            angle.Add(Direction.Left);

            AssertRotated(angle, new Vector2(-1f, -1f));
        }

        [Test]
        public void Rotation_Function_2_Right_Test()
        {
            RotationAngle angle = new();
            angle.Add(Direction.Right);
            angle.Add(Direction.Right);

            AssertRotated(angle, new Vector2(-1f, -1f));
        }

        [Test]
        public void Rotation_Function_3_Left_Test()
        {
            RotationAngle angle = new();
            angle.Add(Direction.Left);
            angle.Add(Direction.Left);
            angle.Add(Direction.Left);

            AssertRotated(angle, new Vector2(1f, -1f));
        }

        [Test]
        public void Rotation_Function_3_Right_Test()
        {
            RotationAngle angle = new();
            angle.Add(Direction.Right);
            angle.Add(Direction.Right);
            angle.Add(Direction.Right);

            AssertRotated(angle, new Vector2(-1f, 1f));
        }

        [Test]
        public void Rotation_Function_4_Left_Test()
        {
            RotationAngle angle = new();
            angle.Add(Direction.Left);
            angle.Add(Direction.Left);
            angle.Add(Direction.Left);
            angle.Add(Direction.Left);

            AssertRotated(angle, _position);
        }

        [Test]
        public void Rotation_Function_4_Right_Test()
        {
            RotationAngle angle = new();
            angle.Add(Direction.Right);
            angle.Add(Direction.Right);
            angle.Add(Direction.Right);
            angle.Add(Direction.Right);

            AssertRotated(angle, _position);
        }

        [Test]
        public void Rotation_Function_5_Left_Test()
        {
            RotationAngle angle = new();
            angle.Add(Direction.Left);
            angle.Add(Direction.Left);
            angle.Add(Direction.Left);
            angle.Add(Direction.Left);
            angle.Add(Direction.Left);

            AssertRotated(angle, new Vector2(-1f, 1f));
        }

        [Test]
        public void Rotation_Function_5_Right_Test()
        {
            RotationAngle angle = new();
            angle.Add(Direction.Right);
            angle.Add(Direction.Right);
            angle.Add(Direction.Right);
            angle.Add(Direction.Right);
            angle.Add(Direction.Right);

            AssertRotated(angle, new Vector2(1f, -1f));
        }

        private void AssertRotated(RotationAngle angle, Vector2 expected)
        {
            Vector2 actual = new RotationFunction(_position, angle).GetRotatedPosition();
            
            Assert.That(actual.x, Is.EqualTo(expected.x).Within(0.0001f));
            Assert.That(actual.y, Is.EqualTo(expected.y).Within(0.0001f));
        }
    }
}