using Sandbox.ModAPI.Ingame;
using VRage.Game.ModAPI.Ingame.Utilities;

namespace IngameScript
{
    public interface IConfigSection
    {
        void Read(MyIni ini);
        void Write(MyIni ini);
    }
}