using CPU.IO;

namespace RAM
{
    public class RamDevice : IDevice
    {
        private readonly ushort _startAddress;
        private readonly ushort _endAddress;
        private ushort Size => (ushort)(_endAddress - _startAddress);

        private readonly byte[] _memory;

        public RamDevice(ushort startAddress, ushort endAddress)
        {
            _startAddress = startAddress;
            _endAddress = endAddress;
            _memory = new byte[Size];
        }

        public byte Read(ushort address)
        {
            if (IsAddressValid(address))
            {
                var relativeAddress = GetRelativeAddress(address);
                return _memory[relativeAddress];
            }

            return 0;
        }

        public void Write(ushort address, byte value)
        {
            if (IsAddressValid(address))
            {
                var relativeAddress = GetRelativeAddress(address);
                _memory[relativeAddress] = value;
            }
        }

        private bool IsAddressValid(ushort address)
        {
            return address >= _startAddress && address <= _endAddress;
        }

        private ushort GetRelativeAddress(ushort address)
        {
            return (ushort)(address - _startAddress);
        }
    }
}
