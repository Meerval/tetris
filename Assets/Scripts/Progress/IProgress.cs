namespace Progress
{
    public interface IProgress<out T>
    {
        T Value();
    }
}