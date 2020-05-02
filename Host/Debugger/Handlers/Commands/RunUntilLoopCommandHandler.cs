using CPU;
using Protocol.Packets;

namespace Host.Debugger.Handlers.Commands
{
    public class RunUntilLoopCommandHandler : PacketHandlerBase
    {
        public RunUntilLoopCommandHandler(Mos6502Core core) : base(core)
        {

        }

        public override PacketBase Handle(PacketBase packet)
        {
            Core.Pins.Ready = true;
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
            Core.Pins.Ready = false;

            return null;
        }
    }
}