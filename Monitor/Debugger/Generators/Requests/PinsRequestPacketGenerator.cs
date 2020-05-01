using Protocol.Packets;
using Protocol.Packets.Requests;

namespace Monitor.Debugger.Generators.Requests
{
    public class PinsRequestPacketGenerator
    {
        public PacketBase Generate()
        {
            var requestPacket = new PinsRequestPacket();
            requestPacket.RecalculateChecksum();

            return requestPacket;
        }
    }
}
