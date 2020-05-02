using CPU;
using Protocol.Packets;

namespace Host.Debugger.Handlers.Commands
{
    public class NextCycleCommandHandler : PacketHandlerBase
    {
        public NextCycleCommandHandler(Mos6502Core core) : base(core)
        {

        }

        public override PacketBase Handle(PacketBase packet)
        {
            var currentCycle = Core.Cycles;

            Core.Pins.Ready = true;
            while (currentCycle == Core.Cycles) ;
            Core.Pins.Ready = false;

            return null;
        }
    }
}