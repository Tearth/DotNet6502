using System;
using CPU;
using Protocol.Packets;
using Protocol.Packets.Responses;

namespace Host.Debugger.Handlers
{
    public class PinsHandler : PacketHandlerBase
    {
        public PinsHandler(Mos6502Core core) : base(core)
        {
        }

        public override PacketBase Handle(PacketBase packet)
        {
            var pinsPacket = (PinsPacket)packet;

            Core.Pins.A     = pinsPacket.AddressBus;
            Core.Pins.D     = pinsPacket.DataBus;
            Core.Pins.Irq   = Convert.ToBoolean((pinsPacket.Other >> 0) & 1);
            Core.Pins.Nmi   = Convert.ToBoolean((pinsPacket.Other >> 1) & 1);
            Core.Pins.Rdy   = Convert.ToBoolean((pinsPacket.Other >> 2) & 1);
            Core.Pins.Reset = Convert.ToBoolean((pinsPacket.Other >> 3) & 1);
            Core.Pins.Rw    = Convert.ToBoolean((pinsPacket.Other >> 4) & 1);
            Core.Pins.Sync  = Convert.ToBoolean((pinsPacket.Other >> 5) & 1);
            Core.Pins.Vcc   = Convert.ToBoolean((pinsPacket.Other >> 6) & 1);

            return null;
        }
    }
}