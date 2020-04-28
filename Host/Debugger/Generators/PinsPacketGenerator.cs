using CPU;
using Protocol.Packets;
using Protocol.Packets.Responses;

namespace Host.Debugger.Generators
{
    public class PinsPacketGenerator
    {
        private Mos6502Core _core;

        public PinsPacketGenerator(Mos6502Core core)
        {
            _core = core;
        }

        public PacketBase Generate()
        {
            var pinsPacket = new PinsPacket
            {
                AddressBus = _core.Pins.A,
                DataBus = _core.Pins.D,
                Other =
                    (byte)
                    (
                        ((_core.Pins.Irq ? 1 : 0) << 0) |
                        ((_core.Pins.Nmi ? 1 : 0) << 1) |
                        ((_core.Pins.Rdy ? 1 : 0) << 2) |
                        ((_core.Pins.Reset ? 1 : 0) << 3) |
                        ((_core.Pins.Rw ? 1 : 0) << 4) |
                        ((_core.Pins.Sync ? 1 : 0) << 5) |
                        ((_core.Pins.Vcc ? 1 : 0) << 6)
                    )
            };

            pinsPacket.RecalculateChecksum();
            return pinsPacket;
        }
    }
}
