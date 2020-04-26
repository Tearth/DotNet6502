using CPU;
using Host.Debugger.Generators;
using Protocol.Packets;
using Protocol.Packets.Requests;

namespace Host.Debugger.Handlers
{
    public class RunToAddressCommandHandler : PacketHandlerBase
    {
        public RunToAddressCommandHandler(Mos6502Core core) : base(core)
        {

        }

        public override PacketBase Handle(PacketBase packet)
        {
            var runToAddressCommandPacket = (RunToAddressCommandPacket)packet;

            Core.Pins.Rdy = true;
            while (Core.Registers.ProgramCounter != runToAddressCommandPacket.Address) ;
            Core.Pins.Rdy = false;

            return null;
        }
    }
}