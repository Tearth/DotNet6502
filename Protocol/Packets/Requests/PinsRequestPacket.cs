namespace Protocol.Packets.Requests
{
    public class PinsRequestPacket : PacketBase
    {
        public PinsRequestPacket() : base(0, PacketType.PinsRequest)
        {

        }

        public PinsRequestPacket(byte[] data) : base(data)
        {

        }
    }
}