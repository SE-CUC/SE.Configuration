using Sandbox.ModAPI.Ingame;
using VRage.Game.ModAPI.Ingame.Utilities;
using System;
using System.Collections.Generic;
using Sandbox.Game;

namespace IngameScript
{
    /// <remarks>
    /// Version: 1.0
    /// Author: @AIBaster & Gemini
    /// Repository: https://github.com/SE-CUC/SE.Configuration
    /// </remarks>
    public class ConfigurationManager : IConfigurationManager
    {
        private readonly IConfigStorage _storage;
        private readonly MyIni _iniParser = new MyIni();
        private readonly Dictionary<Type, IConfigSection> _configSections = new Dictionary<Type, IConfigSection>();

        public ConfigurationManager(IConfigStorage storage)
        {
            if (storage == null)
                throw new ArgumentNullException("storage");
            _storage = storage;
        }

        public void Register(IConfigSection configSection)
        {
            if (configSection == null) return;
            _configSections[configSection.GetType()] = configSection;
        }

        public void Load()
        {
            _iniParser.Clear();
            _iniParser.TryParse(_storage.Load());

            foreach (var section in _configSections.Values)
            {
                section.Read(_iniParser);
            }
        }

        public T GetOptions<T>() where T : class, IConfigSection
        {
            IConfigSection configSection;
            if (_configSections.TryGetValue(typeof(T), out configSection))
            {
                return (T)configSection;
            }
            throw new ConfigurationException("Configuration for type '" + typeof(T).Name + "' was requested but not registered.");
        }

        public void SaveDefaults()
        {
            _iniParser.Clear();
            foreach (var section in _configSections.Values)
            {
                section.Write(_iniParser);
            }
            _storage.Save(_iniParser.ToString());
        }
    }
}