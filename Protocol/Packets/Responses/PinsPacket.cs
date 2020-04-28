namespace Protocol.Packets.Responses
{
    public class PinsPacket : PacketBase
    {
        public ushort AddressBus
        {
            get => (ushort)(Data[5] | Data[6] << 8);
            set
            {
                Data[5] = (byte)value;
                Data[6] = (byte)(value >> 8);
            }
        }

        public byte DataBus
        {
            get => Data[7];
            set => Data[7] = value;
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