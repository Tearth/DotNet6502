using System.Linq;

namespace Protocol.Packets.Requests
{
    public class MemoryPacket : PacketBase
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

        public ushort MemoryLength
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

        public byte[] Memory => Data.Skip(10).Take(MemoryLength).ToArray();

        public byte this[int index]
        {
            get => Data[10 + index];
            set => Data[10 + index] = value;
        }

        public MemoryPacket(ushort address, ushort memoryLength, byte tag) : base((ushort)(5 + memoryLength), PacketType.Memory)
        {
            Address = address;
            MemoryLength = memoryLength;
            Tag = tag;
        }

        public MemoryPacket(byte[] data) : base(data)
        {

        }
    }
}