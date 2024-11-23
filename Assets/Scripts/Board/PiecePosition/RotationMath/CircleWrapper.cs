namespace Board.PiecePosition.RotationMath
{
    public static class CircleWrapper
    {
        public static int Wrap(int input, int pointCount)
        {
            return input < 0 ? pointCount + input % pointCount : input % pointCount;
        }
    }
}