using Monitor.ViewModels;
using Protocol.Packets;
using Protocol.Packets.Responses;

namespace Monitor.Debugger.Handlers
{
    public class RegistersHandler : PacketHandlerBase
    {
        public RegistersHandler(MainWindowViewModel viewModel) : base(viewModel)
        {

        }

        public override byte[] Handle(PacketBase packet)
        {
            var registersPacket = (RegistersPacket) packet;

            ViewModel.Locked = true;
            ViewModel.Registers.ProgramCounter = registersPacket.ProgramCounter;
            ViewModel.Registers.StackPointer = registersPacket.StackPointer;
            ViewModel.Registers.Accumulator = registersPacket.Accumulator;
            ViewModel.Registers.IndexRegisterX = registersPacket.XIndex;
            ViewModel.Registers.IndexRegisterY = registersPacket.YIndex;
            ViewModel.Registers.Flags = registersPacket.Flags;
            ViewModel.Locked = false;

            return null;
        }
    }
}
