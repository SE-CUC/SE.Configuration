using Sandbox.ModAPI.Ingame;
using System;

namespace IngameScript
{
    public class ProgrammableBlockStorage : IConfigStorage
    {
        private readonly IMyProgrammableBlock _programmableBlock;

        public ProgrammableBlockStorage(IMyProgrammableBlock programmableBlock)
        {
            if (programmableBlock == null)
                throw new ArgumentNullException("programmableBlock");
            _programmableBlock = programmableBlock;
        }

        public string Load() => _programmableBlock.CustomData;
        public void Save(string data) => _programmableBlock.CustomData = data;
    }
}