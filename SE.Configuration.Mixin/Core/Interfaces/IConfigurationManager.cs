namespace IngameScript
{
    public interface IConfigurationManager
    {
        void Register(IConfigSection configSection);
        void Load();
        T GetOptions<T>() where T : class, IConfigSection;
        void SaveDefaults();
    }
}