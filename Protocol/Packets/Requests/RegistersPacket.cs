namespace Protocol.Packets.Requests
{
    public class RegistersPacket : PacketBase
    {
        public ushort ProgramCounter
        {
            get => (ushort)(Data[5] | Data[6] << 8);
            set
            {
                Data[5] = (byte)value;
                Data[6] = (byte)(value >> 8);
            }
        }

        public byte StackPointer
        {
            get => Data[7];
            set => Data[7] = value;
        }

        public byte Accumulator
        {
            get => Data[8];
            set => Data[8] = value;
        }

        public byte XIndex
        {
            get => Data[9];
            set => Data[9] = value;
        }

        public byte YIndex
        {
            get => Data[10];
            set => Data[10] = value;
        }

        public byte Flags
        {
            get => Data[11];
            set => Data[11] = value;
        }

        public RegistersPacket() : base(7, PacketType.Registers)
        {

        }

        public RegistersPacket(byte[] data) : base(data)
        {

        }
    }
}
