using CPU;
using Host.Debugger.Generators;
using Protocol.Packets;
using Protocol.Packets.Requests;

namespace Host.Debugger.Handlers
{
    public class RunUntilLoopCommandHandler : PacketHandlerBase
    {
        public RunUntilLoopCommandHandler(Mos6502Core core) : base(core)
        {

        }

        public override PacketBase Handle(PacketBase packet)
        {
            while (true)
            {
                var programCounter1 = Core.Registers.ProgramCounter;

                Core.Pins.Rdy = true;
                while (Core.Pins.Sync) ;
                while (!Core.Pins.Sync) ;
                Core.Pins.Rdy = false;

                var programCounter2 = Core.Registers.ProgramCounter;

                Core.Pins.Rdy = true;
                while (Core.Pins.Sync) ;
                while (!Core.Pins.Sync) ;
                Core.Pins.Rdy = false;

                if (programCounter1 == programCounter2 && programCounter2 == Core.Registers.ProgramCounter)
                {
                    break;
                }
            }

            return null;
        }
    }
}