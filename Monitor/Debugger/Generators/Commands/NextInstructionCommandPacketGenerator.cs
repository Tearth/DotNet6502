using Protocol.Packets;
using Protocol.Packets.Commands;

namespace Monitor.Debugger.Generators.Commands
{
    public class NextInstructionCommandPacketGenerator
    {
        public PacketBase Generate()
        {
            var commandPacket = new NextInstructionCommandPacket();
            commandPacket.RecalculateChecksum();

            return commandPacket;
        }
    }
}