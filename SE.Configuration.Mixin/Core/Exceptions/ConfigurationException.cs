using System;

namespace IngameScript
{
    public class ConfigurationException : Exception
    {
        public ConfigurationException(string message) : base(message) { }
    }
}