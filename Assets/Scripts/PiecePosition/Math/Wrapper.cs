namespace Position.Math
{
    public static class Wrapper
    {
        public static int Wrap(int input, int min, int max)
        {
            if (input < min) return max - (min - input) % (max - min);
            return min + (input - min) % (max - min);
        }
    }
}