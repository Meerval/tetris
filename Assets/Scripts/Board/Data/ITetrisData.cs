namespace Board.Data
{
    public interface ITetrisData<out T>
    {
        public T Value();
    }
}