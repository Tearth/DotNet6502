using Protocol.Packets;
using Protocol.Packets.Commands;

namespace Monitor.Debugger.Generators.Commands
{
    public class NextCycleCommandPacketGenerator
    {
        public PacketBase Generate()
        {
            var commandPacket = new NextCycleCommandPacket();
            commandPacket.RecalculateChecksum();

            return commandPacket;
        }
    }
}