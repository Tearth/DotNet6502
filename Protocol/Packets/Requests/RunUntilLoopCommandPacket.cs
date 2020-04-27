namespace Protocol.Packets.Requests
{
    public class RunUntilLoopCommandPacket : PacketBase
    {
        public RunUntilLoopCommandPacket() : base(0, PacketType.RunUntilLoopCommand)
        {

        }

        public RunUntilLoopCommandPacket(byte[] data) : base(data)
        {

        }
    }
}