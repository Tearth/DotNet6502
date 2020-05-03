using System.Collections.Generic;

namespace M6502.IO
{
    public interface IDevice
    {
        bool Configure(PinsState pins, List<string> parameters);
        void Process();
    }
}
