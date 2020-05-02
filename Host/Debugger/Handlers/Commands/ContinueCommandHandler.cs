using CPU;
using Protocol.Packets;

namespace Host.Debugger.Handlers.Commands
{
    public class ContinueCommandHandler : PacketHandlerBase
    {
        public ContinueCommandHandler(Mos6502Core core) : base(core)
        {

        }

        public override PacketBase Handle(PacketBase packet)
        {
            Core.Pins.Rdy = true;
            return null;
        }
    }
}