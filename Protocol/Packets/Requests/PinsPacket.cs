namespace Protocol.Packets.Requests
{
    public class PinsPacket : PacketBase
    {
        public byte AddressBus
        {
            get => Data[5];
            set => Data[5] = value;
        }

        public ushort DataBus
        {
            get => (ushort)(Data[6] | (Data[7] << 8));
            set { Data[6] = (byte)value; Data[7] = (byte)(value >> 8); }
        }

        public byte Other
        {
            get => Data[8];
            set => Data[8] = value;
        }

        public PinsPacket() : base(4, PacketType.Pins)
        {

        }

        public PinsPacket(byte[] data) : base(data)
        {

        }
    }
}