﻿namespace Protocol.Packets.Commands
{
    public class NextInstructionCommandPacket : PacketBase
    {
        public NextInstructionCommandPacket() : base(0, PacketType.NextInstructionCommand)
        {

        }

        public NextInstructionCommandPacket(byte[] data) : base(data)
        {

        }
    }
}