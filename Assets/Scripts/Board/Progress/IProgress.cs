namespace Board.Progress
{
    public interface IProgress<out T>
    {
        T Value();
    }
}