namespace Protocol.Packets.Requests
{
    public class NextCommandPacket : PacketBase
    {
        public NextCommandPacket() : base(0, PacketType.NextCycleCommand)
        {

        }

        public NextCommandPacket(byte[] data) : base(data)
        {

        }
    }
}