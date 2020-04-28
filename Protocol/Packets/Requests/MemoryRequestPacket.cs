namespace Protocol.Packets.Requests
{
    public class MemoryRequestPacket : PacketBase
    {
        public ushort Address
        {
            get => (ushort)(Data[5] | Data[6] << 8);
            set
            {
                Data[5] = (byte)value;
                Data[6] = (byte)(value >> 8);
            }
        }

        public ushort RequestedLength
        {
            get => (ushort)(Data[7] | Data[8] << 8);
            set
            {
                Data[7] = (byte)value;
                Data[8] = (byte)(value >> 8);
            }
        }

        public byte Tag
        {
            get => Data[9];
            set => Data[9] = value;
        }

        public MemoryRequestPacket(ushort address, ushort requestedLength, byte tag)
            : base(5, PacketType.MemoryRequest)
        {
            Address = address;
            RequestedLength = requestedLength;
            Tag = tag;
        }

        public MemoryRequestPacket(byte[] data) : base(data)
        {

        }
    }
}