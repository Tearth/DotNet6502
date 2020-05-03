using M6502;
using Protocol.Packets;
using Protocol.Packets.Responses;

namespace Host.Debugger.Generators
{
    public class RegistersPacketGenerator
    {
        private readonly M6502Core _core;

        public RegistersPacketGenerator(M6502Core core)
        {
            _core = core;
        }

        public PacketBase Generate()
        {
            var registersPacket = new RegistersPacket
            {
                ProgramCounter = _core.Registers.ProgramCounter,
                StackPointer = _core.Registers.StackPointer,
                Accumulator = _core.Registers.Accumulator,
                XIndex = _core.Registers.IndexRegisterX,
                YIndex = _core.Registers.IndexRegisterY,
                Flags = (byte)_core.Registers.Flags
            };

            registersPacket.RecalculateChecksum();
            return registersPacket;
        }
    }
}