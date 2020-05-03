using Host.Debugger.Generators;
using M6502;
using Protocol.Packets;

namespace Host.Debugger.Handlers.Requests
{
    public class PinsRequestHandler : PacketHandlerBase
    {
        private readonly PinsPacketGenerator _pinsPacketGenerator;

        public PinsRequestHandler(M6502Core core) : base(core)
        {
            _pinsPacketGenerator = new PinsPacketGenerator(core);
        }

        public override PacketBase Handle(PacketBase packet)
        {
            while (!Core.YieldingCycle) ;
            return _pinsPacketGenerator.Generate();
        }
    }
}