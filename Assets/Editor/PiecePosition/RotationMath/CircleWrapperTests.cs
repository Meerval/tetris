using System.Collections.Generic;
using NUnit.Framework;
using PiecePosition.RotationMath;

namespace Editor.PiecePosition.RotationMath
{
    [TestFixture]
    [Category("Unit")]
    public class CircleWrapperTests
    {
        [Test]
        [TestCaseSource(nameof(WrapTestCases))]
        public void Wrap_Test(int input, int expectedResult)
        {
            Assert.That(CircleWrapper.Wrap(input, 4), Is.EqualTo(expectedResult));
        }

        private static IEnumerable<TestCaseData> WrapTestCases
        {
            get
            {
                yield return new TestCaseData(0, 0).SetName("Wrap 0");
                yield return new TestCaseData(4, 0).SetName("Wrap 4");
                yield return new TestCaseData(3, 3).SetName("Wrap 3");
                yield return new TestCaseData(-1, 3).SetName("Wrap -1");
                yield return new TestCaseData(5, 1).SetName("Wrap 5");
                yield return new TestCaseData(-10, 2).SetName("Wrap -10");
                yield return new TestCaseData(10, 2).SetName("Wrap 10");
            }
        }
    }
}