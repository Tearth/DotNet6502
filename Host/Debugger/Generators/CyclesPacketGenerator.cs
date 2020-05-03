using M6502;
using Protocol.Packets;
using Protocol.Packets.Responses;

namespace Host.Debugger.Generators
{
    public class CyclesPacketGenerator
    {
        private readonly M6502Core _core;

        public CyclesPacketGenerator(M6502Core core)
        {
            _core = core;
        }

        public PacketBase Generate()
        {
            var cyclesPacket = new CyclesPacket
            {
                Cycles = _core.Cycles
            };

            cyclesPacket.RecalculateChecksum();
            return cyclesPacket;
        }
    }
}