namespace Protocol.Packets
{
    public abstract class PacketBase
    {
        public ushort Signature
        {
            get => (ushort) (Data[0] | Data[1] << 8);
            set
            {
                Data[0] = (byte)value;
                Data[1] = (byte)(value >> 8);
            }
        }

        public ushort Length
        {
            get => (ushort)(Data[2] | Data[3] << 8);
            set
            {
                Data[2] = (byte)value;
                Data[3] = (byte)(value >> 8);
            }
        }

        public PacketType Type
        {
            get => (PacketType)Data[4];
            set => Data[4] = (byte)value;
        }

        public byte Checksum
        {
            get => Data[Data.Length - 1];
            set => Data[Data.Length - 1] = value;
        }

        public byte[] Data { get; }

        protected PacketBase(ushort payloadLength, PacketType type)
        {
            Data = new byte[6 + payloadLength];

            Signature = 0x6502;
            Length = (ushort)Data.Length;
            Type = type;
        }

        protected PacketBase(byte[] data)
        {
            Data = data;
        }

        public void RecalculateChecksum()
        {
            Checksum = CalculateChecksum();
        }

        public bool IsChecksumValid()
        {
            return CalculateChecksum() == Checksum;
        }

        private byte CalculateChecksum()
        {
            byte checksum = 0;

            // Data.Length - 1 because XOR of checksum with its own copy will give 0
            for (var i = 0; i < Data.Length - 1; i++)
            {
                checksum ^= Data[i];
            }

            return checksum;
        }
    }
}
