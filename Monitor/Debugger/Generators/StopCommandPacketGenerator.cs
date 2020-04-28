using Monitor.ViewModels;
using Protocol.Packets;
using Protocol.Packets.Commands;

namespace Monitor.Debugger.Generators
{
    public class StopCommandPacketGenerator
    {
        private readonly MainWindowViewModel _viewModel;

        public StopCommandPacketGenerator(MainWindowViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public PacketBase Generate()
        {
            var commandPacket = new StopCommandPacket();
            commandPacket.RecalculateChecksum();

            return commandPacket;
        }
    }
}