using Protocol.Packets;
using Protocol.Packets.Commands;

namespace Monitor.Debugger.Generators.Commands
{
    public class StopCommandPacketGenerator
    {
        public PacketBase Generate()
        {
            var commandPacket = new StopCommandPacket();
            commandPacket.RecalculateChecksum();

            return commandPacket;
        }
    }
}