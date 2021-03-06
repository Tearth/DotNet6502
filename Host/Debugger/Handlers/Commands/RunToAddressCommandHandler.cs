﻿using M6502;
using Protocol.Packets;
using Protocol.Packets.Commands;

namespace Host.Debugger.Handlers.Commands
{
    public class RunToAddressCommandHandler : PacketHandlerBase
    {
        public RunToAddressCommandHandler(M6502Core core) : base(core)
        {

        }

        public override PacketBase Handle(PacketBase packet)
        {
            var runToAddressCommandPacket = (RunToAddressCommandPacket)packet;

            Core.Pins.Ready = true;
            while (Core.Registers.ProgramCounter != runToAddressCommandPacket.Address) ;
            Core.Pins.Ready = false;

            return null;
        }
    }
}