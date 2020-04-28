using CPU;
using Protocol.Packets;

namespace Host.Debugger.Handlers
{
    public class RunUntilLoopCommandHandler : PacketHandlerBase
    {
        public RunUntilLoopCommandHandler(Mos6502Core core) : base(core)
        {

        }

        public override PacketBase Handle(PacketBase packet)
        {
            Core.Pins.Rdy = true;
            while (true)
            {
                var programCounter1 = Core.Registers.ProgramCounter;

                while (Core.Pins.Sync) ;
                while (!Core.Pins.Sync) ;

                var programCounter2 = Core.Registers.ProgramCounter;

                while (Core.Pins.Sync) ;
                while (!Core.Pins.Sync) ;

                if (programCounter1 == programCounter2 && programCounter2 == Core.Registers.ProgramCounter)
                {
                    break;
                }
            }
            Core.Pins.Rdy = false;

            return null;
        }
    }
}