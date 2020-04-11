namespace Protocol.Packets.Requests
{
    public class RegistersResponsePacket : PacketBase
    {
        public ushort ProgramCounter
        {
            get => (ushort)(Data[6] | (Data[7] << 8));
            set { Data[6] = (byte)value; Data[7] = (byte)(value >> 8); }
        }

        public byte StackPointer
        {
            get => Data[8];
            set => Data[8] = value;
        }

        public byte Accumulator
        {
            get => Data[9];
            set => Data[9] = value;
        }

        public byte XIndex
        {
            get => Data[10];
            set => Data[10] = value;
        }

        public byte YIndex
        {
            get => Data[11];
            set => Data[11] = value;
        }

        public byte Flags
        {
            get => Data[12];
            set => Data[12] = value;
        }

        public RegistersResponsePacket() : base(7, PacketType.RegistersResponse)
        {

        }

        public RegistersResponsePacket(byte[] data) : base(data)
        {

        }
    }
}
