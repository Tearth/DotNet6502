using Monitor.ViewModels;
using Protocol.Packets;
using Protocol.Packets.Responses;

namespace Monitor.Debugger.Generators.Responses
{
    public class RegistersPacketGenerator
    {
        public PacketBase Generate(RegistersViewModel registersViewModel)
        {
            var registersPacket = new RegistersPacket
            {
                ProgramCounter = registersViewModel.ProgramCounter,
                StackPointer = registersViewModel.StackPointer,
                Accumulator = registersViewModel.Accumulator,
                XIndex = registersViewModel.IndexRegisterX,
                YIndex = registersViewModel.IndexRegisterY,
                Flags = registersViewModel.Flags
            };
            registersPacket.RecalculateChecksum();

            return registersPacket;
        }
    }
}
