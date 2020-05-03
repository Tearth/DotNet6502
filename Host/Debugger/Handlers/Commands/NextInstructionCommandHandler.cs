using M6502;
using Protocol.Packets;

namespace Host.Debugger.Handlers.Commands
{
    public class NextInstructionCommandHandler : PacketHandlerBase
    {
        public NextInstructionCommandHandler(M6502Core core) : base(core)
        {

        }

        public override PacketBase Handle(PacketBase packet)
        {
            Core.Pins.Ready = true;
            while (Core.Pins.Sync) ;
            while (!Core.Pins.Sync) ;
            Core.Pins.Ready = false;

            return null;
        }
    }
}