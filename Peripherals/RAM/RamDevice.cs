using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using CPU.IO;

namespace RAM
{
    public class RamDevice : IDevice
    {
        private PinsState _pinsState;
        private ushort _startAddress;
        private ushort _endAddress;
        private uint Size => (uint)(_endAddress - _startAddress + 1);

        private byte[] _memory;

        public bool Configure(PinsState pinsState, List<string> parameters)
        {
            _pinsState = pinsState;

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
            if (_pinsState.ReadWrite)
            {
                Read();
            }
            else
            {
                Write();
            }
        }

        private void Read()
        {
            if (IsAddressValid(_pinsState.A))
            {
                var relativeAddress = GetRelativeAddress(_pinsState.A);
                _pinsState.D |= _memory[relativeAddress];
            }
        }

        private void Write()
        {
            if (IsAddressValid(_pinsState.A))
            {
                var relativeAddress = GetRelativeAddress(_pinsState.A);
                _memory[relativeAddress] = _pinsState.D;
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
