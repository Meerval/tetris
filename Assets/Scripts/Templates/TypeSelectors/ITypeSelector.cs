namespace Templates.TypeSelectors
{
    public interface ITypeSelector<out T> where T : class
    {
        public T Select();
    }
}