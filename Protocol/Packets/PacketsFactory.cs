﻿using Protocol.Packets.Requests;

namespace Protocol.Packets
{
    public class PacketsFactory
    {
        public PacketBase Create(byte[] buffer)
        {
            switch ((PacketType)buffer[4])
            {
                case PacketType.RegistersRequest: return new RegistersRequestPacket(buffer);
                case PacketType.RegistersResponse: return new RegistersResponsePacket(buffer);
            }

            return null;
        }
    }
}
