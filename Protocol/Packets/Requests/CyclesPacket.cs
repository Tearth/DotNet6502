namespace Protocol.Packets.Requests
{
    public class CyclesPacket : PacketBase
    {
        public uint Cycles
        {
            get => (uint)(Data[5] | (Data[6] << 8) | (Data[7] << 16) | (Data[8] << 24));
            set
            {
                Data[5] = (byte)value;
                Data[6] = (byte)(value >> 8);
                Data[7] = (byte)(value >> 16);
                Data[8] = (byte)(value >> 24);
            }

        }

        public CyclesPacket() : base(4, PacketType.Cycles)
        {

        }

        public CyclesPacket(byte[] data) : base(data)
        {

        }
    }
}