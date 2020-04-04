using System.Collections.Generic;

namespace CPU.IO
{
    public interface IDevice
    {
        bool Configure(Mos6502Core core, List<string> parameters);
        void Process();
    }
}
