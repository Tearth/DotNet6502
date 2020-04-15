using CPU;
using Host.Debugger.Generators;
using Protocol.Packets;
using Protocol.Packets.Requests;

namespace Host.Debugger.Handlers
{
    public class MemoryRequestHandler : PacketHandlerBase
    {
        private readonly MemoryPacketGenerator _memoryPacketGenerator;

        public MemoryRequestHandler(Mos6502Core core) : base(core)
        {
            _memoryPacketGenerator = new MemoryPacketGenerator(core);
        }

        public override PacketBase Handle(PacketBase packet)
        {
            var memoryRequestPacket = (MemoryRequestPacket) packet;
            return _memoryPacketGenerator.Generate
            (
                memoryRequestPacket.Address, 
                memoryRequestPacket.RequestedLength, 
                memoryRequestPacket.Tag
            );
        }
    }
}