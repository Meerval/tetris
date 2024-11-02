namespace Progress
{
    public interface IProgressCalculator<out T>
    {
        T Calculate();
    }
}