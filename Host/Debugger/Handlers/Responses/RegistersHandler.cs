using M6502;
using M6502.Registers;
using Protocol.Packets;
using Protocol.Packets.Responses;

namespace Host.Debugger.Handlers.Responses
{
    public class RegistersHandler : PacketHandlerBase
    {
        public RegistersHandler(M6502Core core) : base(core)
        {
        }

        public override PacketBase Handle(PacketBase packet)
        {
            var registersPacket = (RegistersPacket) packet;

            Core.Registers.ProgramCounter = registersPacket.ProgramCounter;
            Core.Registers.StackPointer = registersPacket.StackPointer;
            Core.Registers.Accumulator = registersPacket.Accumulator;
            Core.Registers.IndexRegisterX = registersPacket.XIndex;
            Core.Registers.IndexRegisterY = registersPacket.YIndex;
            Core.Registers.Flags = (StatusFlags)registersPacket.Flags;

            return null;
        }
    }
}