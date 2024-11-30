namespace Board.Meta
{
    public interface IProgress<out T>
    {
        public T Value();
    }
}