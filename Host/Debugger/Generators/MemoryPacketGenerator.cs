using System;
using CPU;
using Protocol.Packets;
using Protocol.Packets.Requests;
using Protocol.Packets.Responses;

namespace Host.Debugger.Generators
{
    public class MemoryPacketGenerator
    {
        private Mos6502Core _core;

        public MemoryPacketGenerator(Mos6502Core core)
        {
            _core = core;
        }

        public PacketBase Generate(ushort address, ushort requestedLength, byte tag)
        {
            var memoryPacket = new MemoryPacket(address, requestedLength, tag);

            checked
            {
                var memoryIndex = 0;
                try
                {
                    for (var currentAddress = address; currentAddress < address + requestedLength; currentAddress++, memoryIndex++)
                    {
                        memoryPacket[memoryIndex] = _core.Bus.ReadDebug(currentAddress);
                    }
                }
                catch (OverflowException)
                {
                    memoryPacket.MemoryLength -= (ushort)(requestedLength - memoryIndex);
                }
            }

            memoryPacket.RecalculateChecksum();
            return memoryPacket;
        }
    }
}