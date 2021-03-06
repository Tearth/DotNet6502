﻿using System;
using M6502;
using Protocol.Packets;
using Protocol.Packets.Responses;

namespace Host.Debugger.Handlers.Responses
{
    public class PinsHandler : PacketHandlerBase
    {
        public PinsHandler(M6502Core core) : base(core)
        {
        }

        public override PacketBase Handle(PacketBase packet)
        {
            var pinsPacket = (PinsPacket)packet;

            Core.Pins.A = pinsPacket.AddressBus;
            Core.Pins.D = pinsPacket.DataBus;
            Core.Pins.InterruptRequest = Convert.ToBoolean((pinsPacket.Other >> 0) & 1);
            Core.Pins.NonMaskableInterrupt = Convert.ToBoolean((pinsPacket.Other >> 1) & 1);
            Core.Pins.Ready = Convert.ToBoolean((pinsPacket.Other >> 2) & 1);
            Core.Pins.Reset = Convert.ToBoolean((pinsPacket.Other >> 3) & 1);
            Core.Pins.ReadWrite = Convert.ToBoolean((pinsPacket.Other >> 4) & 1);
            Core.Pins.Sync = Convert.ToBoolean((pinsPacket.Other >> 5) & 1);
            Core.Pins.Vcc = Convert.ToBoolean((pinsPacket.Other >> 6) & 1);

            return null;
        }
    }
}