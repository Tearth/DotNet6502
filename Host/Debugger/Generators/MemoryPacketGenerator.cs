using CPU;
using Protocol.Packets;
using Protocol.Packets.Requests;

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

            memoryPacket.RecalculateChecksum();
            return memoryPacket;
        }
    }
}