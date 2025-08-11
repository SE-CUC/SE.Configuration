using Sandbox.ModAPI.Ingame;
using System;
using VRage.Game.ModAPI.Ingame;

namespace IngameScript
{
    public sealed partial class Program : MyGridProgram
    {
        private readonly IConfigurationManager _configManager;
        private readonly DrillController _drillController;

        public Program()
        {
            IConfigStorage storage = new ProgrammableBlockStorage(Me);
            _configManager = new ConfigurationManager(storage);

            _configManager.Register(new DrillingRigConfig());
            _configManager.Register(new PowerManagementConfig());
            
            try
            {
                _configManager.Load();
            }
            catch (Exception ex)
            {
                Echo("Critical error during config load: " + ex.Message);
                return; 
            }

            try
            {
                _drillController = new DrillController(_configManager);
            }
            catch (ConfigurationException ex)
            {
                Echo("Failed to initialize services: " + ex.Message);
            }
        }

        public void Save() {}

        public void Main(string argument, UpdateType updateSource)
        {
            if ((updateSource & (UpdateType.Terminal | UpdateType.Trigger | UpdateType.Mod)) != 0)
            {
                switch (argument.ToLower())
                {
                    case "save_defaults":
                        _configManager.SaveDefaults();
                        Echo("Default configuration saved to Custom Data.");
                        break;
                    default:
                        Echo("Unknown command: '" + argument + "'");
                        break;
                }
            }
        }
    }
}