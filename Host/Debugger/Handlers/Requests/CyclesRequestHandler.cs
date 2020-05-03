using Host.Debugger.Generators;
using M6502;
using Protocol.Packets;

namespace Host.Debugger.Handlers.Requests
{
    public class CyclesRequestHandler : PacketHandlerBase
    {
        private readonly CyclesPacketGenerator _cyclesPacketGenerator;

        public CyclesRequestHandler(M6502Core core) : base(core)
        {
            _cyclesPacketGenerator = new CyclesPacketGenerator(core);
        }

        public override PacketBase Handle(PacketBase packet)
        {
            while (!Core.YieldingCycle) ;
            return _cyclesPacketGenerator.Generate();
        }
    }
}