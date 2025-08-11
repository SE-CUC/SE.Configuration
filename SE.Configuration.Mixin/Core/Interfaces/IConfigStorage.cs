namespace IngameScript
{
    public interface IConfigStorage
    {
        string Load();
        void Save(string data);
    }
}