namespace Protocol.Packets.Commands
{
    public class StopCommandPacket : PacketBase
    {
        public StopCommandPacket() : base(0, PacketType.StopCommand)
        {

        }

        public StopCommandPacket(byte[] data) : base(data)
        {

        }
    }
}