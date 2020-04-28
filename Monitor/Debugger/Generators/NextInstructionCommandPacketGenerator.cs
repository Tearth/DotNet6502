using Monitor.ViewModels;
using Protocol.Packets;
using Protocol.Packets.Commands;
using Protocol.Packets.Requests;

namespace Monitor.Debugger.Generators
{
    public class NextInstructionCommandPacketGenerator
    {
        private readonly MainWindowViewModel _viewModel;

        public NextInstructionCommandPacketGenerator(MainWindowViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public PacketBase Generate()
        {
            var commandPacket = new NextInstructionCommandPacket();
            commandPacket.RecalculateChecksum();

            return commandPacket;
        }
    }
}