using System;
using System.Collections.Generic;
using System.Text;

namespace IngameScript
{
    public static class ConfigurationExtension
    {
        public static SEApplicationBuilder AddConfiguration(this SEApplicationBuilder builder, IEnumerable<IConfigSection> configs)
        {
            builder.Services.AddSingleton<IConfigurationManager, ConfigurationManager>(p => 
            {                 
                var program = p.GetService<Program>();
                IConfigStorage storage = new ProgrammableBlockStorage(program.Me);
                var configManager = new ConfigurationManager(storage);
                foreach (var config in configs)
                {
                    configManager.Register(config);
                }
                try
                {
                    if(storage.Load() == string.Empty)
                    {
                        program.Echo("No configuration found, saving defaults to Custom Data.");
                        configManager.SaveDefaults();
                    }

                    configManager.Load();
                }
                catch (Exception ex)
                {
                    program.Echo($"Critical error during config load: {ex.Message}." +
                                 $"\n Current config: `{storage.Load()}`");                   
                    throw;
                }

                return configManager;
            });

            return builder;
        }

        public static SEApplicationBuilder AddConfiguration(this SEApplicationBuilder builder, Func<SEApplicationBuilder, SEApplicationBuilder> func)
            => func(builder);
    }
}
