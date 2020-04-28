namespace Protocol.Packets.Commands
{
    public class ContinueCommandPacket : PacketBase
    {
        public ContinueCommandPacket() : base(0, PacketType.ContinueCommand)
        {

        }

        public ContinueCommandPacket(byte[] data) : base(data)
        {

        }
    }
}