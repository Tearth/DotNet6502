namespace Protocol.Packets.Requests
{
    public class MemoryPacket : PacketBase
    {
        public ushort MemoryLength
        {
            get => (ushort)(Data[5] | (Data[6] << 8));
            set { Data[5] = (byte)value; Data[6] = (byte)(value >> 8); }
        }
        
        public byte this[int index]
        {
            get => Data[6 + index];
            set => Data[6 + index] = value;
        }

        public MemoryPacket(ushort memoryLength) : base((ushort)(2 + memoryLength), PacketType.Memory)
        {
            MemoryLength = memoryLength;
        }

        public MemoryPacket(byte[] data) : base(data)
        {

        }
    }
}