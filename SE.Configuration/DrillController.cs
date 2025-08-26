namespace IngameScript
{
    public class DrillController
    {
        private readonly DrillingRigConfig _config;
        public DrillController(IConfigurationManager configManager)
        {
            _config = configManager.GetOptions<DrillingRigConfig>();
        }
        public void DoWork()
        {
        }
    }
}
