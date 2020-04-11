namespace Protocol.Packets.Requests
{
    public class RegistersRequestPacket : PacketBase
    {
        public RegistersRequestPacket() : base(0, PacketType.RegistersRequest)
        {

        }

        public RegistersRequestPacket(byte[] data) : base(data)
        {

        }
    }
}
