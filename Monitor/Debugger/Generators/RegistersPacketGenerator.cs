using Monitor.ViewModels;
using Protocol.Packets;
using Protocol.Packets.Requests;
using Protocol.Packets.Responses;

namespace Monitor.Debugger.Generators
{
    public class RegistersPacketGenerator
    {
        private readonly MainWindowViewModel _viewModel;

        public RegistersPacketGenerator(MainWindowViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public PacketBase Generate()
        {
            var registersPacket = new RegistersPacket
            {
                ProgramCounter = _viewModel.Registers.Pc,
                StackPointer = _viewModel.Registers.Sp,
                Accumulator = _viewModel.Registers.Acc,
                XIndex = _viewModel.Registers.X,
                YIndex = _viewModel.Registers.Y,
                Flags = _viewModel.Registers.Flags
            };
            registersPacket.RecalculateChecksum();

            return registersPacket;
        }
    }
}
