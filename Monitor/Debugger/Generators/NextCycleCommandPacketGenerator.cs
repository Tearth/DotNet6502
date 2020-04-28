using Monitor.ViewModels;
using Protocol.Packets;
using Protocol.Packets.Commands;

namespace Monitor.Debugger.Generators
{
    public class NextCycleCommandPacketGenerator
    {
        private readonly MainWindowViewModel _viewModel;

        public NextCycleCommandPacketGenerator(MainWindowViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public PacketBase Generate()
        {
            var commandPacket = new NextCycleCommandPacket();
            commandPacket.RecalculateChecksum();

            return commandPacket;
        }
    }
}