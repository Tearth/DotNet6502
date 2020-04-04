using System.Collections.Generic;
using System.Linq;

namespace CPU.IO
{
    public class Bus
    {
        private readonly Mos6502Core _core;
        private readonly List<IDevice> _devices;

        public Bus(Mos6502Core core)
        {
            _core = core;
            _devices = new List<IDevice>();
        }

        public void AttachDevice(IDevice device)
        {
            _devices.Add(device);
        }

        public byte Read(ushort address)
        {
            _core.Pins.Rw = true;
            _core.Pins.A = address;
            _core.Pins.D = _devices.Aggregate((byte)0, (result, p) => (byte)(result | p.Read(address)));
            _core.YieldCycle();
            
            return _core.Pins.D;
        }

        public void Write(ushort address, byte value)
        {
            _core.Pins.Rw = false;
            _core.Pins.A = address;
            _core.Pins.D = value;
            _devices.ForEach(p => p.Write(_core.Pins.A, _core.Pins.D));
            _core.YieldCycle();
        }
    }
}
