using CPU;
using Protocol.Packets;

namespace Host.Debugger.Handlers.Commands
{
    public class NextInstructionCommandHandler : PacketHandlerBase
    {
        public NextInstructionCommandHandler(Mos6502Core core) : base(core)
        {

        }

        public override PacketBase Handle(PacketBase packet)
        {
            Core.Pins.Rdy = true;
            while (Core.Pins.Sync) ;
            while (!Core.Pins.Sync) ;
            Core.Pins.Rdy = false;

            return null;
        }
    }
}