namespace Protocol.Packets.Requests
{
    public class NextCommandPacket : PacketBase
    {
        public NextCommandPacket() : base(0, PacketType.NextCommand)
        {

        }

        public NextCommandPacket(byte[] data) : base(data)
        {

        }
    }
}