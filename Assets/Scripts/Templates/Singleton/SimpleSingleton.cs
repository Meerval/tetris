namespace Templates.Singleton
{
    public abstract class SimpleSingleton<T> where T : new()
    {
        private static T _instance;
        public static T Instance => _instance ??= new T();
    }
}