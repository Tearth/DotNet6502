using System.Collections.Generic;

namespace CPU.IO
{
    public interface IDevice
    {
        bool Configure(PinsState pins, List<string> parameters);
        void Process();
    }
}
