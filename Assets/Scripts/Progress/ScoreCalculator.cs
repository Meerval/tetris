namespace Progress
{
    public class ScoreCalculator : IProgressCalculator<int>
    {
        private readonly int _removedLinesCount;
        private readonly int _scoreCoefficient = 100;

        public ScoreCalculator(int removedLinesCount)
        {
            _removedLinesCount = removedLinesCount;
        }

        public int Calculate()
        {
            return 100 * _removedLinesCount * Bonus();
        }

        private int Bonus()
        {
            int bonus = _removedLinesCount switch
            {
                1 => 1,
                2 => 3,
                3 => 5,
                4 => 10,
                _ => 0
            };
            return bonus;
        }
    }
}