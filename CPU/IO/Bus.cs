using System.Collections.Generic;

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

        public byte Read(ushort address, bool? forcedRw = null)
        {
            _core.Pins.Rw = forcedRw ?? true;
            _core.Pins.A = address;
            _core.Pins.D = 0;
            _devices.ForEach(p => p.Process());
            _core.YieldCycle();
            
            return _core.Pins.D;
        }

        public void Write(ushort address, byte value, bool? forcedRw = null)
        {
            _core.Pins.Rw = forcedRw ?? false;
            _core.Pins.A = address;
            _core.Pins.D = value;
            _core.YieldCycle();
            _devices.ForEach(p => p.Process());
        }
    }
}
