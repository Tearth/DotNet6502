namespace Protocol.Packets.Requests
{
    public class NextCycleCommandPacket : PacketBase
    {
        public NextCycleCommandPacket() : base(0, PacketType.NextCycleCommand)
        {

        }

        public NextCycleCommandPacket(byte[] data) : base(data)
        {

        }
    }
}