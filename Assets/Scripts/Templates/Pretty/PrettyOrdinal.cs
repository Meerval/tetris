namespace Templates.Pretty
{
    public class PrettyOrdinal : Pretty
    {
        private readonly int _number;

        public PrettyOrdinal(int number)
        {
            _number = number;
        }

        public override string Prettify()
        {
            return _number + (_number % 10) switch
            {
                1 when _number % 100 != 11 => "st",
                2 when _number % 100 != 12 => "nd",
                3 when _number % 100 != 13 => "rd",
                _ => "th"
            };
        }
    }
}