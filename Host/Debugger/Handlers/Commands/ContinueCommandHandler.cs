using M6502;
using Protocol.Packets;

namespace Host.Debugger.Handlers.Commands
{
    public class ContinueCommandHandler : PacketHandlerBase
    {
        public ContinueCommandHandler(M6502Core core) : base(core)
        {

        }

        public override PacketBase Handle(PacketBase packet)
        {
            Core.Pins.Ready = true;
            return null;
        }
    }
}