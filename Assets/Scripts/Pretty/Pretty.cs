namespace Pretty
{
    public abstract class Pretty : IPretty
    {
        public abstract string Prettify();
        
        public override string ToString()
        {
            return Prettify();
        }
    }
}