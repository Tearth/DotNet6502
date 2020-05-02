using CPU;
using Protocol.Packets;

namespace Host.Debugger.Handlers.Commands
{
    public class StopCommandHandler : PacketHandlerBase
    {
        public StopCommandHandler(Mos6502Core core) : base(core)
        {

        }

        public override PacketBase Handle(PacketBase packet)
        {
            Core.Pins.Ready = false;
            return null;
        }
    }
}