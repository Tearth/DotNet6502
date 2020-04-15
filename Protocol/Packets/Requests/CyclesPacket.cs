namespace Protocol.Packets.Requests
{
    public class CyclesPacket : PacketBase
    {
        public ulong Cycles
        {
            get => 
                (ulong)
                (
                    (Data[5] << 0) | 
                    (Data[6] << 8) | 
                    (Data[7] << 16) | 
                    (Data[8] << 24) |
                    (Data[9] << 32) |
                    (Data[10] << 40) |
                    (Data[11] << 48) |
                    (Data[12] << 56)
                );
            set
            {
                Data[5] = (byte)value;
                Data[6] = (byte)(value >> 8);
                Data[7] = (byte)(value >> 16);
                Data[8] = (byte)(value >> 24);
                Data[9] = (byte)(value >> 32);
                Data[10] = (byte)(value >> 40);
                Data[11] = (byte)(value >> 48);
                Data[12] = (byte)(value >> 56);
            }

        }

        public CyclesPacket() : base(8, PacketType.Cycles)
        {

        }

        public CyclesPacket(byte[] data) : base(data)
        {

        }
    }
}