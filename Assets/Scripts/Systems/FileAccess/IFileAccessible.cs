using Systems.Storage.POCO;

namespace Systems.FileAccess
{
    public interface IFileAccessible
    {
        public void Create();
        public void Delete();
        public void Overwrite(SaveData data);
        public SaveData Read();
    }
}