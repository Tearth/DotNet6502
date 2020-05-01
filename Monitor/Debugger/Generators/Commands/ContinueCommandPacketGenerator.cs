using Protocol.Packets;
using Protocol.Packets.Commands;

namespace Monitor.Debugger.Generators.Commands
{
    public class ContinueCommandPacketGenerator
    {
        public PacketBase Generate()
        {
            var commandPacket = new ContinueCommandPacket();
            commandPacket.RecalculateChecksum();

            return commandPacket;
        }
    }
}