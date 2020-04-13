using Protocol.Packets.Requests;

namespace Protocol.Packets
{
    public class PacketsFactory
    {
        public PacketBase Create(byte[] buffer)
        {
            switch ((PacketType)buffer[4])
            {
                case PacketType.RegistersRequest: return new RegistersRequestPacket(buffer);
                case PacketType.Registers: return new RegistersPacket(buffer);
                case PacketType.PinsRequest: return new PinsRequestPacket(buffer);
                case PacketType.Pins: return new PinsPacket(buffer);
            }

            return null;
        }
    }
}
