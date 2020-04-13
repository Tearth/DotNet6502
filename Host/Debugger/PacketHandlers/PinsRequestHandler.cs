using CPU;
using Protocol.Packets;
using Protocol.Packets.Requests;

namespace Host.Debugger.PacketHandlers
{
    public class PinsRequestHandler : PacketHandlerBase
    {
        public PinsRequestHandler(Mos6502Core core) : base(core)
        {
        }

        public override byte[] Handle(PacketBase packet)
        {
            var pinsPacket = new PinsPacket
            {
                AddressBus = Core.Pins.A,
                DataBus = Core.Pins.D,
                Other = 
                    (byte)
                    (
                        ((Core.Pins.Irq   ? 1 : 0) << 0) |
                        ((Core.Pins.Nmi   ? 1 : 0) << 1) |
                        ((Core.Pins.Rdy   ? 1 : 0) << 2) |
                        ((Core.Pins.Reset ? 1 : 0) << 3) |
                        ((Core.Pins.Rw    ? 1 : 0) << 4) |
                        ((Core.Pins.Sync  ? 1 : 0) << 5) |
                        ((Core.Pins.Vcc   ? 1 : 0) << 6)
                    )
            };

            pinsPacket.RecalculateChecksum();
            return pinsPacket.Data;
        }
    }
}