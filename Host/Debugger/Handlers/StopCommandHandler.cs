using CPU;
using Host.Debugger.Generators;
using Protocol.Packets;

namespace Host.Debugger.Handlers
{
    public class StopCommandHandler : PacketHandlerBase
    {
        public StopCommandHandler(Mos6502Core core) : base(core)
        {

        }

        public override PacketBase Handle(PacketBase packet)
        {
            Core.Pins.Rdy = false;
            return null;
        }
    }
}