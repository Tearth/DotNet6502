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
                ProgramCounter = registersViewModel.Pc,
                StackPointer = registersViewModel.Sp,
                Accumulator = registersViewModel.Acc,
                XIndex = registersViewModel.X,
                YIndex = registersViewModel.Y,
                Flags = registersViewModel.Flags
            };
            registersPacket.RecalculateChecksum();

            return registersPacket;
        }
    }
}
