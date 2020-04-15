namespace Protocol.Packets.Requests
{
    public class MemoryRequestPacket : PacketBase
    {
        public ushort RequestedLength
        {
            get => (ushort)(Data[5] | (Data[6] << 8));
            set { Data[5] = (byte)value; Data[6] = (byte)(value >> 8); }
        }

        public MemoryRequestPacket(ushort requestedLength) : base(2, PacketType.MemoryRequest)
        {
            RequestedLength = requestedLength;
        }

        public MemoryRequestPacket(byte[] data) : base(data)
        {

        }
    }
}