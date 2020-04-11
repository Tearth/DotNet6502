using Monitor.ViewModels;
using Protocol.Packets;
using Protocol.Packets.Requests;

namespace Monitor.Debugger.Handlers
{
    public class RegistersResponseHandler : PacketHandlerBase
    {
        public RegistersResponseHandler(MainWindowViewModel viewModel) : base(viewModel)
        {

        }

        public override byte[] Handle(PacketBase packet)
        {
            var registersPacket = (RegistersResponsePacket) packet;

            ViewModel.Registers.Pc = registersPacket.ProgramCounter;
            ViewModel.Registers.Sp = registersPacket.StackPointer;
            ViewModel.Registers.Acc = registersPacket.Accumulator;
            ViewModel.Registers.X = registersPacket.XIndex;
            ViewModel.Registers.Y = registersPacket.YIndex;
            ViewModel.Registers.Flags = registersPacket.Flags;

            return null;
        }
    }
}
