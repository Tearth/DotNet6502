using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using CPU;
using CPU.IO;

namespace ROM
{
    public class RomDevice : IDevice
    {
        private Mos6502Core _core;
        private ushort _startAddress;
        private ushort _endAddress;
        private uint Size => (uint)(_endAddress - _startAddress + 1);

        private byte[] _memory;

        public bool Configure(Mos6502Core core, List<string> parameters)
        {
            _core = core;

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

        public void Process()
        {
            if (_core.Pins.Rw)
            {
                Read();
            }
        }

        private void Read()
        {
            if (IsAddressValid(_core.Pins.A))
            {
                var relativeAddress = GetRelativeAddress(_core.Pins.A);
                _core.Pins.D |= _memory[relativeAddress];
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