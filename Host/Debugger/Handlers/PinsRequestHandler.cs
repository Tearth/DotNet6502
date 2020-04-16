using CPU;
using Host.Debugger.Generators;
using Protocol.Packets;

namespace Host.Debugger.Handlers
{
    public class PinsRequestHandler : PacketHandlerBase
    {
        private readonly PinsPacketGenerator _pinsPacketGenerator;

        public PinsRequestHandler(Mos6502Core core) : base(core)
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