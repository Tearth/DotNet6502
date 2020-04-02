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
            var relativeAddress = GetRelativeAddress(address);
            if (relativeAddress >= Size)
            {
                return 0;
            }

            return _memory[relativeAddress];
        }

        public void Write(ushort address, byte value)
        {
            var relativeAddress = GetRelativeAddress(address);
            if (relativeAddress < Size)
            {
                _memory[relativeAddress] = value;
            }
        }

        private ushort GetRelativeAddress(ushort address)
        {
            return (ushort)(Size - address);
        }
    }
}
