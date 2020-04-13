using CPU;
using Protocol.Packets;
using Protocol.Packets.Requests;

namespace Host.Debugger.Handlers
{
    public class RegistersRequestHandler : PacketHandlerBase
    {
        public RegistersRequestHandler(Mos6502Core core) : base(core)
        {
        }

        public override byte[] Handle(PacketBase packet)
        {
            var registersPacket = new RegistersPacket
            {
                ProgramCounter = Core.Registers.ProgramCounter,
                StackPointer = Core.Registers.StackPointer,
                Accumulator = Core.Registers.Accumulator,
                XIndex = Core.Registers.IndexRegisterX,
                YIndex = Core.Registers.IndexRegisterY,
                Flags = (byte) Core.Registers.Flags
            };

            registersPacket.RecalculateChecksum();
            return registersPacket.Data;
        }
    }
}
