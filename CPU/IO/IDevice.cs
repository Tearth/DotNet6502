using System.Collections.Generic;

namespace CPU.IO
{
    public interface IDevice
    {
        bool Configure(List<string> parameters);
        byte Read(ushort address);
        void Write(ushort address, byte value);
    }
}
