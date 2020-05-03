using M6502;
using Protocol.Packets;

namespace Host.Debugger
{
    public abstract class PacketHandlerBase
    {
        protected M6502Core Core;

        protected PacketHandlerBase(M6502Core core)
        {
            Core = core;
        }

        public abstract PacketBase Handle(PacketBase packet);
    }
}
