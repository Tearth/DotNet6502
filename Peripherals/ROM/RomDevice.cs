using CPU.IO;

namespace ROM
{
    public class RomDevice : IDevice
    {
        private readonly ushort _startAddress;
        private readonly ushort _endAddress;
        private ushort Size => (ushort)(_endAddress - _startAddress);

        private readonly byte[] _memory;

        public RomDevice(ushort startAddress, ushort endAddress)
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