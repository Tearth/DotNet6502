using CPU;
using Protocol.Packets.Requests;

namespace Host.Debugger.PacketHandlers
{
    public class RegistersRequestHandler : PacketHandlerBase
    {
        public RegistersRequestHandler(Mos6502Core core) : base(core)
        {
        }

        public override byte[] Handle()
        {
            var responsePacket = new RegistersResponsePacket
            {
                ProgramCounter = Core.Registers.ProgramCounter,
                StackPointer = Core.Registers.StackPointer,
                Accumulator = Core.Registers.Accumulator,
                XIndex = Core.Registers.IndexRegisterX,
                YIndex = Core.Registers.IndexRegisterY,
                Flags = (byte) Core.Registers.Flags
            };

            responsePacket.RecalculateChecksum();
            return responsePacket.Data;
        }
    }
}
