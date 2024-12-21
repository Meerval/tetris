namespace TetrisData
{
    public interface ITetrisData<out T>
    {
        public T Value();
    }
}