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

            // ForEach method was quite slower than typical for statement
            //_devices.ForEach(p => p.Process());
            for (var i = 0; i < _devices.Count; i++)
            {
                _devices[i].Process();
            }

            _core.YieldCycle();

            return _core.Pins.D;
        }

        public byte ReadDebug(ushort address)
        {
            var oldReadWriteState = _core.Pins.Rw;
            var oldAddressState = _core.Pins.A;
            var oldDataState = _core.Pins.D;

            _core.Pins.Rw = true;
            _core.Pins.A = address;
            _core.Pins.D = 0;

            // ForEach method was quite slower than typical for statement
            //_devices.ForEach(p => p.Process());
            for (var i = 0; i < _devices.Count; i++)
            {
                _devices[i].Process();
            }

            var result = _core.Pins.D;
            _core.Pins.Rw = oldReadWriteState;
            _core.Pins.A = oldAddressState;
            _core.Pins.D = oldDataState;
            
            return result;
        }

        public void Write(ushort address, byte value, bool? forcedRw = null)
        {
            _core.Pins.Rw = forcedRw ?? false;
            _core.Pins.A = address;
            _core.Pins.D = value;
            _core.YieldCycle();

            // ForEach method was quite slower than typical for statement
            //_devices.ForEach(p => p.Process());
            for (var i = 0; i < _devices.Count; i++)
            {
                _devices[i].Process();
            }
        }

        public void WriteDebug(ushort address, byte value)
        {
            var oldReadWriteState = _core.Pins.Rw;
            var oldAddressState = _core.Pins.A;
            var oldDataState = _core.Pins.D;

            _core.Pins.Rw = false;
            _core.Pins.A = address;
            _core.Pins.D = value;

            // ForEach method was quite slower than typical for statement
            //_devices.ForEach(p => p.Process());
            for (var i = 0; i < _devices.Count; i++)
            {
                _devices[i].Process();
            }

            _core.Pins.Rw = oldReadWriteState;
            _core.Pins.A = oldAddressState;
            _core.Pins.D = oldDataState;
        }
    }
}
