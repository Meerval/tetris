namespace Board.Meta
{
    public interface IProgress<out T>
    {
        T Value();
    }
}