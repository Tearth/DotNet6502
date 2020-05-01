using Protocol.Packets;
using Protocol.Packets.Requests;

namespace Monitor.Debugger.Generators.Requests
{
    public class CyclesRequestPacketGenerator
    {
        public PacketBase Generate()
        {
            var requestPacket = new CyclesRequestPacket();
            requestPacket.RecalculateChecksum();

            return requestPacket;
        }
    }
}