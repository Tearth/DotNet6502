using Protocol.Packets;
using Protocol.Packets.Requests;

namespace Monitor.Debugger.Generators.Requests
{
    public class MemoryRequestPacketGenerator
    {
        public PacketBase Generate(ushort address, ushort requestedLength, byte tag)
        {
            var requestPacket = new MemoryRequestPacket(address, requestedLength, tag);
            requestPacket.RecalculateChecksum();

            return requestPacket;
        }
    }
}