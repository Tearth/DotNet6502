using CPU;
using Protocol.Packets;
using Protocol.Packets.Responses;

namespace Host.Debugger.Generators
{
    public class PinsPacketGenerator
    {
        private readonly Mos6502Core _core;

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
                        ((_core.Pins.InterruptRequest ? 1 : 0) << 0) |
                        ((_core.Pins.NonMaskableInterrupt ? 1 : 0) << 1) |
                        ((_core.Pins.Ready ? 1 : 0) << 2) |
                        ((_core.Pins.Reset ? 1 : 0) << 3) |
                        ((_core.Pins.ReadWrite ? 1 : 0) << 4) |
                        ((_core.Pins.Sync ? 1 : 0) << 5) |
                        ((_core.Pins.Vcc ? 1 : 0) << 6)
                    )
            };

            pinsPacket.RecalculateChecksum();
            return pinsPacket;
        }
    }
}
