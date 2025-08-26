using Sandbox.ModAPI.Ingame;
using VRage.Game.ModAPI.Ingame.Utilities;

namespace IngameScript
{
    public class DrillingRigConfig : IConfigSection
    {
        public string GroupName { get; set; } = "Drills";
        public float PistonVelocity { get; set; } = 0.5f;
        public double StoneEjectionRatio { get; set; } = 0.8;

        private const string SECTION = "DrillingRigConfig";

        public void Read(MyIni ini)
        {
            GroupName = ini.Get(SECTION, "GroupName").ToString(GroupName);
            PistonVelocity = ini.Get(SECTION, "PistonVelocity").ToSingle(PistonVelocity);
            StoneEjectionRatio = ini.Get(SECTION, "StoneEjectionRatio").ToDouble(StoneEjectionRatio);
        }

        public void Write(MyIni ini)
        {
            ini.Set(SECTION, "GroupName", GroupName);
            ini.Set(SECTION, "PistonVelocity", PistonVelocity);
            ini.Set(SECTION, "StoneEjectionRatio", StoneEjectionRatio);
        }
    }
}