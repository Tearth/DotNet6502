using CPU;
using Host.Debugger.Generators;
using Protocol.Packets;

namespace Host.Debugger.Handlers
{
    public class NextCycleCommandHandler : PacketHandlerBase
    {
        public NextCycleCommandHandler(Mos6502Core core) : base(core)
        {

        }

        public override PacketBase Handle(PacketBase packet)
        {
            var currentCycle = Core.Cycles;

            Core.Pins.Rdy = true;
            while (currentCycle == Core.Cycles) ;
            Core.Pins.Rdy = false;

            return null;
        }
    }
}