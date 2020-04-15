namespace Protocol.Packets.Requests
{
    public class CyclesRequestPacket : PacketBase
    {
        public CyclesRequestPacket() : base(0, PacketType.CyclesRequest)
        {

        }

        public CyclesRequestPacket(byte[] data) : base(data)
        {

        }
    }
}