namespace Structure
{
    public abstract class SimpleSingleton<T> : ISingleton where T : ISingleton, new()
    {
        private static T _instance;
        public static T Instance => _instance ??= new T();
    }
}