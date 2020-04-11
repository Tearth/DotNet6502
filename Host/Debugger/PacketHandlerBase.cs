using CPU;
using Protocol.Packets;

namespace Host.Debugger
{
    public abstract class PacketHandlerBase
    {
        protected Mos6502Core Core;

        protected PacketHandlerBase(Mos6502Core core)
        {
            Core = core;
        }

        public abstract byte[] Handle(PacketBase packet);
    }
}
