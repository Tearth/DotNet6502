using CPU;
using Host.Debugger.Generators;
using Protocol.Packets;

namespace Host.Debugger.Handlers
{
    public class NextInstructionCommandHandler : PacketHandlerBase
    {
        public NextInstructionCommandHandler(Mos6502Core core) : base(core)
        {

        }

        public override PacketBase Handle(PacketBase packet)
        {
            var currentCycle = Core.Cycles;

            Core.Pins.Rdy = true;
            while (currentCycle == Core.Cycles) ;
            while (!Core.Pins.Sync) ;
            Core.Pins.Rdy = false;

            return null;
        }
    }
}