using Protocol.Packets;
using Protocol.Packets.Requests;

namespace Monitor.Debugger.Generators.Requests
{
    public class RegistersRequestPacketGenerator
    {
        public PacketBase Generate()
        {
            var requestPacket = new RegistersRequestPacket();
            requestPacket.RecalculateChecksum();

            return requestPacket;
        }
    }
}