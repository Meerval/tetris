using Templates.POCO;

namespace Systems.FileAccess
{
    public interface IFileAccessible<T> where T : Poco
    {
        public void Create();
        public void Delete();
        public void Overwrite(T data);
        public T Read();
    }
}