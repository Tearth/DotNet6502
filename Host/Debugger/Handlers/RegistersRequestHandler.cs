using CPU;
using Host.Debugger.Generators;
using Protocol.Packets;

namespace Host.Debugger.Handlers
{
    public class RegistersRequestHandler : PacketHandlerBase
    {
        private readonly RegistersPacketGenerator _registersPacketGenerator;

        public RegistersRequestHandler(Mos6502Core core) : base(core)
        {
            _registersPacketGenerator = new RegistersPacketGenerator(core);
        }

        public override PacketBase Handle(PacketBase packet)
        {
            while (!Core.YieldingCycle) ;
            return _registersPacketGenerator.Generate();
        }
    }
}
