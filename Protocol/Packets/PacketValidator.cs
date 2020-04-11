namespace Protocol.Packets
{
    public class PacketValidator
    {
        public (bool Valid, ushort Size) Validate(byte[] buffer)
        {
            var signature = (ushort)(buffer[0] | (buffer[1] << 8));
            if (signature != 0x6502)
            {
                return (false, 0);
            }

            var packetSize = (ushort)(buffer[2] | (buffer[3] << 8));
            return (true, packetSize);
        }
    }
}
