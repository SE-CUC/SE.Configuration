using Sandbox.ModAPI.Ingame;
using VRage.Game.ModAPI.Ingame.Utilities;

namespace IngameScript
{
    public class PowerManagementConfig : IConfigSection
    {
        public string BatteryTag { get; set; } = "[Main]";
        public float RechargeThreshold { get; set; } = 0.3f;
        public float DischargeThreshold { get; set; } = 0.8f;

        private const string SECTION = "PowerManagementConfig";

        public void Read(MyIni ini)
        {
            BatteryTag = ini.Get(SECTION, "BatteryTag").ToString(BatteryTag);
            RechargeThreshold = ini.Get(SECTION, "RechargeThreshold").ToSingle(RechargeThreshold);
            DischargeThreshold = ini.Get(SECTION, "DischargeThreshold").ToSingle(DischargeThreshold);
        }

        public void Write(MyIni ini)
        {
            ini.Set(SECTION, "BatteryTag", BatteryTag);
            ini.Set(SECTION, "RechargeThreshold", RechargeThreshold);
            ini.Set(SECTION, "DischargeThreshold", DischargeThreshold);
        }
    }
}