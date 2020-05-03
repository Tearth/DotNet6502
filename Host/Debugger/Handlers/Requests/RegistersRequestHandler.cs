using Host.Debugger.Generators;
using M6502;
using Protocol.Packets;

namespace Host.Debugger.Handlers.Requests
{
    public class RegistersRequestHandler : PacketHandlerBase
    {
        private readonly RegistersPacketGenerator _registersPacketGenerator;

        public RegistersRequestHandler(M6502Core core) : base(core)
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
