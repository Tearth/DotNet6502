using CPU;
using Protocol.Packets;
using Protocol.Packets.Requests;

namespace Host.Debugger.Generators
{
    public class CyclesPacketGenerator
    {
        private Mos6502Core _core;

        public CyclesPacketGenerator(Mos6502Core core)
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