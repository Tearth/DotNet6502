using System.Collections.Generic;
using System.Linq;

namespace CPU.IO
{
    public class Bus
    {
        private readonly List<IDevice> _devices;

        public Bus()
        {
            _devices = new List<IDevice>();
        }

        public void AttachDevice(IDevice device)
        {
            _devices.Add(device);
        }

        public byte Read(ushort address)
        {
            return _devices.Aggregate((byte)0, (result, p) => (byte)(result | p.Read(address)));
        }

        public ushort ReadTwo(ushort address)
        {
            return (ushort)(Read(address) | (Read((ushort)(address + 1)) << 8));
        }

        public void Write(ushort address, byte value)
        {
            _devices.ForEach(p => p.Write(address, value));
        }
    }
}
