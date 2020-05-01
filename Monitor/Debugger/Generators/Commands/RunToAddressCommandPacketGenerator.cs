using Protocol.Packets;
using Protocol.Packets.Commands;

namespace Monitor.Debugger.Generators.Commands
{
    public class RunToAddressCommandPacketGenerator
    {
        public PacketBase Generate(ushort address)
        {
            var commandPacket = new RunToAddressCommandPacket
            {
                Address = address
            };
            commandPacket.RecalculateChecksum();

            return commandPacket;
        }
    }
}