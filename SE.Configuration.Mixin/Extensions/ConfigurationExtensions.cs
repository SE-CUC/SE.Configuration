using System;
using System.Collections.Generic;

namespace IngameScript
{
    public static class ConfigurationExtensions
    {
        public static SEApplicationBuilder AddConfiguration(this SEApplicationBuilder builder, Action<IConfigurationManager> configureSections)
        {
            builder.Services.AddSingleton<IConfigStorage>(sp => new ProgrammableBlockStorage(sp.GetService<Program>().Me));
            builder.Services.AddSingleton<IConfigurationManager>(sp => new ConfigurationManager(sp.GetService<IConfigStorage>()));

            var tempProvider = ((ServiceCollection)builder.Services).BuildServiceProvider();
            var configManager = tempProvider.GetService<IConfigurationManager>();
            var program = tempProvider.GetService<Program>();

            if (configureSections != null)
            {
                configureSections(configManager);
            }

            try
            {
                configManager.Load();
            }
            catch (Exception ex)
            {
                program.Echo($"CRITICAL: Configuration load failed: {ex.Message}");           
            }
            finally
            {
                if (string.IsNullOrEmpty(tempProvider.GetService<Program>().Me.CustomData))
                {
                    program.Echo($"CRITICAL: Configuration is empty. Saving and loading default configuration");

                    configManager.SaveDefaults();
                }
            }

            return builder;
        }
    }

    public static class ConfigurationCommandsExtensions
    {
        public static SEApplicationBuilder AddConfigurationCommands(this SEApplicationBuilder builder)
        {
            var provider = ((ServiceCollection)builder.Services).BuildServiceProvider();
            
            var commands = provider.GetService<ICommandService>();
            var configuration = provider.GetService<IConfigurationManager>();
            var storage = provider.GetService<IConfigStorage>();

            commands.RegisterModule(new ConfigurationCommands(configuration, storage));

            return builder;
        }
    }

    public class ConfigurationCommands : ICommandModule
    {
        private readonly IConfigurationManager _configurationManager;

        public ConfigurationCommands(IConfigurationManager manager, IConfigStorage storage)
        {
            if(manager == null)
            {
                throw new ArgumentNullException(nameof(manager), "Configuration manager cannot be null.");
            }

            _configurationManager = manager;
        }

        public IEnumerable<ICommand> GetCommands()
        {
            return new[] {
                new Command("config:force_set", "Set configuration from manager as default.", (args) =>
                {
                    _configurationManager.SaveDefaults();
                }),             
            };
        }
    }
}