namespace Protocol.Packets.Requests
{
    public class RunToAddressCommandPacket : PacketBase
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

        public RunToAddressCommandPacket() : base(2, PacketType.RunToAddressCommand)
        {

        }

        public RunToAddressCommandPacket(byte[] data) : base(data)
        {

        }
    }
}