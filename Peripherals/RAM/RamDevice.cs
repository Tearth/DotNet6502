using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using CPU.IO;

namespace RAM
{
    public class RamDevice : IDevice
    {
        private ushort _startAddress;
        private ushort _endAddress;
        private uint Size => (uint)(_endAddress - _startAddress + 1);

        private byte[] _memory;

        public bool Configure(List<string> parameters)
        {
            if (parameters.Count < 2)
            {
                return false;
            }

            // Parse start address (required)
            if (!ushort.TryParse(parameters[0].Replace("0x", ""), NumberStyles.HexNumber, null, out _startAddress))
            {
                return false;
            }

            // Parse end address (required)
            if (!ushort.TryParse(parameters[1].Replace("0x", ""), NumberStyles.HexNumber, null, out _endAddress))
            {
                return false;
            }

            _memory = new byte[Size];

            // Parse image file (optional)
            if (parameters.Count > 2)
            {
                var imagePath = parameters[2];
                if (!File.Exists(imagePath))
                {
                    return false;
                }

                var atIndex = 0;

                // Parse image start index (optional)
                if (parameters.Count > 3)
                {
                    if (!int.TryParse(parameters[3].Replace("0x", ""), NumberStyles.HexNumber, null, out atIndex))
                    {
                        return false;
                    }
                }

                var imageBytes = File.ReadAllBytes(imagePath);
                Array.Copy(imageBytes, 0, _memory, atIndex, imageBytes.Length);
            }

            return true;
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
