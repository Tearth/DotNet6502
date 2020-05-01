using Protocol.Packets;
using Protocol.Packets.Commands;

namespace Monitor.Debugger.Generators.Commands
{
    public class RunUntilLoopCommandPacketGenerator
    {
        public PacketBase Generate()
        {
            var commandPacket = new RunUntilLoopCommandPacket();
            commandPacket.RecalculateChecksum();

            return commandPacket;
        }
    }
}