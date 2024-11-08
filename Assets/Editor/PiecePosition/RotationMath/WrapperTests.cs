using NUnit.Framework;
using PiecePosition.RotationMath;

namespace Editor.PiecePosition.RotationMath
{
    public class WrapperTests
    {

        [Test]
        public void Wrap_0_Test()
        {
            AssertWrapped(0, expectedResult: 0);
        }
    
        [Test]
        public void Wrap_4_Test()
        {
            AssertWrapped(4, expectedResult: 0);
        }
    
        [Test]
        public void Wrap_3_Test()
        {
            AssertWrapped(3, expectedResult: 3);
        }
    
        [Test]
        public void Wrap_m1_Test()
        {
            AssertWrapped(-1, expectedResult: 3);
        }
    
        [Test]
        public void Wrap_5_Test()
        {
            AssertWrapped(5, expectedResult: 1);
        }

    
        [Test]
        public void Wrap_m10_Test()
        {
            AssertWrapped(-10, expectedResult: 2);
        }
    
        [Test]
        public void Wrap_10_Test()
        {
            AssertWrapped(10, expectedResult: 2);
        }

        private static void AssertWrapped(int input, int expectedResult)
        {
            Assert.That(Equals(Wrapper.Wrap(input, 0, 4), expectedResult));
        }
    }
}
